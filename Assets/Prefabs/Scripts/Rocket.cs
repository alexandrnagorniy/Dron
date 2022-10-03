using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private bool shooting;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(transform.GetChild(0), transform.position, Quaternion.identity);
        //transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        //transform.GetChild(0).SetParent(null);
        if (other.tag == "Verticle")
            other.GetComponent<VerticleController>().Destroying();
        Destroy(gameObject);
    }
}
