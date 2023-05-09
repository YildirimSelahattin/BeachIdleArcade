using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using DG.Tweening;

public class UnlockSunbed : MonoBehaviour
{
    [SerializeField] private GameObject newSunbed;
    [SerializeField] private TextMeshPro dollarAmount;
    [SerializeField] private float sunbedPrice, sunbedRemainPrice;
    [SerializeField] private float fillAmount;
    public NavMeshSurface buildNavMesh;


    void Start()
    {
        dollarAmount.text = sunbedPrice.ToString();
        sunbedRemainPrice = sunbedPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameDataManager.Instance.TotalMoney > 0)
        {
            if (GameDataManager.Instance.TotalMoney >= sunbedPrice)
            {
                Debug.Log("Buyed");
                GameDataManager.Instance.TotalMoney -= sunbedPrice;
                sunbedRemainPrice = 0;
                GameDataManager.Instance.SaveData();
            }
            else
            {
                Debug.Log("ohhhhh");
                sunbedRemainPrice -= GameDataManager.Instance.TotalMoney;
                CalculateFill();
                GameDataManager.Instance.SaveData();
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
    }

    private float CalculateFill()
    {
        Debug.Log("Remain Price: " + sunbedRemainPrice);
        return (360 * sunbedRemainPrice) / sunbedPrice;
    }
}

