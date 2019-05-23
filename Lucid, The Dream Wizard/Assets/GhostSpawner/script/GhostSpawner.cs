using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public float percentageProbability = 25;
    private float percentageHolder = 25;
    public GameObject m_Ghost=null;
    private GameObject m_GhostClone;

    // Start is called before the first frame update
    void Start()
    {
       
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            percentageHolder = percentageProbability * DifficultyManager.getAttackMultiplier();
            int chance = Random.Range(0, 100);
            if(chance < percentageHolder)
            {
                m_GhostClone = Instantiate(m_Ghost, transform.position, Quaternion.identity);
            }
                       
        }
    }
}
