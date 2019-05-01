using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackPower;
    public float attackRange;
    public GameObject projectile;
    public Vector3 attackOffset;
    public float attackTime;
    public float fireTime;
    float baseAttackTime;

    public bool isFireing = false;
    bool hasFired = false;
    Vector3 direction;
    SpriteRenderer sRenderer;
    PlayerController pController;

    private void Start()
    {
        baseAttackTime = attackTime;
        sRenderer = GetComponent<SpriteRenderer>();
        pController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && pController.getGrounded() && isFireing == false)
        {
            isFireing = true;
        }          

        if(isFireing == true)
        {
            attackTime -= Time.deltaTime;

            if(attackTime <= fireTime && attackTime >0 && hasFired == false)
            {
                fireProjectile();
                hasFired = true;
            }
            if(attackTime <= 0)
            {
                isFireing = false;
                hasFired = false;
                attackTime = baseAttackTime;
            }
        }
    }

    void fireProjectile()
    {

        GameObject p = Instantiate(projectile);



        if (sRenderer.flipX == true)
        {
            direction = Vector3.left;
            p.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            direction = Vector3.right;
        }
            

        p.transform.position = (transform.position +(attackOffset * direction.x));

        
        p.GetComponent<Projectile>().direction = direction;
        p.GetComponent<Projectile>().damage = attackPower;
    }


}
