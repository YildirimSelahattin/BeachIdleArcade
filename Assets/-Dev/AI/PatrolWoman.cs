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
            gameObject.transform.DORotate(Vector3.zero, .1f);
            gameObject.transform.DOMoveY(-4.5f, .1f);
            gameObject.transform.DOMoveZ(WomanSpawnerManager.Instance.targetPos[targetIndex].transform.position.z - 1.5f, 0.5f).OnComplete((() =>
            {
                _navAgent.isStopped = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }));
        }
    }
}
