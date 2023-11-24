using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField]private int damage;
    [SerializeField]private UltEvents.UltEvent _onDamage;
    [SerializeField]private GameObject particlesPrefab;


    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Collider2D other = collision2D.collider;
        if(other.CompareTag("Entity")){
            Entity entity = other.GetComponentInParent<Entity>();
            if(entity != null)
                entity.Damage(damage);

                
            

            if(particlesPrefab != null){
                GameObject particles = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
                Destroy(particles, 2);
            }


            _onDamage.Invoke();
        }


    }
}
