using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackPower = 25f;
    public float attackOffset;
    public float attackCooldown;
    public float hitTime;
    float timer;
    float timer2;
    GameObject player;
    SpriteRenderer sRenderer;
    public bool attacking = false;
    bool attacked = false;
    bool attackEvent = false;
    Vector2 direction;
    private AudioSource enemyAudio;
    public AudioClip enemyDamageClip;
    public AudioClip enemySpawnClip;
     

    void Start()
    {
        timer = attackCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
        sRenderer = GetComponent<SpriteRenderer>();
        enemyAudio = GetComponent<AudioSource>();
        enemyAudio.clip = enemySpawnClip;
        enemyAudio.Play();
    }

    void Update()
    {
        setDirection();

        if (attacking)
        {
            timer -= Time.deltaTime;
            if (attacked  == false && attackEvent == true)
                attack(direction);
            if(timer <= 0)
            {
                timer = attackCooldown;
                attacking = false;
                attacked = false;
                attackEvent = false;
            }
        }
        else if(attacking == false)
        {
            
            if (Vector3.Distance(player.transform.position, transform.position) <= 0.5f)
            {
                attacking = true;
            }
        }
    }

    public void setAttackEvent()
    {
        attackEvent = true;
    }
    void attack(Vector2 direction)
    {
        
        Vector2 pos = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos + direction * attackOffset, 0.3f);
            
        foreach(Collider2D hit in hits)
        {
            if (hit.gameObject.tag == "Player" && attacked == false)
            {
                player.GetComponent<Player>().takeDamage(attackPower * DifficultyManager.getAttackMultiplier());
                attacked = true;
                enemyAudio.clip = enemyDamageClip;
                enemyAudio.Play();
            }
        }
    }

    

    void setDirection()
    {
        if (sRenderer.flipX)
        {
            direction = Vector2.left;
        }
        else
            direction = Vector2.right;
    }


}
