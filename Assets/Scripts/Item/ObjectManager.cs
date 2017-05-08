using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager _instance;
    public static ObjectManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject coinPrefab;
    public int initialCoins = 30;

    private List<GameObject> coins = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
        InstantiateCoin();
    }

    private void InstantiateCoin()
    {
        for (int i = 0; i < initialCoins; i++)
        {
            var tempCoin = Instantiate(coinPrefab) as GameObject;
            tempCoin.transform.parent = transform;
            tempCoin.SetActive(false);
            coins.Add(tempCoin);
        }
    }

    public void DropCoinToPosition(Vector3 pos, int coinValue)
    {
        GameObject reusedCoin = null;

        for (int i = 0; i < coins.Count; i++)
        {
            if (coins[i].activeSelf == false)
            {
                reusedCoin = coins[i];
                break;
            }
        }

        if (reusedCoin == null)
        {
            var newCoin = Instantiate(coinPrefab) as GameObject;
            coins.Add(newCoin);
            reusedCoin = newCoin;
        }

        reusedCoin.SetActive(true);
        reusedCoin.GetComponent<Coin>().SetCoinValue(coinValue);
        reusedCoin.transform.position = new Vector3(pos.x, reusedCoin.transform.position.y, pos.z);
    }
}