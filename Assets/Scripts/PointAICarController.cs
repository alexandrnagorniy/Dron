using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAICarController : MonoBehaviour
{
    public GameObject[] Points;

    private NavMeshAgent _myAgent;
    private int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        _myAgent.SetDestination(Points[currentPoint].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Points[currentPoint].transform.position) < 0.85f)
        {
            if (currentPoint < Points.Length - 1)
                currentPoint++;
            else
                currentPoint = 0;
            _myAgent.SetDestination(Points[currentPoint].transform.position);
            GunMashineScene.Instance.SpawnEnemy(Points[currentPoint].transform.position);
        }
    }
}
