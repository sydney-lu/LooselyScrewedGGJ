using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AmountCondition))]
public class AmountContitionEditor : Editor
{
    SerializedProperty script;

    SerializedProperty findByProp;
    SerializedProperty compareByProp;

    SerializedProperty checkTagProp;
    SerializedProperty layerProp;
    SerializedProperty typeTemplateProp;

    SerializedProperty amountProp;
    SerializedProperty curObjectsProp;

    private void OnEnable()
    {
        script = serializedObject.FindProperty("m_Script");

        findByProp = serializedObject.FindProperty("findBy");
        checkTagProp = serializedObject.FindProperty("checkTag");
        layerProp = serializedObject.FindProperty("layer");
        typeTemplateProp = serializedObject.FindProperty("typeTemplate");

        compareByProp = serializedObject.FindProperty("compareBy");
        amountProp = serializedObject.FindProperty("amount");
        curObjectsProp = serializedObject.FindProperty("currentObjects");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(script);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(findByProp);

            int findBySelection = findByProp.enumValueIndex;
            if (findBySelection == 0)
                EditorGUILayout.PropertyField(checkTagProp);

            else if (findBySelection == 1)
                EditorGUILayout.PropertyField(layerProp);

            else if (findBySelection == 2)
                EditorGUILayout.PropertyField(typeTemplateProp);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(compareByProp, GUIContent.none);
                EditorGUILayout.PropertyField(amountProp, GUIContent.none);
                EditorGUILayout.LabelField("CurrentObjects:  " + curObjectsProp.intValue);
            }
            EditorGUILayout.EndHorizontal();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
