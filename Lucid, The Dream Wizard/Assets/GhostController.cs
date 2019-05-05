using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // an enumaration for referring to state
    public enum GhostState
    {
        Spawning,
        Idle,
        Chase,
        Attack,
        Dying,
        Inactive
    };

    public LayerMask layer;

    //a reference to current ghostState
    public GhostState m_GhostState;

    //property gamestate - so we can refer to current gamestate using dot notation
    public GhostState State { get { return m_GhostState; } }

    private int count;
    private Animator animator;
    private Animation anim;

    // Use this for initialization
    void Start()
    {
        m_GhostState = GhostState.Spawning;
        animator = GetComponent<Animator>();
        anim = GetComponent<Animation>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        switch (m_GhostState)
        {
            case GhostState.Idle:
                animator.Play("ghost-idle", 0);
            break;
            case GhostState.Spawning:
                animator.Play("spawn", 0);
                count++;
                if (count > 60)//a real hack - need to do this with a timer
                {
                    m_GhostState = GhostState.Idle;
                    count = 0;
                }
                
            break;
            case GhostState.Attack:
              animator.Play("ghost-attack", 0);

            break;

            default:
                animator.Play("ghost-idle", 0);
                break;
        }

       


    }

  
}
