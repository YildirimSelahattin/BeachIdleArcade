using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject inGameCam;
    public GameObject creamCam;
    public GameObject Canvas;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 1f))
        {
            Debug.DrawRay(transform.position, transform.forward * 1f, Color.green);

            if (hit.collider.CompareTag("Sunbed"))
            {
                Debug.Log("Sunbed");
            }
            if (hit.collider.CompareTag("LayingWomen"))
            {
                inGameCam.SetActive(false);
                creamCam.SetActive(true);
                gameObject.SetActive(false);
                Canvas.SetActive(false);
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
