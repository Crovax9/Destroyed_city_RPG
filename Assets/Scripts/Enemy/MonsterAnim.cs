using UnityEngine;

public class MonsterAnim : MonoBehaviour
{
    public const int ANI_IDLE = 0;
    public const int ANI_WALK = 1;
    public const int ANI_ATTACK = 2;
    public const int ANI_ATTACKIDLE = 3;
    public const int ANI_DEAD = 4;

    private Animator anim;

    private void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimation(int aniNumber)
    {
        anim.SetInteger("aniFlag", aniNumber);
    }
}