using UnityEngine;
using UnityEngine.UI;

public class MonsterParams : CharacterParams
{
    public string name;

    public int exp { get; set; }
    public int rewardMoney { get; set; }

    public Image hpBar;

    public override void InitParams()
    {
        isDead = false;

        XMLManager.Instance.LoadMonsterParamsFromXML(name, this);

        InitHpBarSize();
    }

    void InitHpBarSize()
    {
        hpBar.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    protected override void UpdateAfterReceiveAttack()
    {
        base.UpdateAfterReceiveAttack();
        hpBar.rectTransform.localScale = new Vector3((float)curHp / (float)maxHp, 1.0f, 1.0f);
        
    }
}