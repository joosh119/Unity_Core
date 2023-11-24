using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTowardTrajectory : MonoBehaviour
{
    

    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private float minimumSpeed;//minimum speed needed in order for direction to change


    // Update is called once per frame
    void Update()
    {
        Vector2 v = rb.velocity;
        if(v.magnitude >= minimumSpeed){
            float velocityAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            rb.rotation = velocityAngle;


            Vector3 scale = transform.localScale;
            if(rb.rotation > 180)
                transform.localScale = new Vector3(scale.x, -scale.y, scale.z);
            else
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
    }
}
