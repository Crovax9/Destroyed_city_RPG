using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }


    public Text playerName;
    public Text playerMoney;
    public Image playerHPBar;
    public Text gameOver;

    Animator gameOverAnimator;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gameOverAnimator = gameOver.gameObject.GetComponent<Animator>();
        gameOver.enabled = false;
    }

    public void ShowGameOver()
    {
        gameOver.enabled = true;
        gameOverAnimator.SetTrigger("GameOverTrigger");

    }

    public void UpdatePlayerUI(PlayerParams playerParams)
    {
        playerName.text = playerParams.name;
        playerMoney.text = "Coin: " +  playerParams.money.ToString();
        playerHPBar.rectTransform.localScale = new Vector3((float)playerParams.curHp / (float)playerParams.maxHp, 1.0f, 1.0f);
    }
}