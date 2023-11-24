using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : PlayerControllable
{
    [SerializeField]private Sprite releasedSprite;
    [SerializeField]private Sprite pressedSprite;

    public SpriteRenderer spriteRenderer;


    // Update is called once per frame
    void Update()
    {
        transform.position = InputManager.mouseWorldSpacePosition;
    }


    public override void LeftMouseClick(bool justPressed, bool isPressed){
        if(justPressed){
            spriteRenderer.sprite = pressedSprite;
        }
        else if(!isPressed){
            spriteRenderer.sprite = releasedSprite;
        }
    }
}
