using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHit : MonoBehaviour
{
    public float attackPower = 12f;
    public float damage = 10f;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<Player>().takeDamage(5 * DifficultyManager.getAttackMultiplier());
                
            DestroyThis();
           
        }
    }
   

    void DestroyThis()
    {
        GameObject impact = Instantiate(impactEffect);
        impact.transform.position = transform.position;
        Destroy(transform.gameObject);
    }
}
