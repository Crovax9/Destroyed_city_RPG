using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    private List<Transform> spawnPosition = new List<Transform>();
    private GameObject[] monsters;

    public GameObject monsterPrefab;
    public int spawnCount = 3;
    public float respawnDelay = 3.0f;

    int deadMonsters = 0;

    private void Start()
    {
        SetSpawnPos();
    }

    void SetSpawnPos()
    {
        foreach (Transform pos in transform)
        {
            if (pos.CompareTag("Respawn"))
            {
                spawnPosition.Add(pos);
            }
        }

        if (spawnCount > spawnPosition.Count)
        {
            spawnCount = spawnPosition.Count;
        }

        monsters = new GameObject[spawnCount];

        InstantiateMonsters();
    }

    void InstantiateMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var mon = Instantiate(monsterPrefab, spawnPosition[i].position, Quaternion.identity) as GameObject;
            mon.GetComponentInChildren<MonsterFSM>().SetRespawnObject(gameObject, i, spawnPosition[i].position);
            mon.SetActive(false);
            monsters[i] = mon;
            GameManager.Instance.AddNewMonsters(mon);
        }
    }

    public void RemoveMonster(int spawnID)
    {
        deadMonsters++;
        monsters[spawnID].SetActive(false);

        if (deadMonsters == monsters.Length)
        {
            StartCoroutine(InitMonsters());
            deadMonsters = 0;
        }
    }
    IEnumerator InitMonsters()
    {
        yield return new WaitForSeconds(respawnDelay);

        GetComponent<SphereCollider>().enabled = true;
    }

    void SpawnMonster()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponentInChildren<MonsterFSM>().SetMonsterStatus();
            monsters[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnMonster();
            GetComponent<SphereCollider>().enabled = false;
        }
    }

}