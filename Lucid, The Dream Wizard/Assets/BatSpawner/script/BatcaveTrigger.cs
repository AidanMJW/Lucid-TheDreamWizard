using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatcaveTrigger : MonoBehaviour
{
    //a temp variable for creation of bat clones
    public GameObject m_Bat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
           m_Bat = Instantiate(m_Bat, transform.position, Quaternion.identity);
        }
    }
}
