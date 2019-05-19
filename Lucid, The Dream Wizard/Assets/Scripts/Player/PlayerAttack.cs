using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackPower;
    public float attackRange;
    public GameObject projectile;
    public GameObject dreamStateProjectile;
    public Vector3 attackOffset;
    public float attackTime;
    public float fireTime;
    float baseAttackTime;

    public bool isFireing = false;

    GameObject activeProjectile;
    bool hasFired = false;
    bool fire = false;
    Vector3 direction;
    SpriteRenderer sRenderer;
    DreamStateManager dreamState;

    private void Start()
    {
        baseAttackTime = attackTime;
        sRenderer = GetComponent<SpriteRenderer>();
        dreamState = GetComponent<DreamStateManager>();
    }

    private void Update()
    {
        if (MenuManager.getPauseState() == false)
        {
            if (Input.GetButton("Fire1")  && isFireing == false)
            {
                isFireing = true;
            }
        }
         
        if(isFireing == true)
        {
            if(fire && hasFired == false)
            {
                fireProjectile();
                hasFired = true;
            }
        }
    }

    public void resetFire()
    {
        isFireing = false;
        hasFired = false;
        fire = false;
    }


    public void setFire()
    {
        fire = true;
    }

    void fireProjectile()
    {
        if (dreamState.inDreamState)
            activeProjectile = dreamStateProjectile;
        else
            activeProjectile = projectile;

        GameObject p = Instantiate(activeProjectile);

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
        p.GetComponent<Projectile>().tag = "PlayerProjectile";

        if (dreamState.inDreamState)
            p.GetComponent<Projectile>().damage = attackPower * 2;
        else
            p.GetComponent<Projectile>().damage = attackPower;
        

    }


}
