using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public int percentageChanceOfSpawning = 25;
    public GameObject m_Ghost;
    private GameObject m_GhostClone;

    // Start is called before the first frame update
    void Start()
    {

    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            int chance = Random.Range(0, 100);
            if(chance < percentageChanceOfSpawning)
            {
                m_GhostClone = Instantiate(m_Ghost, transform.position, Quaternion.identity);
            }
                       
        }
    }
}
