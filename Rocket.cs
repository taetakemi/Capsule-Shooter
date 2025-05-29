using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //public GameController gameController;

    public int explosionDamage;
    public float radius;
    float force = 1000f;

    public AudioSource explodeSound;
    public GameObject explosionEffect;

    GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        //StartCoroutine(removeExplosion(explosion, 3f));
        Instantiate(explodeSound, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
                if (nearbyObject.tag == "Player")
                {
                    nearbyObject.GetComponent<Status>().deductHealth(explosionDamage);
                }
                if (nearbyObject.tag == "Enemy")
                {
                    nearbyObject.GetComponent<EnemyStatus>().deductHealth(explosionDamage);
                    nearbyObject.GetComponent<EnemyController>().delayStart();
                }
            }
        }
        Destroy(gameObject);
    }

    //IEnumerator removeExplosion(GameObject explosionObject, float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Debug.Log("times up");
    //    Destroy(explosionObject);
    //}
}