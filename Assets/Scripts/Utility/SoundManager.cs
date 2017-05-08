using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public enum SOUNDLIST
    {
        PLAYER_SHOOT,
        PLAYER_DIE,
        PLAYER_WALK,
        ENEMY_DIE,
        ENEMY_ATTACK,
        NULL,
    }

    private static SoundManager _instance;
    AudioSource myAudio;

    public AudioClip[] audioClip;

    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlaySound(SOUNDLIST Sound)
    {
        
        switch (Sound)
        {
            case SOUNDLIST.PLAYER_SHOOT:
                myAudio.PlayOneShot(audioClip[0]);
                break;
        }
    }

}