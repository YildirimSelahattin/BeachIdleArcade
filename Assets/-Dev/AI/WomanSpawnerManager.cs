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
    public Transform spawnArea;
    GameObject DrownedWoman;
    public int tempTargetIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i <= targetPos.Length; i++)
        {
            if (targetPos[i].gameObject.GetComponent<UnlockSunbed>().isUnlocked == 1)
            {
                tempTargetIndex = targetPos[i].gameObject.GetComponent<UnlockSunbed>().itemID;
                Debug.Log("WomanSpawnerIndex: " + tempTargetIndex);
                Vector3 randomPosition = GetRandomPositionInSpawnArea();
                DrownedWoman = Instantiate(womanPrefabs[0], randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
                DrownedWoman.GetComponent<PatrolWoman>().targetIndex = tempTargetIndex;
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
        yield return new WaitForSeconds(0.1f);
        tempTargetIndex = ItemID;
        Vector3 randomPosition = GetRandomPositionInSpawnArea();
        DrownedWoman = Instantiate(womanPrefabs[0], randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}