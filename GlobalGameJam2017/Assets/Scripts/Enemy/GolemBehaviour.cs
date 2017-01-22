using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemBehaviour : IEnemy {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    Animator anim;
    float rageSpeed;


    public override void TakeDamage(int damage)
    {
        anim.SetTrigger("isDamage");
        HP -= damage;
        gameObject.GetComponentInChildren<HealthBar>().UpdateBar(maxHP, HP);

        if (HP < 1)
        {
            EnemyDie();
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Start() {
        base.Init();
        anim = gameObject.GetComponent<Animator>();
        isCooldown = false;
        InvokeRepeating("RageIncrement", 4.0f, 2.0f);
    }

    void Update() {
        if (humanPlayer) {
            EvaluateState();
            action();
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    protected override void EvaluateState() {
        switch (state) {
            case State.IDLE:
                if (Vector3.Distance(humanPlayer.position, transform.position) < attackRange) {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    anim.SetBool("isWalk", false);
                    anim.SetTrigger("HornAttack");
                }
                else if (Vector3.Distance(humanPlayer.position, transform.position) < chaseRange) {
                    state = State.FOLLOWING;
                    action = FollowingAction;
                    anim.SetBool("isWalk", true);
                    navMeshAgent.speed = chaseSpeed;
                }
                break;
            case State.FOLLOWING:
                if (Vector3.Distance(humanPlayer.position, transform.position) < attackRange) {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    LaunchAttack();
                }
                break;
            case State.ROAMING:
                if (Vector3.Distance(humanPlayer.position, transform.position) < attackRange) {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    anim.SetBool("isWalk", false);
                    anim.SetTrigger("HornAttack");
                    isCooldown = false;
                }
                else if (Vector3.Distance(humanPlayer.position, transform.position) < chaseRange) {
                    state = State.FOLLOWING;
                    action = FollowingAction;
                    anim.SetBool("isWalk", true);
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


    public override void EnemyDie() {
        if (state != State.DYING) {
            spawnManager.EnnemyDied();
            state = State.DYING;
            action = DyingAction;
            clearAnimParameters();
            anim.SetBool("isDeath", true);
            StartCoroutine("DeathDelay");
        }
    }
    
    protected override void AggressiveAction() {
        navMeshAgent.SetDestination(humanPlayer.position);
    }


    protected override void CooldownAction() {
        if (!isCooldown) {
            StartCoroutine("Cooldown", attackCooldown);
        }
    }


    protected override void DyingAction() {

    }


    protected override void RoamingAction() {
        if (!navMeshAgent.hasPath && !isCooldown) {
            StartCoroutine("Cooldown", idleWait);
        }
    }


    protected override void FollowingAction() {
        navMeshAgent.SetDestination(humanPlayer.position);
    }


    protected override void IdleAction() {
        //Find a random place to roam to
        if (!navMeshAgent.hasPath && !isCooldown) {
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


    protected override void LaunchAttack() {
        anim.SetBool("isWalk", false);
        anim.SetTrigger("HornAttack");
        AttackIsOver();

        Invoke("KnockBack", 0.40f);
    }


    //Animation for attack is over
    protected void AttackIsOver() {
        state = State.COOLDOWN;
        action = CooldownAction;
        navMeshAgent.speed = idleSpeed;
    }


    //Animation for death is over
    protected void DeathIsOver() {
        StartCoroutine("DeathDelay");
    }


    //Cooldown between attacks
    IEnumerator Cooldown(float time) {
        isCooldown = true;
        anim.SetBool("isWalk", false);
        navMeshAgent.ResetPath();
        yield return new WaitForSeconds(time);
        //Cooldown over
        if (gameObject != null && state != State.DYING && isCooldown) {
            state = State.IDLE;
            action = IdleAction;
            isCooldown = false;
        }
    }


    //Delay where the corpse is on the ground
    IEnumerator DeathDelay() {
        navMeshAgent.enabled = false;
        collider.enabled = false;

        float waitTime = 3f;
        int increment = 10;

        for (int i = 0; i < increment; i++){
            transform.Translate(0.01f * Vector3.down,Space.World);
            yield return new WaitForSeconds(waitTime / increment);
        }


        Destroy(gameObject);
    }


    public override void ShockwaveHit(float distance) {
        AttackIsOver();
        StartCoroutine("Cooldown", (6/distance));
    }


    void KnockBack() {
        playerCombat.ApplyImpulse((gameObject.transform.forward + new Vector3(0.0f, 0.5f, 0.0f)), 15.0f);
        playerCombat.ApplyDamage(25);
    }


    //Increments speed if he has been chassing a while
    void RageIncrement() {        
        if(state == State.FOLLOWING) {
            if (rageSpeed < 6.0f) {
                rageSpeed += 1.0f;
            }
        }
        else {
            rageSpeed = 0.0f;
        }
        navMeshAgent.speed = chaseSpeed + rageSpeed;
    }
    

    void clearAnimParameters() {
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
