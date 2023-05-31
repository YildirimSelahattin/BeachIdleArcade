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

    void Start()
    {
        _navAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
        _targetCol = WomanSpawnerManager.Instance.targetPos[0].GetComponent<Collider>();
        _navAgent.SetDestination(WomanSpawnerManager.Instance.targetPos[0].transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _targetCol)
        {
            _animator.SetBool("sit", true);
            gameObject.transform.DORotate(Vector3.zero,.1f);
            gameObject.transform.DOMoveZ(8f,0.5f).OnComplete((() => 
            {
                _navAgent.isStopped = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }));
        }
    }
}
