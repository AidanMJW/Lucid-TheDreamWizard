using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public bool clampToPlayer = false;
    public float clampSpeed;

    [Space(10)]
    public float horizontalSpeed;
    public float verticalSpeed;
    public float verticalDistance;
    public float horizontalDistance;


    Vector3 vel = Vector3.zero;

    Vector3 verticle = Vector3.zero;
    Vector3 verticlePlayer = Vector3.zero;

    Vector3 horizontal = Vector3.zero;
    Vector3 horizontalPlayer = Vector3.zero;

    GameObject player;
    Camera cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if(clampToPlayer)
        {
            adjustScreen(clampSpeed);
        }
        else
        {
            verticle.y = transform.position.y;
            verticlePlayer.y = player.transform.position.y;

            horizontal.x = transform.position.x;
            horizontalPlayer.x = player.transform.position.x;

            if (Vector3.Distance(verticle, verticlePlayer) > verticalDistance)
                adjustScreen(verticalSpeed);
            else if (Vector3.Distance(horizontal, horizontalPlayer) > horizontalDistance)
                adjustScreen(horizontalSpeed);
            else { }
        }
    }

    void adjustScreen(float speed)
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref vel, speed);
    }


}
