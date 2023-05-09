using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject MoneyPrefab;
    public List<GameObject> MoneyList = new List<GameObject>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void InstantateMoney(int moneyCount)
    {
        for (int i = 0; i < moneyCount; i++)
        {
            GameObject NewCoin = Instantiate(MoneyPrefab, Vector3.zero, Quaternion.identity);
            MoneyList.Add(NewCoin);
        }
    }
}
