using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject newSunbedArea;

    void Start()
    {
        newSunbedArea.transform.GetChild(1).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Arc2", 90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward,out var hit,1f))
        {
            Debug.DrawRay(transform.position,transform.forward * 1f,Color.green);
            
            if (hit.collider.CompareTag("Sunbed"))
            {
                Debug.Log("Sunbed");
            }
        }
    }
}
