using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunMashineScene : MonoBehaviour
{
    public static GunMashineScene Instance;
    public GameObject EnemyPrefab;

    private Transform _targetTransform;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _targetTransform = Camera.main.transform;
    }

    public void SpawnEnemy(Vector3 value)
    {
        int count = Random.Range(3, 7);
        
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, new Vector3(value.x + Random.Range(-10, 11), 0, value.z + Random.Range(-10, 11)), Quaternion.identity);
            enemy.SetActive(true);
        }

        
    }
}