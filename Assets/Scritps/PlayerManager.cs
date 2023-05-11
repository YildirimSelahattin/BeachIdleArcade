using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {

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
