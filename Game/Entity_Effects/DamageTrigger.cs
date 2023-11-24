using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField]private int damage;
    [SerializeField]private bool destroyOnCollide;
    [SerializeField]private GameObject particlesPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Entity")){
            Entity entity = other.GetComponentInParent<Entity>();
            if(entity != null)
                entity.Damage(damage);

                
            if(destroyOnCollide)
                Destroy(gameObject);

            if(particlesPrefab != null){
                GameObject particles = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
                Destroy(particles, 2);
            }
        }
    }
}
