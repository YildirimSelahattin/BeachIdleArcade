using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WomanSpawnerManager : MonoBehaviour
{
    public static WomanSpawnerManager Instance;
    public GameObject[] womanPrefabs;
    public GameObject[] targetPos;
    public GameObject[] umbrella;
    public Transform spawnArea;
    GameObject DrownedWoman;
    public int tempTargetIndex;
    public List<GameObject> spawnedWomen;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < targetPos.Length; i++)
        {
            if (targetPos[i].gameObject.GetComponent<UnlockSunbed>().isUnlocked == 1)
            {
                int randomNumber = Random.Range(0, 7);
                tempTargetIndex = targetPos[i].gameObject.GetComponent<UnlockSunbed>().itemID;
                Vector3 randomPosition = GetRandomPositionInSpawnArea();
                DrownedWoman = Instantiate(womanPrefabs[randomNumber], randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
                DrownedWoman.GetComponent<PatrolWoman>().targetIndex = tempTargetIndex;
                spawnedWomen.Add(DrownedWoman);
            }
        }
    }

    public void BarIsOpen()
    {
        for (int i = 0; i < umbrella.Length; i++)
        {
            if(WomanSpawnerManager.Instance.umbrella[i].transform.GetChild(5).transform.GetChild(2).gameObject.active == true)
            {
                WomanSpawnerManager.Instance.umbrella[i].transform.GetChild(6).gameObject.SetActive(true);
            }
        }
    }

    Vector3 GetRandomPositionInSpawnArea()
    {
        float minX = spawnArea.position.x - spawnArea.localScale.x / 2f;
        float maxX = spawnArea.position.x + spawnArea.localScale.x / 2f;
        float minZ = spawnArea.position.z - spawnArea.localScale.z / 2f;
        float maxZ = spawnArea.position.z + spawnArea.localScale.z / 2f;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, -5.15f, randomZ);
    }

    public IEnumerator RandomSpawnWoman(int ItemID)
    {
        int randomNumber = Random.Range(0, 7);
        yield return new WaitForSeconds(0.1f);
        Vector3 randomPosition = GetRandomPositionInSpawnArea();
        DrownedWoman = Instantiate(womanPrefabs[randomNumber], randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
        DrownedWoman.GetComponent<PatrolWoman>().targetIndex = ItemID;
        spawnedWomen.Add(DrownedWoman);
    }
}