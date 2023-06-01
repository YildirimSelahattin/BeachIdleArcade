using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PatrolWoman : MonoBehaviour
{
    Animator _animator;
    NavMeshAgent _navAgent;
    Collider _targetCol;
    public int targetIndex;

    void Start()
    {
        _navAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
        _targetCol = WomanSpawnerManager.Instance.targetPos[targetIndex].GetComponent<Collider>();
        _navAgent.SetDestination(WomanSpawnerManager.Instance.targetPos[targetIndex].transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _targetCol)
        {
            _animator.SetBool("sit", true);
            gameObject.transform.DOMoveY(-4.5f, .1f);
            gameObject.transform.DOMoveZ(WomanSpawnerManager.Instance.targetPos[targetIndex].transform.position.z - .8f, 0.5f).OnComplete((() =>
            {
                _navAgent.isStopped = true;
                gameObject.transform.DORotate(Vector3.zero, .1f);
                gameObject.GetComponent<BoxCollider>().enabled = false;

                switch (targetIndex)
                {
                    case 0:
                        WomanSpawnerManager.Instance.umbrella[1].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[1].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 1:
                        WomanSpawnerManager.Instance.umbrella[0].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[0].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 3:
                        WomanSpawnerManager.Instance.umbrella[3].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[3].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 4:
                        WomanSpawnerManager.Instance.umbrella[2].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[2].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 5:
                        WomanSpawnerManager.Instance.umbrella[2].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[2].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 7:
                        WomanSpawnerManager.Instance.umbrella[4].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[4].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 8:
                        WomanSpawnerManager.Instance.umbrella[4].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[4].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 9:
                        WomanSpawnerManager.Instance.umbrella[7].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[7].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 10:
                        WomanSpawnerManager.Instance.umbrella[6].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[6].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 12:
                        WomanSpawnerManager.Instance.umbrella[6].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[6].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 13:
                        WomanSpawnerManager.Instance.umbrella[5].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[5].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    case 14:
                        WomanSpawnerManager.Instance.umbrella[5].transform.GetChild(5).transform.GetChild(1).gameObject.SetActive(false);
                        WomanSpawnerManager.Instance.umbrella[5].transform.GetChild(5).transform.GetChild(2).gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }));
        }
    }
}
