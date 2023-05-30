using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public GameObject inGameCam;
    public GameObject creamCam;
    public GameObject paintManager;
    public GameObject paintedTexture;
    public GameObject inGameCanvas;
    public GameObject creamingDoneButton;
    public GameObject carryParrent;
    public GameObject emergency;
    public bool isCarry = false;
    public bool isSafe = false;
    public ParticleSystem heartParticle;
    NavMeshAgent playerNavMesh;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        paintManager.SetActive(false);
        playerNavMesh = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 1f))
        {
            Debug.DrawRay(transform.position, transform.forward * 1f, Color.green);

            if (hit.collider.CompareTag("LayingWomen"))
            {
                hit.collider.GetComponent<BoxCollider>().enabled = false;
                CreamSceneOpen();
            }

            if (hit.collider.CompareTag("DrowningWoman"))
            {
                PlayerController.Instance.playerAnimator.SetLayerWeight(1, 1);
                hit.collider.gameObject.GetComponent<Animator>().SetBool("isCarry", true);
                isCarry = true;
                Pointer.Instance.target = emergency.transform;
                hit.collider.gameObject.transform.parent = carryParrent.transform;
                //Destroy(hit.collider.gameObject);
                hit.collider.gameObject.transform.DOLocalMove(Vector3.zero, 0.2f);
                hit.collider.gameObject.transform.DOLocalRotate(Vector3.zero, 0.2f);
                Pointer.Instance.img.material.color = Color.green;
            }

            if (hit.collider.CompareTag("Emergency"))
            {
                if (isCarry == true)
                {
                    Destroy(gameObject.transform.GetChild(2).transform.GetChild(0).gameObject);
                    heartParticle.Play();
                    PlayerController.Instance.playerAnimator.SetLayerWeight(1, 0);
                    isCarry = false;
                    Pointer.Instance.img.enabled = false;
                    Pointer.Instance.img.material.color = Color.red;
                    LifeguardController.Instance.StartCoroutine(LifeguardController.Instance.RandomSpawnDrownedWoman(60));
                    CoinPickup.Instance.StartCoroutine(CoinPickup.Instance.UIMoneySpawner());
                }
            }
        }
    }
    public void OnClickDoneCream()
    {
        CreamSceneClose();
        CoinPickup.Instance.StartCoroutine(CoinPickup.Instance.UIMoneySpawner());
    }

    public void CreamSceneOpen()
    {
        inGameCam.SetActive(false);
        creamCam.SetActive(true);
        inGameCanvas.SetActive(false);
        playerNavMesh.speed = 0;
        //gameObject.GetComponent<PlayerController>().enabled = false;
        paintManager.SetActive(true);
        (paintedTexture.GetComponent<Paintable>() as MonoBehaviour).enabled = true;
    }

    public void CreamSceneClose()
    {
        playerNavMesh.speed = 5.5f;
        inGameCam.SetActive(true);
        creamCam.SetActive(false);
        inGameCanvas.SetActive(true);
        creamingDoneButton.SetActive(false);
        paintManager.SetActive(false);
        //gameObject.GetComponent<PlayerController>().enabled = true;
        (paintedTexture.GetComponent<Paintable>() as MonoBehaviour).enabled = false;
    }
}
