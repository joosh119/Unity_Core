using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkAI : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;

    [SerializeField]private float walkForce;
    [SerializeField]private Vector2 startDirection;
    [SerializeField]private float minSpeed;//minimum speed before switching directions
    
    private Vector2 currentDirection;
    private bool hasReachedMinimumSpeed;

    void Start()
    {
        currentDirection = startDirection;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(walkForce* Time.deltaTime * currentDirection);

        float vX = rb.velocity.x;
        //if it has been stopped
        if(Mathf.Abs(vX) > minSpeed){
            hasReachedMinimumSpeed = true;
        }
        else if(Mathf.Abs(vX) < minSpeed && hasReachedMinimumSpeed){
            currentDirection *= -1;

            float absScale = Mathf.Abs(transform.localScale.x);
            if(vX > 0)
                transform.localScale = new Vector2(-absScale, transform.localScale.y);
            else
                transform.localScale = new Vector2(-absScale, transform.localScale.y);


                
            hasReachedMinimumSpeed = false;
        }
    }
}
