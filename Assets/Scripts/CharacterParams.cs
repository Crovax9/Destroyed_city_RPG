using UnityEngine;
using UnityEngine.Events;

//CharacterParamiter Script

public class CharacterParams : MonoBehaviour
{
    public int level { get; set; }
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int attackMin { get; set; }
    public int attackMax { get; set; }
    public int defense { get; set; }

    public bool isDead { get; set; }

    [System.NonSerialized]
    public UnityEvent enemyDeadEvent = new UnityEvent();

    private void Start()
    {
        InitParams();
    }

    public virtual void InitParams()
    {

    }

    public int GetRandomAttack()
    {
        int randAttack = Random.Range(attackMin, attackMax + 1);

        return randAttack;
    }

    public void SetEnemyAttack(int enemyAttack)
    {
        curHp -= enemyAttack;
        UpdateAfterReceiveAttack();
    }

    protected virtual void UpdateAfterReceiveAttack()
    {
        print(name + "'s HP" + curHp);

        if (curHp < 0)
        {
            curHp = 0;
            isDead = true;
            enemyDeadEvent.Invoke();
        }
    }
}