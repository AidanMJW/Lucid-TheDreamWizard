using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public Vector2Int lootDropRange = new Vector2Int();
    public List<GameObject> loot = new List<GameObject>();
    public GameObject deathEffect;

    void Update()
    {
        if (health <= 0 || transform.position.y <= -50)
            die();
    }

    public void takeDamage(float amount)
    {
        float h = health;
        h -= amount;
        if (h < 0)
            h = 0;
        health = h;
    }

    void die()
    {
        dropLoot();
        GameObject d = Instantiate(deathEffect);
        d.transform.position = transform.position;
        Destroy(transform.gameObject);
    }

    void dropLoot()
    {
        int amount = Random.Range(lootDropRange.x, lootDropRange.y + 1);

        if(amount != 0)
        {
            for(int i = 0; i < amount; i++)
            {
                GameObject l = Instantiate(loot[Random.Range(0, loot.Count)]);
                Vector3 spawnPos = transform.position;
                spawnPos.x += Random.Range(-0.1f, 0.1f);
                l.transform.position = spawnPos;
            }
        }
    }
}
