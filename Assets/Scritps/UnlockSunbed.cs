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
    float tempSunbedPrice;
    public bool inside = false;
    public Camera BarCam;
    public static bool isBarOpen = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        PlayerPrefs.SetInt("isUnlocked" + 0, 1);
    }

    void Start()
    {
        dollarAmount.text = sunbedPrice.ToString();
        sunbedRemainPrice = sunbedPrice;
        isUnlocked = PlayerPrefs.GetInt("isUnlocked" + itemID, 0);

        sunbedRemainPrice = PlayerPrefs.GetFloat("sunbedRemainPrice" + itemID, sunbedRemainPrice);
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);
        UIManager.Instance.totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();
        dollarAmount.text = sunbedRemainPrice.ToString();

        fillAmount = CalculateFill();
        CoinMaker = CountCoins(GameManager.Instance.transform);

        Material radialFillCloneMat = new Material(Shader.Find("Custom/RadialFill"));
        radialFillCloneMat.SetFloat("_Angle", 270);
        radialFillCloneMat.SetFloat("_Arc1", 0);
        radialFillCloneMat.SetFloat("_Arc2", fillAmount);
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().material = radialFillCloneMat;

        if (isUnlocked == 1)
        {
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
            StartCoroutine(CloseArea());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.InstantateMoney((int)sunbedRemainPrice);

        if (other.CompareTag("Player"))
        {
            if (GameDataManager.Instance.TotalMoney > 0 && isUnlocked == 0)
            {
                StartCoroutine(CoinMaker);
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

        PlayerPrefs.SetFloat("sunbedRemainPrice" + itemID, sunbedRemainPrice);

        if (sunbedRemainPrice == 0)
        {
            if(itemID == 1001)
            {
                isBarOpen = true;
                BarCam.gameObject.SetActive(true);
                StartCoroutine(CamMove());
            }

            isUnlocked = 1;
            PlayerPrefs.SetInt("isUnlocked" + itemID, isUnlocked);

            if (itemID < 100)
            {
                StartCoroutine(WomanSpawnerManager.Instance.RandomSpawnWoman(itemID));

                gameObject.transform.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(5).gameObject.SetActive(true);
            }

            GameDataManager.Instance.SaveData();
            StartCoroutine(CloseArea());
        }
        GameDataManager.Instance.SaveData();
    }

    public IEnumerator CamMove()
    {
        yield return new WaitForSeconds(0.05f);
        BarCam.gameObject.transform.DOMoveY(BarCam.gameObject.transform.position.y - 1, 2f).OnComplete(() =>
        {
            BarCam.gameObject.SetActive(false);
        });
    }

    public IEnumerator CloseArea()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        buildNavMesh.BuildNavMesh();
    }

    private float CalculateFill()
    {
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }
}