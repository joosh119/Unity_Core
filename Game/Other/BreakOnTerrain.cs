using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnTerrain : MonoBehaviour
{
    [SerializeField]private GameObject breakParticlesPrefab;
    [SerializeField]private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Untagged")){
            Destroy(gameObject);
            

            float angle = Mathf.Atan2(-rb.velocity.y, -rb.velocity.x) * Mathf.Rad2Deg;
            GameObject particles = Instantiate(breakParticlesPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            Destroy(particles, 2);
        }
    }
}
