using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject MoneyPrefab;
    public List<GameObject> MoneyList = new List<GameObject>();
    public GameObject[] newSunbedArea;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        foreach (GameObject sunbed in newSunbedArea)
        {
            Material radialFillCloneMat = new Material(Shader.Find("Custom/RadialFill"));
            radialFillCloneMat.SetFloat("_Angle", 270);
            radialFillCloneMat.SetFloat("_Arc1", 0);
            radialFillCloneMat.SetFloat("_Arc2", 360);
            sunbed.transform.GetChild(1).GetComponent<SpriteRenderer>().material = radialFillCloneMat;
        }
    }

    public void InstantateMoney(int moneyCount)
    {
        if (MoneyList.Count < moneyCount)
        {
            for (int i = 0; i <= moneyCount; i++)
            {
                GameObject NewCoin = Instantiate(MoneyPrefab, Vector3.zero, Quaternion.identity);
                MoneyList.Add(NewCoin);
            }
        }
    }
}
