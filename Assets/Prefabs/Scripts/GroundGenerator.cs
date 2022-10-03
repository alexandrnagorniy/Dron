using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public GameObject[] trees;
    public int minTrees, maxTrees;
    public GameObject[] targetObject;
    public int minTO, maxTO;

    void Generate() 
    {
        float x = transform.position.x;
        float z = transform.position.z;
        int _trees = Random.Range(minTrees, maxTrees);
        for (int i = 0; i < _trees; i++)
        {

            Vector3 _pos = new Vector3(Random.Range(x - 35, x + 35), 0, Random.Range(z - 18, z + 18));
            Instantiate(trees[Random.Range(0, trees.Length)], _pos, Quaternion.identity, GameController.Instance.parent);
        }

        Vector3 pos = new Vector3(Random.Range(x - 30, x + 30), 0.5f, Random.Range(z - 15, z + 15));
        Instantiate(targetObject[Random.Range(0, targetObject.Length)], pos,
            Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0)), GameController.Instance.parent);

        this.enabled = false;
    }

    void Update()
    {
        if (Vector3.Distance(CameraController.Instance.target.position, transform.position) < 150)
            Generate();
    }
}
