using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public AudioSource source;
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

    IEnumerator Sound() 
    {
        source.Play();
        Instantiate(transform.GetChild(0), transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Sound());
        if (!shooting)
        {
            if (other.tag == "Verticle")
                other.GetComponent<VerticleController>().Destroying();
            shooting = true;
        }
    }
}
