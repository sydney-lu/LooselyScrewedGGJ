using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class SplineMesh : MonoBehaviour
{
    [Space(10)]
    [SerializeField]
    protected BezierSpline spline;
    [SerializeField]
    protected Mesh flatMesh;
    [SerializeField]
    protected int frequency = 20;

    [Space(10)]
    public GameObject startObject;
    public GameObject endObject;

    [Space(10)]
    [SerializeField]
    protected bool hasBottom;
    [SerializeField]
    protected bool flipEnd;

    [HideInInspector]
    public GameObject subObject;
    private MeshFilter filter;
    private MeshRenderer renderer;
    protected Mesh mesh;
    protected int flatVertCount;

    protected virtual void Start()
    {
        if (!filter || !mesh)
            InitializeObject();
        if (spline && flatMesh)
            Generate();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            if (!filter || !mesh)
                InitializeObject();
            Generate();
        }
#endif
    }

    private void InitializeObject()
    {
        if (!subObject)
        {
            subObject = new GameObject(name + "_SplineMesh");
            subObject.transform.parent = transform;
            subObject.transform.localPosition = Vector3.zero;
            subObject.transform.localRotation = Quaternion.identity;
            subObject.AddComponent<MeshRenderer>();
            subObject.AddComponent<MeshFilter>();
        }
        if (!filter) filter = subObject.GetComponent<MeshFilter>();
        if (!renderer) renderer = subObject.GetComponent<MeshRenderer>();
        if (!mesh)
        {
            mesh = new Mesh();
            mesh.name = name + "_SplineMesh";
        }
        filter.sharedMesh = mesh;
    }

    public void UpdateEnds()
    {
        if (spline && flatMesh)
        {
            if (!spline.Loop)
            {

                if (startObject)
                {
                    startObject.transform.position = spline.GetPoint(0);
                    startObject.transform.rotation = Quaternion.LookRotation(-spline.GetDirection(0));
                    startObject.SetActive(true);
                }
                if (endObject)
                {
                    endObject.transform.position = spline.GetPoint(1);
                    endObject.transform.rotation = Quaternion.LookRotation(spline.GetDirection(1) * (flipEnd ? 1 : -1));
                    endObject.SetActive(true);
                }
            }
            else
            {
                if (startObject) startObject.SetActive(false);
                if (endObject) endObject.SetActive(false);
            }
        }
    }

    public Vector3 GetStartPos()
    {
        if (startObject) return startObject.transform.position;
        else return spline.GetPoint(0);
    }
    public Vector3 GetEndPos()
    {
        if (endObject) return endObject.transform.position;
        else return spline.GetPoint(1);
    }

    public void Generate()
    {
        if (spline && flatMesh)
        {
            flatVertCount = flatMesh.vertexCount;

            List<Vector3> newVerts = GenerateVerts();
            List<int> newTriLists = GenerateTris(newVerts);
            ApplyToMesh(newVerts, newTriLists);
            UpdateEnds();
        }
    }

    private List<Vector3> GenerateVerts()
    {
        List<Vector3> newVerts = new List<Vector3>();

        float stepSize;
        if (spline.Loop)
            stepSize = 1 / ((float)frequency);
        else stepSize = 1 / ((float)frequency - 1);

        for (int f = 0; f < frequency; f++)
        {
            for (int i = 0; i < flatVertCount; i++)
            {
                Quaternion RotOffset = Quaternion.Inverse(transform.rotation);
                Vector3 vert = flatMesh.vertices[i];
                Vector3 Offset = spline.GetPoint(stepSize * f);

                Offset.x = (Offset.x - transform.position.x);
                Offset.y = (Offset.y - transform.position.y);
                Offset.z = (Offset.z - transform.position.z);
                Offset = RotOffset * Offset;

                Offset.x /= transform.localScale.x;
                Offset.y /= transform.localScale.y;
                Offset.z /= transform.localScale.z;

                vert = (Quaternion.LookRotation(spline.GetDirection(stepSize * f))) * vert + Offset;
                newVerts.Add(vert);
            }
        }
        return newVerts;
    }

    private List<int> GenerateTris(List<Vector3> verts)
    {
        List<int> trisLists = new List<int>();

        int loopModifier = !spline.Loop ? 1 : 0;
        int totalVerts = flatVertCount * (frequency + loopModifier);

        for (int f = 0; f < frequency - loopModifier; f++)
        {
            for (int i = 0; i < flatVertCount - (!hasBottom ? 1 : 0); i++)
            {
                int v1 = ((i % flatVertCount) + (flatVertCount * f)) % totalVerts;
                int v2 = (((i + 1) % flatVertCount) + (flatVertCount * f)) % totalVerts;
                int v3 = (v1 + flatVertCount) % totalVerts;
                int v4 = (v2 + flatVertCount) % totalVerts;
                
                trisLists.AddRange(new List<int> { v1, v2, v3 });
                trisLists.AddRange(new List<int> { v2, v4, v3 });
            }
        }
        return trisLists;
    }

    private void ApplyToMesh(List<Vector3> verts, List<int> TriLists)
    {
        mesh.SetVertices(verts);

        mesh.subMeshCount = 1;
        mesh.SetTriangles(TriLists, 0);
        mesh.RecalculateNormals();
        SetGradient();
    }

    public void SetGradient()
    {
        Vector3[] verts = mesh.vertices;
        Color32[] colors = new Color32[verts.Length];

        for (int i = 0; i < verts.Length; i++)
        {
            float step = Mathf.Floor(i / flatVertCount) / frequency;
            colors[i].r = (byte)Mathf.Lerp(0 * 255, 1 * 255, step);
        }
        mesh.colors32 = colors;
    }

    public void SetMaterialParam(string name, float value)
    {
        if(renderer) renderer.material.SetFloat(name, value);
    }
    public void SetMaterialParam(string name, int value)
    {
        if (renderer) renderer.material.SetInt(name, value);
    }
    public void SetMaterialParam(string name, Color value)
    {
        if (renderer) renderer.material.SetColor(name, value);
    }
}