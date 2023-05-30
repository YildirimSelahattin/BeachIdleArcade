using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeguardController : MonoBehaviour
{
    public static LifeguardController Instance;
    public GameObject objectPrefab;
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
        Debug.Log("dasdasdasdasdasd");
        Pointer.Instance.img.enabled = false;
        Pointer.Instance.img.material.color = Color.red;
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
        DrownedWoman = Instantiate(objectPrefab, randomPosition, Quaternion.Euler(new Vector3(-70, 180, 0)));
        Debug.Log("Intantiate Drown Woman");
        Pointer.Instance.img.enabled = true;
        Pointer.Instance.target = DrownedWoman.transform;
    }
}