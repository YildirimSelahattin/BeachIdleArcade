using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using DG.Tweening;

public class UnlockSunbed : MonoBehaviour
{
    public static UnlockSunbed Instance;
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
    public float timer;
    public bool inside = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        dollarAmount.text = sunbedPrice.ToString();
        sunbedRemainPrice = sunbedPrice;
        isUnlocked = PlayerPrefs.GetInt("isUnlocked" + itemID, 0);
        CoinMaker = CountCoins(GameManager.Instance.transform);
        Timer = TimeCounter(20);

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

            StartCoroutine(CloseArea());

            buildNavMesh.BuildNavMesh();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.InstantateMoney((int)sunbedRemainPrice);

        if (other.CompareTag("Player"))
        {
            //if (inside == false) inside = true;

            //if (GameDataManager.Instance.TotalMoney > 0 && timer > 2)
            if (GameDataManager.Instance.TotalMoney > 0 && isUnlocked == 0)
            {
                StartCoroutine(CoinMaker);
                timer = 0;
            }
            else
            {
                StopCoroutine(CoinMaker);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(CoinMaker);
            inside = false;
            timer = 0;
        }
    }

    IEnumerator CountCoins(Transform player)
    {
        yield return new WaitForSeconds(1);

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
            isUnlocked = 1;
            PlayerPrefs.SetInt("isUnlocked" + itemID, isUnlocked);

            if (itemID < 100)
            {
                StartCoroutine(WomanSpawnerManager.Instance.RandomSpawnWoman(itemID));
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


            GameDataManager.Instance.SaveData();
            //gameObject.SetActive(false);
            StartCoroutine(CloseArea());
        }
        GameDataManager.Instance.SaveData();
    }

    public IEnumerator CloseArea()
    {
        yield return new WaitForSeconds(0.2f);
        //buildNavMesh.BuildNavMesh();
        //gameObject.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private float CalculateFill()
    {
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }

    IEnumerator TimeCounter(int cooldown)
    {
        gameObject.transform.GetChild(4).gameObject.SetActive(true);
        for (int counter = 1; counter <= cooldown; counter++)
        {
            timeFillAmount += 18;
            gameObject.transform.GetChild(4).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc1", timeFillAmount);
            yield return new WaitForSecondsRealtime(0.05f);

            if (counter == cooldown)
            {
                gameObject.transform.GetChild(4).gameObject.SetActive(false);
                cooldown = 20;
                StartCoroutine(CoinMaker);
                break;
            }
        }
    }
}