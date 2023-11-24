using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantScreenPosition : MonoBehaviour
{
    [SerializeField]private Transform thisTransform;
    [SerializeField]private Vector2 screenPosition;//relative position on screen from -1 to 1, with 0 being the center
    [SerializeField]private bool moveX;
    [SerializeField]private bool moveY;
    

    // Update is called once per frame
    void Update()
    {
        float xPos = thisTransform.position.x;
        float yPos = thisTransform.position.y;

        float camHalfHeight = Camera.main.orthographicSize;
        Transform camTransform = Camera.main.transform;


        if(moveX){
            float camHalfWidth = camHalfHeight * Camera.main.aspect;
            xPos = camTransform.position.x + (camHalfWidth*screenPosition.x);
        }
        if(moveY){
            yPos = camTransform.position.y + (camHalfHeight*screenPosition.y);
        }


        thisTransform.position = new Vector2(xPos, yPos);
    }
}
