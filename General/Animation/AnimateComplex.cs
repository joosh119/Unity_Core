using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class AnimateComplex : MonoBehaviour
{
    [SerializeField]private AnimationData animationData;
    

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float frameStartTime;
    

    private bool loop;
    private AnimationData.Animation currentAnimation;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if(currentAnimation != null){

        }
    }


    public void PlayAnimation(string animationKey){
        
    }
}
