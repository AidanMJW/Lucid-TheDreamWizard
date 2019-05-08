using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurusAnimation : MonoBehaviour
{
    SpriteRenderer spriteRen;
    EnemyAiMovement aiMovement;
    public EnemyMeleeAttack enemyMelee;
    Animator anim;
    bool attacked = false;

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        aiMovement = GetComponent<EnemyAiMovement>();
        anim = GetComponent<Animator>();
        enemyMelee = GetComponent<EnemyMeleeAttack>();
    }

    private void Update()
    {
        flipSprite();
        setAttackAnim();
    }

    void flipSprite()
    {
        if (aiMovement.dir == 1)
            spriteRen.flipX = false;
        else
            spriteRen.flipX = true;
    }

    void setAttackAnim()
    {
        if(enemyMelee.attacking == true && attacked == false)
        {
            anim.SetTrigger("Attack");
            attacked = true;
        }
        if(enemyMelee.attacking == false && attacked == true)
        {
            attacked = false;
        }
    }
}
