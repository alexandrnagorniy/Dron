using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VerticleController : MonoBehaviour
{
    public int enemyCount;

    public GameObject good;
    public ParticleSystem destroyed;
    private bool destroyedState;

    public enum typeVerticle { building, vert }
    public typeVerticle type;

    public NavMeshAgent agent;
    private Vector3 targetPosition;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        destroyed.Stop(true);
        if (type == typeVerticle.vert)
        {
            Retarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckDistance() 
    {
        currentPosition = transform.position;
        yield return new WaitForSeconds(0.3f);
        if (Vector3.Distance(currentPosition, transform.position) > 0.6f)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 10)
                StartCoroutine(CheckDistance());
            else
            {
                yield return new WaitForSeconds(Random.Range(3.5f, 5f));
                Retarget();
            }
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(3.5f, 5f));
            Retarget();
        }
    }

    //void CheckDistance() 
    //{
        
    //}

    void Retarget() 
    {
        targetPosition = Vector3.zero;
        float randomDistance = Random.Range(-50f, 50f);
        targetPosition = transform.position + new Vector3(randomDistance, transform.position.y, randomDistance);
        StartCoroutine( CheckDistance());
        agent.SetDestination(targetPosition);
    }

    public void Destroying() 
    {
        if (!destroyedState)
        {
            if (agent != null)
                agent.Stop();
            GameController.Instance.AddPiggydog(enemyCount);
            destroyed.gameObject.SetActive(true);
            destroyed.Play(true);
            good.SetActive(false);
            destroyedState = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tree")
            Destroy(other.gameObject);
    }
}
