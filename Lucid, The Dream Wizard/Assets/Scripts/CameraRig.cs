using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public bool clampToPlayer = false;
    public float clampSpeed;
    float _clampSpeed;
    public float lockSpeed;

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
        _clampSpeed = clampSpeed;
    }

    void Update()
    {
        Debug.Log(getPlayerSpeed());

        if (!clampToPlayer)
        {
            if (getPlayerSpeed() >= lockSpeed)
            {
                if(getPlayerSpeed() >= lockSpeed * 2)
                    clampSpeed = 0.025f;
                else
                    clampSpeed = 0.05f; 
            }
                
            else
                clampSpeed = _clampSpeed;

                adjustScreen(clampSpeed);
        }
        else
        {
            transform.position = player.transform.position;
        }
    }

    void adjustScreen(float speed)
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref vel, speed);
    }

    float getPlayerSpeed()
    {
        float playerSpeed = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        return playerSpeed;
    }

}
