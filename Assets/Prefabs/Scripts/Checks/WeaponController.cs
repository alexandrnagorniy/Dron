using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public ParticleSystem BulletPaticle;

    [SerializeField]
    private float _cooldown;
    private bool _canShoot = true;
    private bool _shooting;

    private void Awake()
    {
        BulletPaticle.Stop();
    }

    public void StartShooting()
    {
        _shooting = true;
    }

    public void EndShooting()
    {
        _shooting = false;
    }

    void Update()
    {
        if (_shooting)
        {
            if (_canShoot)
            {
                BulletPaticle.Play();
                StartCoroutine(ShootingTimer());
                _canShoot = false;
            }
        }
    }

    IEnumerator ShootingTimer()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.GetComponent<EnemyController>() != null)
            {
                hit.transform.GetComponent<EnemyController>().Damage();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
        yield return new WaitForSeconds(_cooldown);
        _canShoot = true;
    }
}
