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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(RandomSpawnWoman(3));
        StartCoroutine(RandomSpawnWoman2(10));
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

    public IEnumerator RandomSpawnWoman(int spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);
        Vector3 randomPosition = GetRandomPositionInSpawnArea();
        DrownedWoman = Instantiate(womanPrefabs[0], randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    public IEnumerator RandomSpawnWoman2(int spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);
        Vector3 randomPosition = GetRandomPositionInSpawnArea();
        DrownedWoman = Instantiate(womanPrefabs[1], randomPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
}