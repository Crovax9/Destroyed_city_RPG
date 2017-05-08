using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFSM : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        AttackWait,
        Dead
    }

    private State currentState = State.Idle;

    private Vector3 curTargetPos;
    private GameObject curEnemy;

    private float attackDelay = 2.0f;
    private float attackTimer = 0.0f;
    private float attackDistance = 5.5f;
    
    private float rotAnglePerSecond = 360.0f;
    private float moveSpeed = 2.0f;


    private PlayerAnim myAnim;

    private PlayerParams myParams;
    MonsterParams curEnemyParams;

    private void Start()
    {
        myAnim = GetComponent<PlayerAnim>();
        myParams = GetComponent<PlayerParams>();
        myParams.InitParams();
        myParams.enemyDeadEvent.AddListener(CallDeadEvent);

        ChangeState(State.Idle, PlayerAnim.ANI_IDLE);
    }

    public void AttackCalculate()
    {
        if (curEnemy == null)
        {
            return;
        }
        curEnemy.GetComponent<MonsterFSM>().PlayHitEffect();

        int attackPower = myParams.GetRandomAttack();
        curEnemyParams.SetEnemyAttack(attackPower);
        SoundManager.Instance.PlaySound(SoundManager.SOUNDLIST.PLAYER_SHOOT);
    }

    public void AttackEnemy(GameObject enemy)
    {
        if (curEnemy != null && curEnemy == enemy)
        {
            return;
        }
        curEnemyParams = enemy.GetComponent<MonsterParams>();
        if (curEnemyParams.isDead == false)
        {
            curEnemy = enemy;
            curTargetPos = curEnemy.transform.position;
            GameManager.Instance.ChangeCurrentTarget(curEnemy);

            ChangeState(State.Move, PlayerAnim.ANI_WALK);
        }
        else
        {
            curEnemyParams = null;
        }
        
    }

    void ChangeState(State newState, int aniNumber)
    {
        if (currentState == newState)
        {
            return;
        }

        myAnim.ChangeAnim(aniNumber);
        currentState = newState;
    }

    void Update()
    {
        UpdateState();
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleState();
                break;

            case State.Move:
                MoveState();
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

            default:

                break;
        }
    }

    void IdleState()
    {

    }
    void MoveState()
    {
        TurnToDestination();
        MoveToDestination();
    }

    void AttackState()
    {
        attackTimer = 0.0f;

        transform.LookAt(curTargetPos);
        ChangeState(State.AttackWait, PlayerAnim.ANI_ATTACKIDLE);
    }

    void AttackWaitState()
    {
        if (attackTimer > attackDelay)
        {
            ChangeState(State.Attack, PlayerAnim.ANI_ATTACK);
        }

        attackTimer += Time.deltaTime;
    }

    void DeadState()
    {

    }

    public void CurrentEnemyDead()
    {
        ChangeState(State.Idle, PlayerAnim.ANI_IDLE);

        curEnemy = null;
    }

    public void MoveTo(Vector3 targetPos)
    {
        if (currentState == State.Dead)
        {
            return;
        }
        curEnemy = null;
        curTargetPos = targetPos;
        ChangeState(State.Move, PlayerAnim.ANI_WALK);
    }

    void TurnToDestination()
    {
        Quaternion lookRotation = Quaternion.LookRotation(curTargetPos - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotAnglePerSecond);
    }

    void CallDeadEvent()
    {
        ChangeState(State.Dead, PlayerAnim.ANI_DEAD);
        UIManager.Instance.ShowGameOver();
        StartCoroutine(MoveScene());
    }

    IEnumerator MoveScene()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("OpeningScene");
    }

    void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, curTargetPos, Time.deltaTime * moveSpeed);

        if (curEnemy == null)
        {
            if (Vector3.Distance(transform.position, curTargetPos) < 1.0f)
            {
                ChangeState(State.Idle, PlayerAnim.ANI_IDLE);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, curTargetPos) < attackDistance)
            {
                ChangeState(State.Attack, PlayerAnim.ANI_ATTACK);
            }
        }
        
    }
}

