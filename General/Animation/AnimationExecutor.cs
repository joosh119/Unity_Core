using System;
using UnityEngine;
using UltEvents;

[RequireComponent(typeof(AnimationManager))]
public class AnimationExecutor : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField] private string[] animationOrder;

    //COMPONENTS
    private AnimationManager animationManager;

    //PRIVATE VARIABLES
    private int currentAnimation;
    private UltEvent _OnAnimationFinish;


    void Awake()
    {
        animationManager = GetComponent<AnimationManager>();

        _OnAnimationFinish = new UltEvent();
    }

    public void StartExecution(int start = 0){
        currentAnimation = start;

        int animationsLength = animationOrder.Length;
        void OnFinish()
        {
            if (start + 1 < animationsLength)
                StartExecution(start + 1);
            else
                _OnAnimationFinish.Invoke();
        }

        animationManager.PlayAnimationOneShot(
            animationOrder[start],
            (Action)OnFinish
        );
    }

    public void AddAnimationFinishAction(Action action){
        _OnAnimationFinish += action;
    }
}