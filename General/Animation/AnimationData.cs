using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "New AnimationData", menuName = "Animation Data", order = 0)]
public class AnimationData : ScriptableObject
{
    //object representing one unit of an animation
    [System.Serializable]
    public struct AnimationSegment{
        public Sprite sprite;
        public float time;
    }

    //object representing an entire animation
    [System.Serializable]
    public class Animation{
        public string animationKey;
        public AnimationSegment[] animationSegments;
    }


    //public Animation[] animations;
    public OrderedDictionary animations;


    //public Animation GetAnimation(string animationKey){
    //    int prevIndex = 0;
    //    int index = animations.Length/2;
    //    while(true){
    //        int comparison = animationKey.CompareTo(animations[index].animationKey);
    //        
    //        if(comparison == 0){
    //            return animations[index];
    //        }
    //        else {
    //            int diff = Mathf.Abs(index - prevIndex);
    //            index += (int)Mathf.Sign(comparison) * (diff/2);
    //        }
    //
    //    }
    //}


    public Animation GetAnimation(string animationKey){
        animations.
    }

}