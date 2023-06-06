using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeverageServiceContoller : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    public GameObject player;
    public GameObject[] beverages;
    public bool isServing;

    void Start()
    {
        Material radialFillCloneMat = new Material(Shader.Find("Custom/RadialFill"));
        radialFillCloneMat.SetFloat("_Angle", 270);
        radialFillCloneMat.SetFloat("_Arc1", 0);
        radialFillCloneMat.SetFloat("_Arc2", fillAmount);
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().material = radialFillCloneMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isServing == false)
                StartCoroutine(ServingBeverage(player.transform));
            else
                StopCoroutine(ServingBeverage(player.transform));
        }
    }

    IEnumerator ServingBeverage(Transform player)
    {
        yield return new WaitForSeconds(1);

        for (int counter = 0; counter <= 1; counter++)
        {
            var beverage = beverages[counter];

            beverage.transform.position = player.position;
            beverage.SetActive(true);
            beverage.transform.DOJump(transform.position, 3f, 1, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                if (beverage != null)
                    beverage.transform.localPosition = Vector3.zero;
            });
            if (counter == 1)
            {
                isServing = true;
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}
