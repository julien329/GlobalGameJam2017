using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : IEnemy {

    Animator anim;

    

    public override void EnemyDie()
    {
        state = State.DYING;
        action = DyingAction;
        anim.SetBool("isDeath", true);
    }

    public override void TakeDamage(int damage)
    {
        anim.SetBool("isDamage", true);
        HP -= damage;
        if(HP < 1)
        {
            EnemyDie();
        }
    }

    protected override void AggressiveAction()
    {
        navMeshAgent.SetDestination(humanPlayer.transform.position);
    }

    protected override void CooldownAction()
    {
        if(!isCooldown)
        {
            StartCoroutine("Cooldown", attackCooldown);
        }
    }

    protected override void DyingAction()
    {

    }

    protected override void EvaluateState()
    {
        switch (state)
        {
            case State.IDLE:
                if (Vector3.Distance(humanPlayer.transform.position, transform.position) < attackRange)
                {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    anim.SetBool("isRun", false);
                    anim.SetBool("LowKick", true);
                }
                else if (Vector3.Distance(humanPlayer.transform.position, transform.position) < chaseRange)
                {
                    state = State.FOLLOWING;
                    action = FollowingAction;
                    anim.SetBool("isRun", true);
                }
                break;
            case State.FOLLOWING:
                if (Vector3.Distance(humanPlayer.transform.position, transform.position) < attackRange)
                {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    LaunchAttack();
                }
                break;
            case State.FLEEING:
                break;
            case State.ATTACKING:
                break;
            case State.COOLDOWN:
                break;
            case State.DYING:
                break;
            default:
                break;
        }
    }

    protected override void FleeingAction()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    protected override void FollowingAction()
    {
        navMeshAgent.SetDestination(humanPlayer.transform.position);
    }

    protected override void IdleAction()
    {
        
    }

    protected override void LaunchAttack()
    {
        anim.SetBool("isRun", false);
        anim.SetBool("LowKick", true);
    }

    //Animation for attack is over
    protected void AttackIsOver()
    {
        state = State.COOLDOWN;
        action = CooldownAction;
    }

    //Animation for death is over
    protected void DeathIsOver()
    {
        StartCoroutine("DeathDelay");
    }

    //Cooldown between attacks
    IEnumerator Cooldown(float time)
    {
        isCooldown = true;
        TakeDamage(1);
        yield return new WaitForSeconds(time);
        //Cooldown over
        if(gameObject != null && state != State.DYING)
        {
            state = State.IDLE;
            isCooldown = false;
        }
    }

    //Delay where the corpse is on the ground
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        base.Init();
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        isCooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
        EvaluateState();
        action();
    }
}
