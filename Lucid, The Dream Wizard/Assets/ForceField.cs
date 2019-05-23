using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerProjectile")//other.name == "FireBall(Clone)"
        {
            //  Debug.Log("fireball entered evasive trigger ");
            Destroy(other.gameObject);
        }
    }
}
