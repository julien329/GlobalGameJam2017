﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mummy : IEnemy {

    Animator anim;

    public override void EnemyDie()
    {
        state = State.DYING;
        action = DyingAction;
        clearAnimParameters();
        anim.SetBool("isDeath", true);
        StartCoroutine("DeathDelay");
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
                    LaunchAttack();
                }
                else if (Vector3.Distance(humanPlayer.transform.position, transform.position) < chaseRange)
                {
                    state = State.FOLLOWING;
                    action = FollowingAction;
                    anim.SetBool("isRun", true);
                    navMeshAgent.speed = chaseSpeed;
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
            case State.ROAMING:
                if (Vector3.Distance(humanPlayer.transform.position, transform.position) < attackRange)
                {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    LaunchAttack();
                    isCooldown = false;
                }
                else if (Vector3.Distance(humanPlayer.transform.position, transform.position) < chaseRange)
                {
                    state = State.FOLLOWING;
                    action = FollowingAction;
                    anim.SetBool("isRun", true);
                    navMeshAgent.speed = chaseSpeed;
                    isCooldown = false;
                }
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

    protected override void RoamingAction()
    {
        if (!navMeshAgent.hasPath && !isCooldown)
        {
            StartCoroutine("Cooldown", idleWait);
        }
    }

    protected override void FollowingAction()
    {
        navMeshAgent.SetDestination(humanPlayer.transform.position);
    }

    protected override void IdleAction()
    {
        //Find a random place to roam to
        if (!navMeshAgent.hasPath && !isCooldown)
        {    
            Vector3 randomDirection = Random.insideUnitCircle * idleRoamRange;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, idleRoamRange, 1);
            Vector3 targetPosition = hit.position;
            navMeshAgent.SetDestination(targetPosition);
            
            anim.SetBool("isWalk", true);
            state = State.ROAMING;
            action = RoamingAction;
        }
    }

    protected override void LaunchAttack()
    {
        anim.SetBool("LowKick", true);
    }

    //Animation for attack is over
    protected void AttackIsOver()
    {
        state = State.COOLDOWN;
        action = CooldownAction;
        navMeshAgent.speed = idleSpeed;
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
        anim.SetBool("isRun", false);
        anim.SetBool("isWalk", false);
        navMeshAgent.ResetPath();
        yield return new WaitForSeconds(time);
        //Cooldown over
        if(gameObject != null && state != State.DYING && isCooldown)
        {
            state = State.IDLE;
            action = IdleAction;
            isCooldown = false;
        }
    }

    //Delay where the corpse is on the ground
    IEnumerator DeathDelay()
    {
        Collider collider = GetComponent<Collider>();
        NavMeshAgent nv = GetComponent<NavMeshAgent>();
        nv.enabled = false;
        collider.enabled = false;

        float waitTime = 3f;
        int increment = 10;

        for (int i = 0; i < increment; i++)
        {
            transform.Translate(0.1f * Vector3.down, Space.World);
            yield return new WaitForSeconds(waitTime / increment);
        }

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

  
  
    void clearAnimParameters()
    {
        anim.SetBool("isWalk", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isAnother", false);
        anim.SetBool("Attack", false);
        anim.SetBool("LowKick", false);
        anim.SetBool("isDeath", false);
        anim.SetBool("isDeath2", false);
        anim.SetBool("HitStrike", false);
        anim.SetBool("isDamage", false);
    }
}
