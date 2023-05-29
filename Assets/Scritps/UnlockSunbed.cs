using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using DG.Tweening;

public class UnlockSunbed : MonoBehaviour
{
    public int itemID;
    public int isUnlocked;
    [SerializeField] private GameObject newSunbed;
    [SerializeField] private TextMeshPro dollarAmount;
    [SerializeField] private float sunbedPrice, sunbedRemainPrice;
    [SerializeField] private float fillAmount;
    public NavMeshSurface buildNavMesh;
    public GameObject dollarPrefab;
    public GameObject player;
    private IEnumerator CoinMaker;
    public bool willBuy;
    float tempSunbedPrice;


    void Start()
    {
        timerIsRunning = true;
        dollarAmount.text = sunbedPrice.ToString();
        sunbedRemainPrice = sunbedPrice;
        isUnlocked = PlayerPrefs.GetInt("isUnlocked" + itemID, 0);
        CoinMaker = CountCoins(GameManager.Instance.transform);

        if (isUnlocked == 1)
        {
            if (itemID < 100)
            {
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x, -4.8f, transform.position.z)
                    , Quaternion.Euler(-90f, 0f, 0f));
            }
            else
            {
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x - 1.7f, -8.8f, transform.position.z + 0.88f)
                    , Quaternion.Euler(0f, 0f, 0f));
            }

            gameObject.SetActive(false);

            //buildNavMesh.BuildNavMesh();
        }
    }

    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.InstantateMoney((int)sunbedRemainPrice);

        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0 && willBuy == false)
        {
            StartCoroutine(CoinMaker);
        }
        else
        {
            StopCoroutine(CoinMaker);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(CoinMaker);
        }
    }

    IEnumerator CountCoins(Transform player)
    {
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        tempSunbedPrice = sunbedRemainPrice;
        for (int counter = 0; counter <= (int)tempSunbedPrice; counter++)
        {
            Debug.Log("2");
            var newCoin = GameManager.Instance.MoneyList[counter];

            if (GameDataManager.Instance.TotalMoney > 0 && sunbedRemainPrice > 0)
            {
                newCoin.transform.position = player.position;
                newCoin.SetActive(true);
                newCoin.transform.DOJump(transform.position, 3f, 1, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    if (newCoin != null)
                        newCoin.transform.localPosition = Vector3.zero;
                });

                SellTheLand();
            }
            else
            {
                Debug.Log("3");
                counter = 0;
            }
            yield return new WaitForSecondsRealtime(0.1f);
            Debug.Log("4");
        }
        GameManager.Instance.MoneyList.Clear();
        Debug.Log("5");
    }

    private void SellTheLand()
    {
        if (GameDataManager.Instance.TotalMoney >= sunbedRemainPrice)
        {
            GameDataManager.Instance.TotalMoney--;
            sunbedRemainPrice--;
            GameDataManager.Instance.SaveData();
            fillAmount = CalculateFill();
        }
        if (GameDataManager.Instance.TotalMoney > 0 && sunbedRemainPrice > 0)
        {
            GameDataManager.Instance.TotalMoney--;
            sunbedRemainPrice--;
            fillAmount = CalculateFill();
        }

        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);

        UIManager.Instance.totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();
        dollarAmount.text = sunbedRemainPrice.ToString();

        if (sunbedRemainPrice == 0)
        {
            isUnlocked = 1;
            PlayerPrefs.SetInt("isUnlocked" + itemID, isUnlocked);

            if (itemID < 100)
            {

                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x, -4.8f, transform.position.z)
                    , Quaternion.Euler(-90f, 0f, 0f));

                desk.transform.localScale = Vector3.zero;
                desk.transform.DOScale(10f, 1f).SetEase(Ease.OutElastic);
            }
            else
            {
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x - 1.7f, -8.8f, transform.position.z + 0.88f)
                    , Quaternion.Euler(0f, 0f, 0f));
                desk.transform.DOScale(0.5f, 0.01f);
                desk.transform.DOScale(1f, 1f).SetEase(Ease.OutElastic);
            }

            //buildNavMesh.BuildNavMesh();
            GameDataManager.Instance.SaveData();
            gameObject.SetActive(false);
        }
        GameDataManager.Instance.SaveData();
    }

    private float CalculateFill()
    {
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }
}

