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
        dollarAmount.text = sunbedPrice.ToString("C0");
        sunbedRemainPrice = sunbedPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetFloat("TotalMoney") > 0)
        {
            fillAmount = Mathf.Abs(1f - CalculateMoney() / sunbedPrice);

            if (PlayerPrefs.GetFloat("dollar") >= sunbedPrice)
            {
                PlayerPrefs.SetFloat("dollar", PlayerPrefs.GetFloat("dollar") - sunbedPrice);

                sunbedRemainPrice = 0;
            }
            else
            {
                sunbedRemainPrice -= PlayerPrefs.GetFloat("dollar");
                PlayerPrefs.SetFloat("dollar", 0);
            }

            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);

            UIManager.Instance.totalMoneyText.text = PlayerPrefs.GetFloat("dollar").ToString("C0");
            dollarAmount.text = sunbedRemainPrice.ToString("C0");

            if (sunbedRemainPrice == 0)
            {
                GameObject desk = Instantiate(newSunbed, new Vector3(transform.position.x, 2.2f, transform.position.z)
                    , Quaternion.Euler(0f, -90f, 0f));

                desk.transform.DOScale(1.1f, 1f).SetEase(Ease.OutElastic);
                desk.transform.DOScale(1f, 1f).SetDelay(1.1f).SetEase(Ease.OutElastic);

                gameObject.SetActive(false);

                buildNavMesh.BuildNavMesh();
            }

        }
    }

    private float CalculateMoney()
    {
        return (360 *  sunbedRemainPrice) / PlayerPrefs.GetFloat("TotalMoney");
    }
}

