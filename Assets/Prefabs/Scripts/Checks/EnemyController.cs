using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _myAgent;
    private Transform _targetTransform;
    private Animator _myAnimator;

    public ParticleSystem BloodFX;

    private void Awake()
    {
        BloodFX.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _targetTransform = Camera.main.transform;
        _myAnimator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        _myAgent.SetDestination(_targetTransform.position);
    }

    public void Damage()
    {
        BloodFX.Play();
        _myAnimator.SetBool("Dead", true);
        GetComponent<CapsuleCollider>().enabled = false;
        _myAgent.enabled = false;
        this.enabled = false;
    }
}
