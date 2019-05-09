using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0.3f;
    public Vector3 direction = Vector3.zero;
    public float damage;
    public GameObject impactEffect;
    Vector2 vel = Vector3.zero;
    GameObject player;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(direction != Vector3.zero)
        {
            transform.position += direction * speed * Time.deltaTime ;
        }

        destoryTest();

        if(transform.position.z != 0)
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
    }

    void destoryTest()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 10f)
            DestroyThis(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            DestroyThis();
        }
    }

    void DestroyThis( bool impacted = true)
    {
        if(impacted)
        {
            GameObject impact = Instantiate(impactEffect);
            impact.transform.position = transform.position;
        }

        Destroy(transform.gameObject);
    }

    private void OnBecameInvisible()
    {
        DestroyThis(false);
    }

}
