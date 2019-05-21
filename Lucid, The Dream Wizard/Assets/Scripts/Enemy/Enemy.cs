using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public Vector2Int lootDropRange = new Vector2Int();
    public List<Drop> Drops = new List<Drop>();
    public GameObject deathEffect;
    public bool active = true;
    SpriteRenderer sRender;
    DamageFlash damageFlash;


    private void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        damageFlash = GetComponent<DamageFlash>();
    }

    void Update()
    {
        if (health <= 0 || transform.position.y <= -50)
            die();


    }





    public bool takeDamage(float amount)
    {
        if(active)
        {
            float h = health;
            h -= amount;
            if (h < 0)
                h = 0;
            health = h;
        }
        damageFlash.changeColour = true;
        return active;
       
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
        int amount = Random.Range(DifficultyManager.getDropRateMax().x, DifficultyManager.getDropRateMax().y + 1);
        int probablity = Random.Range(1, 11); 
        List<Drop> _drops = new List<Drop>();
        foreach (Drop d in Drops)
        {
            if (d.probablity >= probablity)
            {
                _drops.Add(d);
            }

        }

        if (amount != 0 && _drops.Count > 0)
        {
            for(int i = 0; i < amount; i++)
            {

                _drops.Clear();
                probablity = Random.Range(1, 11);
                foreach (Drop d in Drops)
                {
                    if (d.probablity >= probablity)
                    {
                        _drops.Add(d);
                    }
                        
                }

                GameObject l = Instantiate(_drops[Random.Range(0, _drops.Count)].worldItem);
                Vector3 spawnPos = transform.position;
                spawnPos.y += 0.1f;
                spawnPos.x += Random.Range(-0.1f, 0.1f);
                l.transform.position = spawnPos;
            }
        }
    }
}
