using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathSpline))]
public class PathSplineInspector : BezierSplineInspector
{
    SerializedProperty StartPathsProp;
    SerializedProperty EndPathsProp;

    private void OnEnable()
    {
        StartPathsProp = serializedObject.FindProperty("StartPaths");
        EndPathsProp = serializedObject.FindProperty("EndPaths");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.PropertyField(StartPathsProp, true);
        EditorGUILayout.PropertyField(EndPathsProp, true);

        serializedObject.ApplyModifiedProperties();
    }
}
