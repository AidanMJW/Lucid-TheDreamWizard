using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public List<GameObject> clouds = new List<GameObject>();
    public float speed;
    public Vector3 startPoint;
    public Vector3 EndPoint;


    private void Update()
    {
        for(int i = 0; i < clouds.Count; i++)
        {
            clouds[i].transform.position = new Vector3(clouds[i].transform.position.x - speed * Time.deltaTime, clouds[i].transform.position.y, clouds[i].transform.position.z);
            if(clouds[i].transform.position.x <= EndPoint.x)
                clouds[i].transform.position = new Vector3(startPoint.x, clouds[i].transform.position.y, clouds[i].transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(startPoint, 0.2f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(EndPoint, 0.2f);
    }
}
