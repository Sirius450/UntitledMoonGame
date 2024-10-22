using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;      //pour faire du code existant seulement pour le dev

[CustomEditor(typeof(EnemyFSM))]
public class EnemyFSMEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyFSM Target = (EnemyFSM)target;

        DrawDefaultInspector();

        EditorGUILayout.LabelField(Target.GetState().ToString());
    }
}
