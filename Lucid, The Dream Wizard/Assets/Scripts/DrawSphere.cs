using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphere : MonoBehaviour
{ 
    public Color color = Color.blue;
    public float size = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, size);
    }
}
