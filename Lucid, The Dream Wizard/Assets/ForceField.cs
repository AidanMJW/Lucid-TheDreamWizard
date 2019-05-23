using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public GameObject boss;
    void Start()
    {
        
    }

    private void Update()
    {
        transform.position = boss.transform.position;
    }

}
