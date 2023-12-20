using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField]private Sprite[] frames;
    [SerializeField]private float frameRate;
    private SpriteRenderer spriteRenderer;
    private float lastFrameTime;
    private int currentFrame;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastFrameTime >= 1/frameRate){
            lastFrameTime = Time.time;
            currentFrame++;
            if(currentFrame>=frames.Length)
                currentFrame = 0;
            
            spriteRenderer.sprite = frames[currentFrame];
        }
    }

    public void SetAnimation(Sprite[] animation){
        frames = animation;

        spriteRenderer.sprite = frames[currentFrame];
    }
}
