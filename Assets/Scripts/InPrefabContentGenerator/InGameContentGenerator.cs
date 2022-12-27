using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameContentGenerator : MonoBehaviour
{
    public enum GeneratorType 
    {
        none, once, more
    }
    public GeneratorType type;

    public GameObject[] spawnedObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
