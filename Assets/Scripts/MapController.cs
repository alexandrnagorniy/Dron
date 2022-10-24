using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform.parent;

        StartCoroutine(Check());
    }

    IEnumerator Check() 
    {
        yield return new WaitForSeconds(1f);
        if (Vector3.Distance(transform.position, target.position) < 200)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (!Physics.Raycast(transform.position + transform.right * 35, Vector3.up, out hit) && hit.collider == null)
                Instantiate(transform, transform.position + transform.right * 35, transform.rotation);
            if (!Physics.Raycast(transform.position + transform.right * -35, Vector3.up, out hit) && hit.collider == null)
                Instantiate(transform, transform.position + transform.right * -35, transform.rotation);
        }
        else
            StartCoroutine(Check());
    }
}