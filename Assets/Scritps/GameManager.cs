using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject MoneyPrefab;
    public List<GameObject> MoneyList = new List<GameObject>();
    public GameObject moneyParent;
    public NavMeshSurface buildNavMesh;

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
        StartCoroutine(BuildNav());
    }

    IEnumerator BuildNav()
    {
        yield return new WaitForSeconds(1);
        buildNavMesh.BuildNavMesh();
    }

    public void InstantateMoney(int moneyCount)
    {
        if (MoneyList.Count < moneyCount)
        {
            for (int i = 0; i <= moneyCount; i++)
            {
                GameObject NewCoin = Instantiate(MoneyPrefab, moneyParent.transform);
                MoneyList.Add(NewCoin);
            }
        }
    }
}
