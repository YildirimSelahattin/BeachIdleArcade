using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRequestsManager : MonoBehaviour
{
    /*
    public bool requestDrink = false;
    public bool requestMassage = false;
    public int requestNumber;

    void Start()
    {
        requestNumber = RandomRequest();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0)
        {
            StartCoroutine(ResponseToRequests());
        }
        else
        {
            StopCoroutine(ResponseToRequests());
        }
    }

    IEnumerator ResponseToRequests()
    {
        for (int timeCounter = 0; timeCounter <= 10; timeCounter++)
        {
            Serve();
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return new WaitForSecondsRealtime(40f);
        requestNumber = RandomRequest();
    }

    public void Serve()
    {
        
    }

    public int RandomRequest()
    {
        return Random.Range(0,2);
    }
    */

    public GameObject request;
    public static CustomerRequestsManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        StartCoroutine(ResponseToRequests());
    }

    IEnumerator ResponseToRequests()
    {
        yield return new WaitForSecondsRealtime(40f);
        Pointer.Instance.img.enabled = true;
        Pointer.Instance.target = gameObject.transform;
        request.SetActive(true);
        Pointer.Instance.img.material.color = Color.blue;
    }
}
