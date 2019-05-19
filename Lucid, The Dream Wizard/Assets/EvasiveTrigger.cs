using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveTrigger : MonoBehaviour
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
               // gameObject.GetComponentInParent<DemonController>().m_DemonState = DemonController.DemonState.Attack;
            }
            else if (other.tag == "PlayerProjectile")//other.name == "FireBall(Clone)"
            {
             //  Debug.Log("fireball entered evasive trigger ");
                gameObject.GetComponentInParent<DemonController>().avoid(other.gameObject);
            }
        }

   
}
