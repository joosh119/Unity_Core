using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;
using UnityEngine.UI;
using TMPro;
using static JMath.ZeroToOneFunctions;

///<summary>
///Helper class dedicated to in UI events.
///NOTE: All functions in this class continue working when time scale is 0.
///</summary>
public static class UIHelper
{
    //UNIVERSAL

    //Change over time-------------------------------------------------------------------
    ///<summary>
    ///Calls the action given over time, with values between the given start and given end
    ///</summary>
    public static void ChangeFloatOverTime(Action<float> valueToChange, float startValue, float newValue, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_ChangeFloatOverTime(valueToChange, startValue, newValue, time, zeroToOneFunction));
    }
    ///<summary>
    ///Calls the action given over time, with values between the given start and given end. The calls are cancelled if the gameobject given is destroyed
    ///</summary>
    public static void ChangeFloatOverTime(Action<float> valueToChange, float startValue, float newValue, float time, ZeroToOneFunction zeroToOneFunction, GameObject cancelObject){
        Timing.RunCoroutine(_ChangeFloatOverTime(valueToChange, startValue, newValue, time, zeroToOneFunction).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _ChangeFloatOverTime(Action<float> valueToChange, float startValue, float newValue, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        while(t < time){
            t += Time.deltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            float setValue =  Mathf.LerpUnclamped(startValue, newValue, modTime);
            valueToChange.Invoke(setValue);

            yield return Timing.WaitForOneFrame;
        }

        valueToChange.Invoke(newValue);
    }
    
    ///<summary>
    ///Calls the action given over time, with values between the given start and given end
    ///</summary>
    public static void ChangeVectorOverTime(Action<Vector3> valueToChange, Vector3 startValue, Vector3 newValue, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_ChangeVectorOverTime(valueToChange, startValue, newValue, time, zeroToOneFunction));
    }
    ///<summary>
    ///Calls the action given over time, with values between the given start and given end. The calls are cancelled if the gameobject given is destroyed
    ///</summary>
    public static void ChangeVectorOverTime(Action<Vector3> valueToChange, Vector3 startValue, Vector3 newValue, float time, ZeroToOneFunction zeroToOneFunction, GameObject cancelObject){
        Timing.RunCoroutine(_ChangeVectorOverTime(valueToChange, startValue, newValue, time, zeroToOneFunction).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _ChangeVectorOverTime(Action<Vector3> valueToChange, Vector3 startValue, Vector3 newValue, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        while(t < time){
            t += Time.deltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            Vector3 setValue =  Vector3.LerpUnclamped(startValue, newValue, modTime);
            valueToChange.Invoke(setValue);

            yield return Timing.WaitForOneFrame;
        }

        valueToChange.Invoke(newValue);
    }

    ///<summary>
    ///Continuously increments the value passed to the given action, from 0 to 1.
    ///</summary>
    public static void CallZeroToOne(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_CallZeroToOne(action, time, zeroToOneFunction));
    }
    ///<summary>
    ///Continuously increments the value passed to the given action, from 0 to 1. The calls stop if the gameobject given is destroyed
    ///</summary>
    public static void CallZeroToOne(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction, GameObject cancelObject){
        Timing.RunCoroutine(_CallZeroToOne(action, time, zeroToOneFunction));
    }
    public static IEnumerator<float> _CallZeroToOne(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        while(t < time){
            float modTime = zeroToOneFunction.Invoke(t/time);
            action.Invoke(modTime);

            t += Time.unscaledDeltaTime;
            yield return Timing.WaitForOneFrame;
        }
    }



    //TIMING

    //Delay--------------------------------------------------------------------------------
    ///<summary>
    ///Delay lambda or method call by a given time, in seconds
    ///</summary>
    public static void DelayAction(Action action, float delay){
        Timing.RunCoroutine(_DelayAction(action, delay));
    }
    ///<summary>
    ///Delay lambda or method call by a given time, in seconds. The call is canceled of the gameobject given is destroyed
    ///</summary>
    public static void DelayAction(Action action, float delay, GameObject cancelObject){
        Timing.RunCoroutine(_DelayAction(action, delay).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _DelayAction(Action action, float delay){
        {//unscaled delay
            float t = 0;
            while(t < delay){
                t += Time.unscaledDeltaTime;
                yield return Timing.WaitForOneFrame;
            }
        }

        action.Invoke();
    }



    //RECT TRANSFORM

    //Movement-----------------------------------------------------------------------------
    ///<summary>
    ///Moves a transform to the given position on a straight line
    ///</summary>
    public static void MoveTo(RectTransform obj, Vector2 newAnchoredPos, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_MoveTo(obj, newAnchoredPos, time, zeroToOneFunction).CancelWith(obj.gameObject));
    }
    private static IEnumerator<float> _MoveTo(RectTransform obj, Vector2 newPos, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        Vector2 startPos = obj.anchoredPosition;
        while(t < time){
            t += Time.unscaledDeltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            obj.anchoredPosition = Vector2.LerpUnclamped(startPos, newPos, modTime);

            yield return Timing.WaitForOneFrame;
        }

        obj.anchoredPosition = newPos;
    }


    //Scaling------------------------------------------------------------------------------
    ///<summary>
    ///Scales a transform to the given scale
    ///</summary>
    public static void ScaleTo(Transform obj, Vector2 newLocalScale, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_ScaleTo(obj, newLocalScale, time, zeroToOneFunction).CancelWith(obj.gameObject));
    }
    private static IEnumerator<float> _ScaleTo(Transform obj, Vector2 newScale, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        Vector2 startScale = obj.localScale;
        while(t < time){
            t += Time.unscaledDeltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            obj.localScale = Vector2.Lerp(startScale, newScale, modTime);

            yield return Timing.WaitForOneFrame;
        }

        obj.localScale = newScale;
    }
    

    //Shake--------------------------------------------------------------------------------
    ///<summary>
    ///Shakes a given transform. The shaking dampens over time
    ///</summary>
    public static void Shake1(Transform obj, float time, float magnitude){
        Timing.RunCoroutine(_Shake1(obj, time, magnitude, obj.localPosition).CancelWith(obj.gameObject));
    }
    ///<summary>
    ///Shakes a given transform. The shaking dampens over time. The transform returns to the given position when it finishes
    ///</summary>
    public static void Shake1(Transform obj, float time, float magnitude, Vector3 returnPos){
        Timing.RunCoroutine(_Shake1(obj, time, magnitude, returnPos).CancelWith(obj.gameObject));
    }
    private static IEnumerator<float> _Shake1(Transform obj, float time, float magnitude, Vector3 returnPos) {
        float t = 0;
        while(t < time){
            t += Time.unscaledDeltaTime;

            obj.localPosition = returnPos + (Vector3)UnityEngine.Random.insideUnitCircle * magnitude;
            

            yield return Timing.WaitForOneFrame;
        }

        obj.localPosition = returnPos;
        
    }
    ///<summary>
    ///Shakes a given transform. The shaking dampens over time
    ///</summary>
    public static void Shake2(Transform obj, float time, float magnitude){
        Timing.RunCoroutine(_Shake2(obj, time, magnitude, obj.localPosition).CancelWith(obj.gameObject));
    }
    ///<summary>
    ///Shakes a given transform. The shaking dampens over time. The transform returns to the given position when it finishes
    ///</summary>
    public static void Shake2(Transform obj, float time, float magnitude, Vector3 returnPos){
        Timing.RunCoroutine(_Shake2(obj, time, magnitude, returnPos).CancelWith(obj.gameObject));
    }
    private static IEnumerator<float> _Shake2(Transform obj, float time, float magnitude, Vector3 returnPos) {
        float startX = returnPos.x;
        float startY = returnPos.y;

        float t = 0;
        
        while (t < time) {
            
            t += Time.unscaledDeltaTime;          
            
            float percentComplete = t / time;         
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            
            // map value to [-1, 1]
            float x =  UnityEngine.Random.value * 2.0f - 1.0f;
            float y =  UnityEngine.Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;
            
            obj.localPosition = new Vector3(startX + x, startY + y, returnPos.z);
                
            yield return Timing.WaitForOneFrame;
        }
        
        obj.localPosition = returnPos;
    }



    //Text-----------------------------------------------------------------------------------
    ///<summary>
    ///Displays text over time, with the given time between each character
    ///</summary>
    public static void GraduallyDisplayText(TextMeshProUGUI textMesh, string text, float characterDelay){
        Timing.RunCoroutine(_GraduallyDisplayText(textMesh, text, characterDelay, 0).CancelWith(textMesh.gameObject));
    }
    ///<summary>
    ///Displays text over time, with the given time between each character, and the given time between each break character.
    ///NOTE: The break character is '|' and is hardcoded
    ///</summary>
    public static void GraduallyDisplayText(TextMeshProUGUI textMesh, string text, float characterDelay, float breakDelay){
        Timing.RunCoroutine(_GraduallyDisplayText(textMesh, text, characterDelay, breakDelay).CancelWith(textMesh.gameObject));
    }
    private static IEnumerator<float> _GraduallyDisplayText(TextMeshProUGUI textMesh, string text, float characterDelay, float breakDelay){
        int length = text.Length;

        int c = 0;
        while(c < length){
            char nextChar = text[c];
            
            if(nextChar == '|'){
                yield return Timing.WaitForSeconds(breakDelay);
            }
            else{
                textMesh.text += nextChar;
                yield return Timing.WaitForSeconds(characterDelay);
            }
            

            c++;
        }


        
    }
}
