using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnTerrain : MonoBehaviour
{
    //PREFABS
    [SerializeField]private GameObject breakParticlesPrefab;

    //COMPONENTS
    private Rigidbody2D rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Break(other);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Break(other.collider);
    }



    void Break(Collider2D other){
        if(other.CompareTag("Untagged")){
            Destroy(gameObject);
            

            float angle = Mathf.Atan2(-rb.velocity.y, -rb.velocity.x) * Mathf.Rad2Deg;
            if(breakParticlesPrefab != null){
                GameObject particles = Instantiate(breakParticlesPrefab, transform.position, Quaternion.Euler(0, 0, angle));
                Destroy(particles, 2);
            }
        }
    }
}
