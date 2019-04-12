using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LandChunkGenerator))]
public class LandChunkGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LandChunkGenerator myScript = (LandChunkGenerator)target;
        if (GUILayout.Button("Build LandChunk"))
        {
            myScript.buildLandChunk();
        }
    }
}
