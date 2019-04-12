using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformGenerator))]
public class PlatformGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlatformGenerator myScript = (PlatformGenerator)target;
        if (GUILayout.Button("Build Platform"))
        {
            myScript.buildPlatform();
        }
    }
}
