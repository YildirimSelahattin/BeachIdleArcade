using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeverageServiceContoller : MonoBehaviour
{
    [SerializeField] private float fillAmount;
    public GameObject player;
    public GameObject[] beverages;

    void Start()
    {
        Material radialFillCloneMat = new Material(Shader.Find("Custom/RadialFill"));
        radialFillCloneMat.SetFloat("_Angle", 270);
        radialFillCloneMat.SetFloat("_Arc1", 0);
        radialFillCloneMat.SetFloat("_Arc2", 0);
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().material = radialFillCloneMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ServingBeverage(player.transform));
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator ServingBeverage(Transform player)
    {
        for (int counter = 0; counter <= 1; counter++)
        {
            var beverage = beverages[counter];

            beverage.transform.position = player.position;
            beverage.SetActive(true);
            beverage.transform.DOJump(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 2.5f), .5f, 1, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                gameObject.transform.parent.GetChild(5).transform.GetChild(4).gameObject.SetActive(true);

                if (beverage != null)
                    beverage.transform.localPosition = Vector3.zero;
            });
            fillAmount += 180;
            gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);

            yield return new WaitForSecondsRealtime(0.2f);
        }

        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(0.3f);
        fillAmount--;
        gameObject.transform.parent.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);

        if(fillAmount > 0)
        {
            StartCoroutine(TimeCounter());
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.transform.GetChild(5).transform.GetChild(4).gameObject.SetActive(false);
        }
    }
}
