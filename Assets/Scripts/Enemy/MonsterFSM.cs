using System.Collections;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack,
        AttackWait,
        Dead,
        NoState
    }

    private State currentState = State.Idle;

    private MonsterAnim enemyAnim;
    private Transform target;

    private MonsterParams myParamiters;
    private PlayerParams playerParams;

    private CharacterController controller;

    private float chaseDistance = 15.0f;
    private float attackDistance = 2.5f;
    private float reChaseDistance = 2.5f;

    private float rotAnglePerSecond = 360.0f;
    private float moveSpeed = 2.5f;

    private float attackDelay = 2.0f;
    private float attackTimer = 0.0f;

    public ParticleSystem hitEffect;
    public GameObject selectMark;

    private GameObject respawnObject;
    public int spawnID { get; set; }
    private Vector3 originPos;

    private void Start()
    {
        enemyAnim = GetComponent<MonsterAnim>();
        ChangeState(State.Idle, MonsterAnim.ANI_IDLE);

        myParamiters = GetComponent<MonsterParams>();
        myParamiters.enemyDeadEvent.RemoveAllListeners();
        myParamiters.enemyDeadEvent.AddListener(CallDeadEvent);

        controller = GetComponent<CharacterController>();

        playerParams = GameObject.FindWithTag("Player").GetComponent<PlayerParams>();
        target = GameObject.FindWithTag("Player").transform;

        hitEffect.Stop();
        HideSelection();
    }

    private void Update()
    {
        UpdateState();
    }

    public void SetMonsterStatus()
    {
        transform.position = originPos;
        GetComponent<MonsterParams>().InitParams();
        GetComponent<BoxCollider>().enabled = true; ;
    }

    public void PlayHitEffect()
    {
        hitEffect.Play();
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;

            case State.Chase:
                ChaseState();
                break;

            case State.Attack:
                AttackState();
                break;

            case State.AttackWait:
                AttackWaitState();
                break;

            case State.Dead:
                DeadState();
                break;

            case State.NoState:
                NoState();
                break;

            default:

                break;
        }

    }

    public void SetRespawnObject(GameObject respawnObject, int spawnID, Vector3 originPos)
    {
        this.respawnObject = respawnObject;
        this.spawnID = spawnID;
        this.originPos = originPos;
    }

    void CallDeadEvent()
    {
        ChangeState(State.Dead, MonsterAnim.ANI_DEAD);
        ObjectManager.Instance.DropCoinToPosition(transform.position, myParamiters.rewardMoney);
        target.gameObject.SendMessage("CurrentEnemyDead");

        //DeadSound
        StartCoroutine(RemoveMeFormWorld());
    }

    IEnumerator RemoveMeFormWorld()
    {
        yield return new WaitForSeconds(1.0f);
        ChangeState(State.Idle, MonsterAnim.ANI_IDLE);
        respawnObject.GetComponent<RespawnObject>().RemoveMonster(spawnID);

    }

    public void HideSelection()
    {
        selectMark.SetActive(false);
    }
    public void ShowSelection()
    {
        selectMark.SetActive(true);
    }

    void IdleState()
    {
        if (GetDistanceFromPlayer() < chaseDistance)
        {
            ChangeState(State.Chase, MonsterAnim.ANI_WALK);
        }
    }

    void ChaseState()
    {
        if (GetDistanceFromPlayer() < attackDistance)
        {
            ChangeState(State.Attack, MonsterAnim.ANI_ATTACK);
        }
        else
        {
            ChangeState(State.Chase, MonsterAnim.ANI_WALK);
            TurnToDestination();
            MoveToDestination();
        }
    }

    void AttackState()
    {
        if (playerParams.isDead == true)
        {
            ChangeState(State.NoState, MonsterAnim.ANI_IDLE);
        }

        if (GetDistanceFromPlayer() > reChaseDistance)
        {
            attackTimer = 0.0f;
            ChangeState(State.Chase, MonsterAnim.ANI_WALK);
        }

        else
        {
            attackTimer = 0.0f;

            transform.LookAt(target.position);
            ChangeState(State.AttackWait, MonsterAnim.ANI_ATTACKIDLE);
        }
    }

    void AttackWaitState()
    {
        if (attackTimer > attackDelay)
        {
            ChangeState(State.Attack, MonsterAnim.ANI_ATTACK);
        }

        attackTimer += Time.deltaTime;
    }

    void DeadState()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public void AttackCalculate()
    {
        playerParams.SetEnemyAttack(myParamiters.GetRandomAttack());
    }

    void NoState()
    {
        target = null;
        ChangeState(State.NoState, MonsterAnim.ANI_IDLE);
    }

    void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotAnglePerSecond);
    }

    void MoveToDestination()
    {
        controller.Move(transform.forward * moveSpeed * Time.deltaTime);
    }

    float GetDistanceFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance;
    }

    void ChangeState(State newState, int aniNumber)
    {
        if (currentState == newState)
        {
            return;
        }

        enemyAnim.ChangeAnimation(aniNumber);
        currentState = newState;
    }
}