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
           // Debug.Log("player entered idle trigger - idle mode ");
            //set state to idle
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Idle;            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //Debug.Log("player exited idle trigger - inactive mode mode ");
            gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Inactive;
        }
    }
}
