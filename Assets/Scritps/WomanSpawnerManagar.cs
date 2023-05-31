using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanSpawnerManagar : MonoBehaviour
{
    public static WomanSpawnerManagar Instance;
    public GameObject[] womanPrefabs;
    public Transform spawnArea;
    GameObject DrownedWoman;

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
        StartCoroutine(RandomSpawnDrownedWoman(10));
    }

    Vector3 GetRandomPositionInSpawnArea()
    {
        float minX = spawnArea.position.x - spawnArea.localScale.x / 2f;
        float maxX = spawnArea.position.x + spawnArea.localScale.x / 2f;
        float minY = spawnArea.position.y - spawnArea.localScale.y / 2f;
        float maxY = spawnArea.position.y + spawnArea.localScale.y / 2f;
        float minZ = spawnArea.position.z - spawnArea.localScale.z / 2f;
        float maxZ = spawnArea.position.z + spawnArea.localScale.z / 2f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        return new Vector3(randomX, randomY, randomZ);
    }

    public IEnumerator RandomSpawnDrownedWoman(int spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);
        Vector3 randomPosition = GetRandomPositionInSpawnArea();
        DrownedWoman = Instantiate(womanPrefabs[0], randomPosition, Quaternion.Euler(new Vector3(-70, 180, 0)));
        Pointer.Instance.img.enabled = true;
        Pointer.Instance.target = DrownedWoman.transform;
    }
}