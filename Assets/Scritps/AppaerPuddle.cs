using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.AI;

public class AppaerPuddle : MonoBehaviour
{
    public float scale;
    public Color baseColor;
    public float alpha = 1;

    void Start()
    {
        gameObject.transform.DOScale(scale, 1.5f).SetEase(Ease.OutCirc);
        baseColor = gameObject.GetComponent<MeshRenderer>().material.color;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.3f);
        if (alpha > 0)
        {
            alpha -= 0.1f;
            baseColor.a = alpha;
            gameObject.GetComponent<MeshRenderer>().material.color = baseColor;
        }
        else
        {
            alpha = 1;
            gameObject.SetActive(false);
        }
        StartCoroutine(FadeOut());
    }
}
