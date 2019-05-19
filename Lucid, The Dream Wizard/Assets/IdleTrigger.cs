using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleTrigger : MonoBehaviour
{
  

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //set state to idle
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Idle;            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Inactive;
        }
    }
}
