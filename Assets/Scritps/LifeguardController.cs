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
        }
    }

    void Start()
    {
        Pointer.Instance.img.enabled = false;
        Pointer.Instance.img.material.color = Color.red;

        StartCoroutine(ResponseToRequests(30));
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
        Debug.Log("Drown");

        if (PlayerManager.Instance.reqCream == true)
        {
            StartCoroutine(AgainRequest());
        }
        else
        {
            Debug.Log("Boguluyoooooom");
            PlayerManager.Instance.reqDrown = true;
            yield return new WaitForSeconds(spawnTime);
            Vector3 randomPosition = GetRandomPositionInSpawnArea();
            DrownedWoman = Instantiate(objectPrefab, randomPosition, Quaternion.Euler(new Vector3(-70, 180, 0)));
            
            Pointer.Instance.img.enabled = true;
            Pointer.Instance.img.material.color = Color.red;
            Pointer.Instance.target = DrownedWoman.transform;

            StartCoroutine(AgainRequest());
        }
    }

    IEnumerator ResponseToRequests(int time)
    {
        yield return new WaitForSecondsRealtime(8);
        Debug.Log("Cream " + WomanSpawnerManager.Instance.spawnedWomen.Count);
        int tempIndex = Random.Range(0, WomanSpawnerManager.Instance.spawnedWomen.Count);

        if (WomanSpawnerManager.Instance.spawnedWomen[tempIndex].gameObject.GetComponent<PatrolWoman>().isSwim == true)
        {
            Debug.Log("IF - Index: " + WomanSpawnerManager.Instance.spawnedWomen[tempIndex].gameObject.GetComponent<PatrolWoman>().targetIndex
             + "Swim: " + WomanSpawnerManager.Instance.spawnedWomen[tempIndex].gameObject.GetComponent<PatrolWoman>().isSwim);
            StartCoroutine(AgainRequest());
        }
        else
        {
            Debug.Log("ELSE - Index: " + WomanSpawnerManager.Instance.spawnedWomen[tempIndex].gameObject.GetComponent<PatrolWoman>().targetIndex
             + "Swim: " + WomanSpawnerManager.Instance.spawnedWomen[tempIndex].gameObject.GetComponent<PatrolWoman>().isSwim);

            PlayerManager.Instance.reqCream = true;
            yield return new WaitForSecondsRealtime(time);
            WomanSpawnerManager.Instance.spawnedWomen[tempIndex].transform.GetChild(2).GetComponent<BoxCollider>().enabled = true;
            WomanSpawnerManager.Instance.spawnedWomen[tempIndex].transform.GetChild(3).gameObject.SetActive(true);
            Pointer.Instance.img.enabled = true;
            Pointer.Instance.target = WomanSpawnerManager.Instance.spawnedWomen[tempIndex].transform;
            Pointer.Instance.img.material.color = Color.blue;

            StartCoroutine(AgainRequest());
        }
    }

    public IEnumerator AgainRequest()
    {
        Debug.Log("Again");
        Debug.Log("reqDrown : " + PlayerManager.Instance.reqDrown + "--- reqCream : " + PlayerManager.Instance.reqCream);
        int rnd = Random.Range(0, 2);

        if (PlayerManager.Instance.reqDrown == false && PlayerManager.Instance.reqCream == false && rnd == 1)
        {
            Debug.Log(" > Drown");
            StartCoroutine(RandomSpawnDrownedWoman(41));
        }
        else if (PlayerManager.Instance.reqDrown == false && PlayerManager.Instance.reqCream == false && rnd == 0 || rnd == 1)
        {
            Debug.Log(" > Cream");
            StartCoroutine(ResponseToRequests(29));
        }
        else
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(AgainRequest());
        }
    }
}