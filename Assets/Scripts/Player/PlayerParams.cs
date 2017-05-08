using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParams : CharacterParams
{
    public string name { get; set; }
    public int curExp { get; set; }
    public int expToNextLevel { get; set; }
    public int money { get; set; }

    public override void InitParams()
    {
        name = "Player";
        level = 1;
        maxHp = 100;
        curHp = maxHp;
        attackMax = 40;
        attackMin = 30;

        defense = 1;
        curExp = 0;
        expToNextLevel = level * 100;
        money = 0;

        isDead = false;

        UIManager.Instance.UpdatePlayerUI(this);
    }

    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();

        UIManager.Instance.UpdatePlayerUI(this);
    }

    public void AddMoney(int money)
    {
        this.money += money;

        UIManager.Instance.UpdatePlayerUI(this);
    }
}