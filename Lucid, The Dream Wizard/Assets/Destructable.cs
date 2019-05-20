using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destoryEffect;
    public List<Drop> Drops = new List<Drop>();
    bool destroyed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile" && destroyed == false)
        {
            destroyed = true;
            collision.gameObject.GetComponent<Projectile>().DestroyThis(true);
            dropLoot();
            DestoyThis();  
        }
    }

    void DestoyThis()
    {
        GameObject d = Instantiate(destoryEffect);
        d.transform.position = transform.position;

        Destroy(this.gameObject);
    }

    void dropLoot()
    {
        int amount = Random.Range(0, 3);
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
            for (int i = 0; i < amount; i++)
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
