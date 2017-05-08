using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 180.0f;
    [System.NonSerialized]
    public int money = 100;

    private void Update()
    {
        transform.Rotate(0.0f, rotateSpeed * Time.deltaTime, 0.0f);
    }

    public void SetCoinValue(int money)
    {
        this.money = money;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerParams>().AddMoney(money);
            //CoinSound
            HideCoinObject();
        }
    }

    public void HideCoinObject()
    {
        gameObject.SetActive(false);
    }
}