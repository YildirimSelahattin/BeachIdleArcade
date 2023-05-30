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
    private IEnumerator Timer;
    public bool willBuy;
    float tempSunbedPrice;
    public float timeFillAmount;

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
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x - 1.7f, -8.8f, transform.position.z + 0.88f)
                    , Quaternion.Euler(0f, 0f, 0f));
            }
            buildNavMesh.BuildNavMesh();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.InstantateMoney((int)sunbedRemainPrice);

        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0 && willBuy == false)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            InvokeRepeating("TimeCounter", 0f, 0.05f);
            //gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            CancelInvoke("TimeCounter");
            StopCoroutine(CoinMaker);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            CancelInvoke("TimeCounter");
            StopCoroutine(CoinMaker);
            timeFillAmount = 0;
        }
    }

    IEnumerator CountCoins(Transform player)
    {
        CancelInvoke("TimeCounter");
        gameObject.transform.GetChild(4).gameObject.SetActive(false);

        tempSunbedPrice = sunbedRemainPrice;
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
            CancelInvoke("TimeCounter");
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

            buildNavMesh.BuildNavMesh();
            GameDataManager.Instance.SaveData();
            gameObject.SetActive(false);
        }
        GameDataManager.Instance.SaveData();
    }

    private float CalculateFill()
    {
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }

    public void TimeCounter()
    {
        Debug.Log("Invokeeee: " + timeFillAmount);
        timeFillAmount += 18;
        gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc1", timeFillAmount);

        if (340 < timeFillAmount)
        {
            StartCoroutine(CoinMaker);
        }

    }
}

