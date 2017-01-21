using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class IEnemy : MonoBehaviour
{
    //NavMeshAgent component in order to find paths
    protected NavMeshAgent navMeshAgent;
    //Human player is kept in memory in order to track his movements and decide on actions
    protected GameObject humanPlayer;

    //Enemy main stats
    public int HP;
    public int Speed;

    //Function delegate to run the actions correctly
    protected delegate void ActionType();
    ActionType action;

    // Use this for initialization
    void Start () {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        action = IdleAction;
	}

    //Used when unit launches an attack
    protected abstract void LaunchAttack();

    //Called when damage is dealt
    public abstract void TakeDamage(int damage);

    //Called when unit must be killed
    public abstract void EnemyDie();

    //Re-evaluates the state of the enemy - Should assign the action delegate handler in case of change of state.
    protected abstract void EvaluateState();

    //Five type of actions depending on the state
    protected abstract void IdleAction();
    protected abstract void FollowingAction();
    protected abstract void FleeingAction();
    protected abstract void AggressiveAction();
    protected abstract void DyingAction();

    //Unit states used for decision making
    protected enum STATE
    {
        IDLE,
        FOLLOWING,
        FLEEING,
        ATTACKING,
        DYING
    }
}
