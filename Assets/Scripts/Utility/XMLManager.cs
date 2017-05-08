using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using UnityEngine;

public class XMLManager : MonoBehaviour
{
    private static XMLManager _instance;
    public static XMLManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public TextAsset monsterXml;

    struct MonsterParamiters
    {
        public string name;
        public int level;
        public int maxHp;
        public int attackMin;
        public int attackMax;
        public int defense;
        public int exp;
        public int rewardMoney;
    }

    Dictionary<string, MonsterParamiters> dicMonsters = new Dictionary<string, MonsterParamiters>();

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        MakeMonsterXML();   
    }

    void MakeMonsterXML()
    {
        XmlDocument monsterXMLDoc = new XmlDocument();
        monsterXMLDoc.LoadXml(monsterXml.text);

        XmlNodeList monsterNodeList = monsterXMLDoc.GetElementsByTagName("row");

        foreach (XmlNode monsterNode in monsterNodeList)
        {
            MonsterParamiters monParams = new MonsterParamiters();
            foreach (XmlNode childNode in monsterNode.ChildNodes)
            {
                if (childNode.Name == "name")
                {
                    monParams.name = childNode.InnerText;
                }

                if (childNode.Name == "level")
                {
                    monParams.level = Int16.Parse(childNode.InnerText);
                }

                if (childNode.Name == "maxHp")
                {
                    monParams.maxHp = Int16.Parse(childNode.InnerText);
                }

                if (childNode.Name == "attackMin")
                {
                    monParams.attackMin = Int16.Parse(childNode.InnerText);
                }

                if (childNode.Name == "attackMax")
                {
                    monParams.attackMax = Int16.Parse(childNode.InnerText);
                }

                if (childNode.Name == "defense")
                {
                    monParams.defense = Int16.Parse(childNode.InnerText);
                }

                if (childNode.Name == "exp")
                {
                    monParams.exp = Int16.Parse(childNode.InnerText);
                }

                if (childNode.Name == "rewardMoney")
                {
                    monParams.rewardMoney = Int16.Parse(childNode.InnerText); 
                }

                print(childNode.Name + " : " + childNode.InnerText);
            }
            dicMonsters[monParams.name] = monParams;
        }
    }

    public void LoadMonsterParamsFromXML(string monName, MonsterParams mParams)
    {
        mParams.level = dicMonsters[monName].level;
        mParams.curHp = mParams.maxHp = dicMonsters[monName].maxHp;
        mParams.attackMin = dicMonsters[monName].attackMin;
        mParams.attackMax = dicMonsters[monName].attackMax;
        mParams.defense = dicMonsters[monName].defense;
        mParams.exp = dicMonsters[monName].exp;
        mParams.rewardMoney = dicMonsters[monName].rewardMoney;
    }
}