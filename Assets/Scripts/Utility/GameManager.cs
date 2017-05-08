using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    List<GameObject> monsters = new List<GameObject>();



    private void Awake()
    {
        _instance = this;
    }



    public void AddNewMonsters(GameObject mon)
    {
        bool sameExist = false;

        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] == mon)
            {
                sameExist = true;
                Debug.LogError("Same monster has been already added");
                break;
            }
        }

        if (sameExist == false)
        {
            monsters.Add(mon);
        }
    }

    public void RemoveMonster(GameObject mon)
    {
        foreach (GameObject monster in monsters)
        {
            if (monster == mon)
            {
                monsters.Remove(monster);
                break;
            }
        }
    }

    public void ChangeCurrentTarget(GameObject mon)
    {
        DeselectAllMonsters();
        mon.GetComponentInChildren<MonsterFSM>().ShowSelection();
    }

    public void DeselectAllMonsters()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].GetComponentInChildren<MonsterFSM>().HideSelection();
        }
    }
}