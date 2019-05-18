using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //set state to idle
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Attack;
        }
        else if (other.name == "FireBall(Clone)")
        {
            Debug.Log("fireballed" + other.name);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //set state to idle
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Attack;
        }
        else if (other.name == "FireBall(Clone)")
        {
            Debug.Log("fireballed"+ other.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Idle;
        }
    }
}
