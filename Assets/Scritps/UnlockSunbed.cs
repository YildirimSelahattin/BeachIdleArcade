using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using DG.Tweening;

public class UnlockSunbed : MonoBehaviour
{
    public int sunbedID;
    public int isUnlocked;
    [SerializeField] private GameObject newSunbed;
    [SerializeField] private TextMeshPro dollarAmount;
    [SerializeField] private float sunbedPrice, sunbedRemainPrice;
    [SerializeField] private float fillAmount;
    public NavMeshSurface buildNavMesh;
    public GameObject dollarPrefab;
    public GameObject player;
    private IEnumerator CoinMaker;

    void Start()
    {
        dollarAmount.text = sunbedPrice.ToString();
        sunbedRemainPrice = sunbedPrice;
        isUnlocked = PlayerPrefs.GetInt("isUnlocked" + sunbedID, 0);
        CoinMaker = CountCoins(GameManager.Instance.transform);

        if (isUnlocked == 1)
        {
            GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x, -5, transform.position.z)
                    , Quaternion.Euler(0f, -90f, 0f));

            gameObject.SetActive(false);

            buildNavMesh.BuildNavMesh();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.InstantateMoney((int)sunbedRemainPrice);

        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0)
            StartCoroutine(CoinMaker);
        else
            StopCoroutine(CoinMaker);
/*
        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0)
        {
            if (GameDataManager.Instance.TotalMoney >= sunbedPrice)
            {
                GameDataManager.Instance.TotalMoney -= sunbedPrice;
                sunbedRemainPrice = 0;
                GameDataManager.Instance.SaveData();
                Debug.Log("ID: " + sunbedID);
                isUnlocked = 1;
                PlayerPrefs.SetInt("isUnlocked" + sunbedID, isUnlocked);
            }
            else
            {
                sunbedRemainPrice -= GameDataManager.Instance.TotalMoney;
                fillAmount = CalculateFill();
                GameDataManager.Instance.TotalMoney = 0;
            }

            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);

            UIManager.Instance.totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();
            dollarAmount.text = sunbedRemainPrice.ToString();

            if (sunbedRemainPrice == 0)
            {
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x, -5, transform.position.z)
                    , Quaternion.Euler(0f, -90f, 0f));

                desk.transform.DOScale(2.7f, 2.5f).SetEase(Ease.OutElastic);
                desk.transform.DOScale(2.5f, 2.5f).SetDelay(1.1f).SetEase(Ease.OutElastic);

                gameObject.SetActive(false);

                buildNavMesh.BuildNavMesh();
            }
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {StopCoroutine(CoinMaker);}

    }

    IEnumerator CountCoins(Transform player)
    {
        for (int counter = 0; counter < sunbedRemainPrice; counter++)
        {
            var newCoin = GameManager.Instance.MoneyList[counter];

            if (GameDataManager.Instance.TotalMoney > 0)
            {
                newCoin.transform.position = player.position;
                newCoin.SetActive(true);
                newCoin.transform.DOJump(transform.position, 3f, 1, 0.3f).SetEase(Ease.OutSine);
                SellTheLand();
            }
            else
            {
                counter = 0;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void SellTheLand()
    {
        GameDataManager.Instance.SaveData();

        GameDataManager.Instance.TotalMoney--;

        if (GameDataManager.Instance.TotalMoney == 0)
        {
            Debug.Log("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
        }
    }

    private float CalculateFill()
    {
        Debug.Log("Remain Price: " + sunbedRemainPrice);
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }
}

