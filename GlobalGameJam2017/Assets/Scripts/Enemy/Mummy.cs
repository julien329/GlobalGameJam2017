using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : IEnemy {

    Animator anim;

    public override void EnemyDie()
    {
        throw new NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    protected override void AggressiveAction()
    {
        navMeshAgent.SetDestination(humanPlayer.transform.position);
    }

    protected override void DyingAction()
    {
        throw new NotImplementedException();
    }

    protected override void EvaluateState()
    {
        switch (state)
        {
            case State.IDLE:
                if(Vector3.Distance(humanPlayer.transform.position, transform.position) < 8.0f)
                {
                    state = State.FOLLOWING;
                    action = FollowingAction;
                    anim.SetBool("isRun", true);
                }
                break;
            case State.FOLLOWING:
                if (Vector3.Distance(humanPlayer.transform.position, transform.position) < 3.0f)
                {
                    state = State.ATTACKING;
                    action = AggressiveAction;
                    anim.SetBool("isRun", false);
                    anim.SetBool("LowKick", true);
                    
                }
                break;
            case State.FLEEING:
                break;
            case State.ATTACKING:
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
        throw new NotImplementedException();
    }

    protected void AttackIsOver()
    {
        state = State.FLEEING;
        action = FleeingAction;
        Debug.Log("Animation is over");
    }

    // Use this for initialization
    void Start () {
        base.Init();
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        EvaluateState();
        action();
    }

    //Animation d'attaque
    private IEnumerator WaitForAnimationAttack(Animation animation)
    {
        Debug.Log("Animation is playing");

        do
        {
            yield return null;
        } while (animation.isPlaying);
        state = State.FLEEING;
        Debug.Log("Animation is over");
    }
}
