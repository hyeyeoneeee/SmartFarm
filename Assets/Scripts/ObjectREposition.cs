using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Greenhouse)), CanEditMultipleObjects]
public class ObjectREposition : Editor
{
    /*[SerializeField] Transform target;
    public void Reposition()
    {
        Transform[] trans = target.GetComponentsInChildren<Transform>();
        for(int i = 1; i < trans.Length; i++)
        {
            Vector3 prev_pos = trans[i].position;
            prev_pos.x += trans[0].position.x;
            prev_pos.y += trans[0].position.y;
            prev_pos.z += trans[0].position.z;
            trans[i].position = prev_pos;
        }
        trans[0].position = Vector3.zero;
    }*/

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("RePosition"))
        {
            Greenhouse greenhouse = (Greenhouse)target;
            greenhouse.Reposition();
        }
    }
}
