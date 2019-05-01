using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurusAnimation : MonoBehaviour
{
    SpriteRenderer spriteRen;
    EnemyAiMovement aiMovement;

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        aiMovement = GetComponent<EnemyAiMovement>();
    }

    private void Update()
    {
        flipSprite();
    }
    void flipSprite()
    {
        if (aiMovement.dir == 1)
            spriteRen.flipX = false;
        else
            spriteRen.flipX = true;
    }
}
