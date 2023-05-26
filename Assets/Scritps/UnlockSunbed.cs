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
    public float timeRemaining = 2;
    public bool willBuy;

    void Start()
    {
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
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x - 1.4f, -8.8f, transform.position.z + 0.8f)
                    , Quaternion.Euler(0f, 0f, 0f));
            }

            gameObject.SetActive(false);

            buildNavMesh.BuildNavMesh();
        }
    }

    
    void Update()
    {
        if (timeRemaining > 0 && willBuy == true)
        {
            timeRemaining -= Time.deltaTime;
            willBuy = true;
        }
        else
        {
            willBuy = false;
        }
        Debug.Log("Time:" + timeRemaining);
        Debug.Log("Booool" + willBuy);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.InstantateMoney((int)sunbedRemainPrice);

        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0 && willBuy == false)
        {
            willBuy = true;
            if(timeRemaining < 0.1f)
                StartCoroutine(CoinMaker);
        }
        else
        {
            StopCoroutine(CoinMaker);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0 && willBuy == false)
        {
            willBuy = true;
            if(timeRemaining < 0.1f)
                StartCoroutine(CoinMaker);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            willBuy = false;
            StopCoroutine(CoinMaker);
            timeRemaining = 2;
        }
    }

    IEnumerator CountCoins(Transform player)
    {
        timeRemaining = 2;
        willBuy = false;
        
        float tempSunbedPrice = sunbedRemainPrice;
        for (int counter = 0; counter <= (int)tempSunbedPrice; counter++)
        {
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
                counter = 0;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
        GameManager.Instance.MoneyList.Clear();
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
                desk.transform.DOScale(10.5f, 1f).SetEase(Ease.OutElastic);
            }
            else
            {
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x - 1.4f, -8.8f, transform.position.z + 0.8f)
                    , Quaternion.Euler(0f, 0f, 0f));

                desk.transform.DOScale(1.08f, 1f).SetEase(Ease.OutElastic);
            }


            gameObject.SetActive(false);

            buildNavMesh.BuildNavMesh();
        }
        GameDataManager.Instance.SaveData();
    }

    private float CalculateFill()
    {
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }
}

