using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New AnimationData", menuName = "Animation Data", order = 0)]
public class AnimationData : ScriptableObject
{
    //struct representing one unit of an animation
    [System.Serializable]
    public struct AnimationSegment{
        //MEMBER VARIABLES
        public Sprite sprite;
        public float time;
    }

    //object representing an entire animation
    [System.Serializable]
    public class Animation : IComparable<Animation>{
        //MEMBER VARIABLES
        public string animationKey;
        public AnimationSegment[] animationSegments;

        public Animation(string animationKey){
            this.animationKey = animationKey;
        }

        public int CompareTo(Animation other)
        {
            return this.animationKey.CompareTo(other.animationKey);
        }
    }

    //object storing animation name and the actual animation
    //[System.Serializable]
    //public class AnimationDisplay{
    //    //MEMBER VARIABLES
    //    public string animationName;
    //    public Animation animation;
    //}



    //EDITOR VARIABLES
    [SerializeField]private Animation[] animations;


    //PRIVATE VARIABLES
    private SortedSet<Animation> animationsSet;



    //void Awake(){
    //    animationsMap = new HashSet<Animation>(animations);
    //    animations = null;
    //}
    

    public Animation GetAnimation(int animationIndex){
        if(animationIndex >= animations.Length)
            return null;
        
        return animations[animationIndex];
    }

    public Animation GetAnimation(string animationKey){
        animationsSet ??= new SortedSet<Animation>(animations);

        animationsSet.TryGetValue(new Animation(animationKey), out Animation returnAnimation);
        return returnAnimation;
    }

    public float GetAnimationTime(string animationKey){
        Animation animation = GetAnimation(animationKey);

        if(animation != null){
            float totalTime = 0;
            foreach(AnimationSegment segment in animation.animationSegments){
                totalTime += segment.time;
            }

            return totalTime;
        }

        return 0;
    }

    public int GetAnimationCount(){
        return animationsSet.Count;
    }
}



/*
 * THINGS TO IMPLEMENT:
 * - repeat animation multiple times
 * - chain animations
 * - loop infinitely
 * - alter speed of animation mid game
 * - Movement of sprite???
 * - Different types of animations
 * - repeat certain segments of animation
 */