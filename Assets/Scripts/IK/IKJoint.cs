using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IKJoint : MonoBehaviour
{
    private Transform mainTransform;
    public Transform basePoint;
    public Transform bone1;
    public Transform bone2;
    public Transform endPoint;

    [Space(10)]
    public Transform target;
    public Transform hintPos;
    public Transform useTargetRotation;

    [Space(10)]
    public bool rotateBase = true;

    [Space(10)]
    public Vector3 bone1_OffsetRotation;
    public Vector3 bone2_OffsetRotation;
    public Vector3 Target_OffsetRotation;

    private void Start()
    {
        mainTransform = transform.root;
    }

    public void LateUpdate()
    {
        if (bone1 != null && bone2 != null && endPoint != null && target != null)
        {
            float bone1_Length = Vector3.Distance(bone1.position, bone2.position);
            float forearm_Length = Vector3.Distance(bone2.position, endPoint.position);

            Vector3 hint = (bone1.position + endPoint.position) / 2 + transform.up;
            if (hintPos) hint = hintPos.position;
            else if (mainTransform) hint = (bone1.position + endPoint.position) / 2 + mainTransform.up;

            if (rotateBase)
            {
                Vector3 baseToTarget = target.position - basePoint.position; baseToTarget.y = 0;
                basePoint.rotation = Quaternion.LookRotation(transform.up, baseToTarget);
            }

            bone1.LookAt(target, hint - bone1.position);
            bone1.Rotate(bone1_OffsetRotation);

            Vector3 cross = Vector3.Cross(hint - bone1.position, bone2.position - bone1.position);

            float totalLength = bone1_Length + forearm_Length;
            float targetDistance = Vector3.Distance(bone1.position, target.position);
            targetDistance = Mathf.Min(targetDistance, totalLength - totalLength * 0.001f);

            float adjacent = ((bone1_Length * bone1_Length) - (forearm_Length * forearm_Length) + (targetDistance * targetDistance)) / (2 * targetDistance);
            float angle = Mathf.Acos(adjacent / bone1_Length) * Mathf.Rad2Deg;

            bone1.RotateAround(bone1.position, cross, -angle);

            bone2.LookAt(target, cross);
            bone2.Rotate(bone2_OffsetRotation);

            if(useTargetRotation) useTargetRotation.rotation = target.rotation * Quaternion.Euler(Target_OffsetRotation);
        }
    }
}
