using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShake : MonoBehaviour
{
    
    public float breakTime = 3f;
    GameObject plaformParent;

    bool braking = false;
    bool check = false;

    void Start()
    {
        plaformParent = transform.parent.gameObject;
    }

    void Update()
    {
        if(braking == true)
        {
            if (check == false)
            {
                plaformParent.transform.position = new Vector3(plaformParent.transform.position.x + 1f * Time.deltaTime, plaformParent.transform.position.y + 1f * Time.deltaTime, 0);
                check = true;
            }
            else
            {
                plaformParent.transform.position = new Vector3(plaformParent.transform.position.x + -1f * Time.deltaTime, plaformParent.transform.position.y + -1f * Time.deltaTime, 0);
                check = false;
            }

            breakTime -= Time.deltaTime;

            if (breakTime <= 0)
                breakPlatform();

        }



    }

    public void setBreak()
    {
        braking = true;
    }

    void breakPlatform()
    {
        plaformParent.gameObject.SetActive(false);
    }

}
