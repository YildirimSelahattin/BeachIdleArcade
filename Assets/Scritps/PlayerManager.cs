using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject inGameCam;
    public GameObject creamCam;
    public GameObject inGameCanvas;
    public GameObject creamingCamvas;

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
                inGameCanvas.SetActive(false);

                StartCoroutine(Deeeeeeee());
                
            }
        }
    }
    public void OnClickDoneCream()
    {
        inGameCam.SetActive(true);
        creamCam.SetActive(false);
        inGameCanvas.SetActive(true);
        creamingCamvas.SetActive(false);
    }

    IEnumerator Deeeeeeee()
    {
        yield return new WaitForSecondsRealtime(5);
        creamingCamvas.SetActive(true);
        CustomerRequestsManager.Instance.request.SetActive(false);
    }

}
