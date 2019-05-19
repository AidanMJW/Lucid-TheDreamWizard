using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   /* private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //set state to idle
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Attack;
        }
        else if (other.tag == "PlayerProjectile")
        {
            //Debug.Log("fireball stayed in weapons trigger " + other.name);
        }
    }*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
           // Debug.Log("player entered weapons trigger - attack mode ");
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Attack;
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
          //  Debug.Log("player entered weapons trigger - idle mode ");
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Idle;
        }
    }
}
