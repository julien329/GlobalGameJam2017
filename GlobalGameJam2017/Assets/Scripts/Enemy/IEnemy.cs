using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class IEnemy : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    //AI element
    [SerializeField]
    protected float idleSpeed;
    [SerializeField]
    protected float idleWait;
    [SerializeField]
    protected float idleRoamRange;
    [SerializeField]
    protected float chaseSpeed;
    [SerializeField]
    protected float chaseRange;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float attackCooldown;
    protected bool isCooldown;

    //Enemy main stats
    protected int maxHP;
    public int HP;
    public int Speed;

    //Function delegate to run the actions correctly
    protected delegate void ActionType();
    protected State state;
    protected ActionType action;
    protected SpawnManager spawnManager;
    protected PlayerCombat playerCombat;
    protected Transform humanPlayer;
    protected NavMeshAgent navMeshAgent;
    protected Collider collider;


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    //Standard init for all ennemies, must be called from derived class
    protected void Init() {
        maxHP = HP;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        action = IdleAction;
        state = State.IDLE;
        navMeshAgent.speed = idleSpeed;
        humanPlayer = GameObject.Find("Player").transform;
        spawnManager = GameObject.Find("Map").GetComponent<SpawnManager>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        collider = GetComponent<Collider>();
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
    protected abstract void RoamingAction();
    protected abstract void AggressiveAction();
    protected abstract void CooldownAction();
    protected abstract void DyingAction();

    //
    public abstract void ShockwaveHit(float distance);

    //Unit states used for decision making
    protected enum State {
        IDLE,
        FOLLOWING,
        ROAMING,
        ATTACKING,
        COOLDOWN,
        DYING
    }

   
}
