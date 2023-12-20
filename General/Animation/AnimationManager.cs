using System;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(SpriteRenderer))]
public class AnimationManager : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField]private string startAnimationKey;
    [SerializeField]private AnimationData animationData;
    public AnimationData _animationData{ get {return animationData;} }
    
    //COMPONENTS
    private SpriteRenderer spriteRenderer;
    
    //PRIVATE VARIABLES
    private int currentFrame;
    private float frameStartTime;
    private float animationSpeedModifier;
    private bool loop;
    private bool priority;
    private AnimationData.Animation currentAnimation;
    private UltEvent _OnAnimationFinish;



    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayAnimation(startAnimationKey, enabled);
    }


    // Update is called once per frame
    void Update()
    {
        //Check if past the time to start
        float elapsed = Time.time - frameStartTime;
        if( elapsed >= currentAnimation.animationSegments[currentFrame].time * animationSpeedModifier ){
            StepAnimation();
        }
    }



    public void PauseAnimation(bool paused){
        this.enabled = !paused;
    }

    private void StartAnimation(AnimationData.Animation animation, bool startAnimation, int startFrame){
        loop = true;
        _OnAnimationFinish = null;

        currentFrame = startFrame;
        
        //set new frame instantly
        spriteRenderer.sprite = currentAnimation.animationSegments[startFrame].sprite;
        frameStartTime = Time.time;

        //reset speed modifier to default
        animationSpeedModifier = 1;

        //pause animation if animation has one frame
        if(currentAnimation.animationSegments.Length <= 1)
            enabled = false;
        else if(startAnimation)
            enabled = true;
        
        
    }

    public bool PlayAnimation(int animationIndex, bool startAnimation = true, int startFrame = 0){
        if(priority)
            return false;
        
        AnimationData.Animation searchAnimation = animationData.GetAnimation(animationIndex);

        if(searchAnimation.animationKey == currentAnimation?.animationKey)
            return true;
        
        currentAnimation = searchAnimation;
        
        if(currentAnimation == null){
            enabled = false;
            return false;
        }


        StartAnimation(currentAnimation, startAnimation, startFrame);

        return true;
    }

    public bool PlayAnimation(string animationKey, bool startAnimation = true, int startFrame = 0){
        if(priority)
            return false;
        
        
        if(animationKey == currentAnimation?.animationKey)
            return true;
        
        currentAnimation = animationData.GetAnimation(animationKey);
        
        if(currentAnimation == null){
            enabled = false;
            return false;
        }
            
        StartAnimation(currentAnimation, startAnimation, startFrame);

        return true;
    }

    public void PlayAnimationOneShot(string animationKey, UltEvent OnAnimationFinish = null, bool hasPriority = false, int startFrame = 0){
        if(priority)
            return;
        
        PlayAnimation(animationKey, true, startFrame);
        
        loop = false;
        priority = hasPriority;
        this._OnAnimationFinish = OnAnimationFinish;
    }

    public void PlayAnimationOneShot(string animationKey, UltEventHolder OnAnimationFinish, bool hasPriority = false, int startFrame = 0){
        //Don't know how to convert from UltEvent to Action
        PlayAnimationOneShot(animationKey, OnAnimationFinish.Event, hasPriority, startFrame);
    }
    
    public void StepAnimation(int frameStep = 1){
        if(currentAnimation == null)
            return;
        
        currentFrame += frameStep;
        if(currentFrame >= currentAnimation.animationSegments.Length){
            if(loop)
                currentFrame = 0;
            else{
                priority = false;
                enabled = false;
                _OnAnimationFinish?.Invoke();
                return;
            }
        }
            

        //set new frame
        spriteRenderer.sprite = currentAnimation.animationSegments[currentFrame].sprite;
        frameStartTime = Time.time;
    }

    public void IncrementAnimationIndex(int indexStep = 1){
        int currentIndex = 0;
        //find current index
        for(int i = 0; i < animationData.GetAnimationCount(); i++){
            if(animationData.GetAnimation(i).animationKey == currentAnimation.animationKey){
                currentIndex = i;
                break;
            }
        }

        if(currentIndex + indexStep <  animationData.GetAnimationCount())
            PlayAnimation(currentIndex + indexStep);
    }

    public void ChangeAnimationSpeed(float timeModifier){
        if(timeModifier == 0)
            animationSpeedModifier = float.PositiveInfinity;
        else
            animationSpeedModifier = 1/timeModifier;
    }


    public int GetCurrentAnimationLength(){
        return currentAnimation.animationSegments.Length;
    }
}
