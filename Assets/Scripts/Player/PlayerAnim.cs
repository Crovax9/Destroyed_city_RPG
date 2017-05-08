using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public const int ANI_IDLE = 0;
    public const int ANI_WALK = 1;
    public const int ANI_ATTACK = 2;
    public const int ANI_ATTACKIDLE = 3;
    public const int ANI_DEAD = 4;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnim(int aniNumber)
    {
        anim.SetInteger("animFlag", aniNumber);
    }
}