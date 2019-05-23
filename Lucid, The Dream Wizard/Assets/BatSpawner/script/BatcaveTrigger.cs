using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatcaveTrigger : MonoBehaviour
{
    //a temp variable for creation of bat clones
    public GameObject m_Bat;
    private GameObject m_BatClone;
    public float percentageProbability = 25;
    private float percentageHolder = 25;

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
            percentageHolder = percentageProbability * DifficultyManager.getAttackMultiplier();
            
            int chance = Random.Range(0, 100);
            if (chance < percentageHolder)
            {
                m_BatClone = Instantiate(m_Bat, transform.position, Quaternion.identity);
            }
        }
    }
}
