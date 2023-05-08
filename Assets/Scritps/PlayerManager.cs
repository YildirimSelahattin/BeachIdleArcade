using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] newSunbedArea;

    void Start()
    {
        foreach (GameObject sunbed in newSunbedArea)
        {
            Material radialFillCloneMat = new Material(Shader.Find("Custom/RadialFill"));
            radialFillCloneMat.SetFloat("_Angle", 270);
            radialFillCloneMat.SetFloat("_Arc1", 0);
            radialFillCloneMat.SetFloat("_Arc2", 360);
            sunbed.transform.GetChild(1).GetComponent<SpriteRenderer>().material = radialFillCloneMat;
        }
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 1f))
        {
            Debug.DrawRay(transform.position, transform.forward * 1f, Color.green);

            if (hit.collider.CompareTag("Sunbed"))
            {
                Debug.Log("Sunbed");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewSunbed"))
        {
            //fillAmount = givenMoney * 360) / 100;
            //newSunbedArea[0].transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", fillAmount);

            GameDataManager.Instance.SaveData();
        }
    }
}
