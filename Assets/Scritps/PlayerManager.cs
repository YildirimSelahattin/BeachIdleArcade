using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    public GameObject inGameCam;
    public GameObject creamCam;
    public GameObject inGameCanvas;
    public GameObject creamingCamvas;
    public GameObject carryParrent;
    public GameObject emergency;
    public bool isCarry = false;

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

            if (hit.collider.CompareTag("DrowningWoman"))
            {
                PlayerController.Instance.playerAnimator.SetLayerWeight(1, 1);
                hit.collider.gameObject.GetComponent<Animator>().SetBool("isCarry", true);
                isCarry = true;
                ObjectPointer.Instance.target = emergency.transform;
                ObjectPointer.Instance.offset = new Vector3(-8,4,9);
                hit.collider.gameObject.transform.parent = carryParrent.transform;
                //Destroy(hit.collider.gameObject);
                hit.collider.gameObject.transform.DOLocalMove(Vector3.zero, 0.2f);
                hit.collider.gameObject.transform.DOLocalRotate(Vector3.zero, 0.2f);
            }

            if(hit.collider.CompareTag("Emergency"))
            {
                if(isCarry == true)
                {
                    Destroy(gameObject.transform.GetChild(2).transform.GetChild(0).gameObject);
                    PlayerController.Instance.playerAnimator.SetLayerWeight(1, 0);
                    isCarry = false;
                    ObjectPointer.Instance.img.enabled = false;
                     ObjectPointer.Instance.offset = new Vector3(0,4,0);

                }
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
        yield return new WaitForSecondsRealtime(10);
        creamingCamvas.SetActive(true);
        CustomerRequestsManager.Instance.request.SetActive(false);
    }

}
