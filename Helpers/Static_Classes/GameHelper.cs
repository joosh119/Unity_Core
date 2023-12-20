using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MEC;

using static JMath.ZeroToOneFunctions;
using Unity.VisualScripting;
using UnityEngine.Scripting;

///<summary>
///Helper class dedicated to in game events.
///NOTE: Time based functions in this class stop working when time scale is 0.
///DEVNOTE: Fix above issue. Create a way to distinguish if the moethod should be affected by timescale or not.
///Need fixes: ChangeFloatOverTime, ChangeVectorOverTime, ~DelayAction~, ~RepeatAction~, ~ProgressiveRepeatAction~, CallZeroToOne, MoveTo, MoveOnPath, ScaleTo, Shake1, Shake2, FadeTo, SizeTo
///DEVNOTE: Condense multiple methods to use default arguments, instead of method overloading
///DEVNOTE: Shake function does not slow with timescale; it should.
///</summary>
public static class GameHelper
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



    //TIMING

    //Delay--------------------------------------------------------------------------------
    ///<summary>
    ///Delay lambda or method call by a given time, in seconds. The call is cancelled if the gameobject given is destroyed
    ///</summary>
    public static void DelayAction(Action action, float delay, GameObject cancelObject){
        Timing.RunCoroutine(_DelayAction(action, delay).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _DelayAction(Action action, float delay){
        yield return Timing.WaitForSeconds(delay);

        action.Invoke();
    }
    
    //TIMING

    //Delay--------------------------------------------------------------------------------
    ///<summary>
    ///Delay lambda or method call by a given time, in seconds. The call is cancelled if the gameobject given is destroyed
    ///</summary>
    public static void DelayActionUnscaled(Action action, float delay, GameObject cancelObject){
        Timing.RunCoroutine(_DelayActionUnscaled(action, delay).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _DelayActionUnscaled(Action action, float delay){
        float startTime = Time.unscaledTime;
        do{
            yield return Timing.WaitForOneFrame;
        }
        while( Time.unscaledTime - startTime < delay );

        action.Invoke();
    }


    //Repetition---------------------------------------------------------------------------
    ///<summary>
    ///Repeat action the given number of times, with the given delay between each call
    ///</summary>
    public static void RepeatAction(Action action, int timesToRepeat, float delay){
        Timing.RunCoroutine(_RepeatAction(action, timesToRepeat, delay));
    }
    ///<summary>
    ///Repeat action the given number of times, with the given delay between each call. The calls stop if the gameobject given is destroyed
    ///</summary>
    public static void RepeatAction(Action action, int timesToRepeat, float delay, GameObject cancelObject){
        Timing.RunCoroutine(_RepeatAction(action, timesToRepeat, delay).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _RepeatAction(Action action, int timesToRepeat, float delay){
        int c = 0;
        while(c < timesToRepeat){
            action.Invoke();

            c++;
            yield return Timing.WaitForSeconds(delay);
        }
    }

    ///<summary>
    ///Repeat action the given number of times, with the given delay between each call. For each call, it sends a point, with each point separated by given distance, centered at the given point
    ///</summary>
    public static void ProgressiveRepeatAction(Action<float> action, int timesToRepeat, float delay, float separation, float centerPoint){
        Timing.RunCoroutine(_ProgressiveRepeatAction(action, timesToRepeat, delay, separation, centerPoint), null);
    }
    ///<summary>.
    ///Repeat action the given number of times, with the given delay between each call. For each call, it sends a point, with each point separated by given distance, centered at the given point. The calls stop if the gameobject given is destroyed
    ///</summary>
    public static void ProgressiveRepeatAction(Action<float> action, int timesToRepeat, float delay, float separation, float centerPoint, GameObject cancelObject){
        Timing.RunCoroutine(_ProgressiveRepeatAction(action, timesToRepeat, delay, separation, centerPoint).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _ProgressiveRepeatAction(Action<float> action, int timesToRepeat, float delay, float separation, float centerPoint){
        float startPoint = centerPoint + JMath.GetStartPoint(timesToRepeat, separation);
        
        int i = 0;
        while(i < timesToRepeat){
            float newPoint = startPoint  +  (i * separation);
            action.Invoke(newPoint);

            i++;
            yield return Timing.WaitForSeconds(delay);
        }
    }

    ///<summary>
    ///Continuously increments the value passed to the given action, from 0 to 1. The calls stop if the gameobject given is destroyed
    ///</summary>
    public static void CallZeroToOne(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction, GameObject cancelObject){
        Timing.RunCoroutine(_CallZeroToOne(action, time, zeroToOneFunction).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _CallZeroToOne(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        while(t < time){
            float modTime = zeroToOneFunction.Invoke(t/time);
            action.Invoke(modTime);

            t += Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
        action.Invoke(1);
    }

    ///<summary>
    ///Continuously increments the value passed to the given action, from 0 to 1. The calls stop if the gameobject given is destroyed
    ///</summary>
    public static void CallZeroToOneUnscaled(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction, GameObject cancelObject){
        Timing.RunCoroutine(_CallZeroToOneUnscaled(action, time, zeroToOneFunction).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _CallZeroToOneUnscaled(Action<float> action, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        while(t < time){
            float modTime = zeroToOneFunction.Invoke(t/time);
            action.Invoke(modTime);

            t += Time.unscaledDeltaTime;
            yield return Timing.WaitForOneFrame;
        }
        action.Invoke(1);
    }

    /// <summary>
    /// Continuously calls the update function until the given gameobject. Functions as Update() would on a gameobject.
    /// This shouldn't really be relied on all the time.
    /// </summary>
    /// <param name="update"></param>
    /// <param name="cancelObject"></param>
    public static void PseudoUpdate(Action update, GameObject cancelObject){
        Timing.RunCoroutine(_PseudoUpdate(update).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _PseudoUpdate(Action update){
        while(true){
            update.Invoke();

            yield return Timing.WaitForOneFrame;
        }
    }



    //TRANSFORMS

    //Movement-----------------------------------------------------------------------------
    ///<summary>
    ///Moves a transform to the given position on a straight line
    ///</summary>
    public static void MoveTo(Transform obj, Vector2 newLocalPos, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_MoveTo(obj, newLocalPos, time, zeroToOneFunction).CancelWith(obj.gameObject));
    }
    private static IEnumerator<float> _MoveTo(Transform obj, Vector2 newPos, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        Vector2 startPos = obj.localPosition;
        while(t < time){
            t += Time.deltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            obj.localPosition = Vector2.LerpUnclamped(startPos, newPos, modTime);

            yield return Timing.WaitForOneFrame;
        }

        obj.localPosition = newPos;
    }
    
    /// <summary>
    /// NOT IMPLEMENTED
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="newLocalPos"></param>
    /// <param name="time"></param>
    public static void MoveOnPath(Transform obj, Vector2 newLocalPos, float time){
        Timing.RunCoroutine(_MoveOnPath(obj, newLocalPos, time).CancelWith(obj.gameObject));
    }
    private static IEnumerator<float> _MoveOnPath(Transform obj, Vector2 newPos, float time){
        float t = 0;
        Vector2 startPos = obj.position;
        while(t < time){
            t += Time.deltaTime;
            //float modTime = zeroToOneFunction.Invoke(t/time);

            //obj.localPosition = Vector2.LerpUnclamped(startPos, newPos, modTime);

            yield return Timing.WaitForOneFrame;
        }

        obj.localPosition = newPos;
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
            t += Time.deltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            obj.localScale = Vector2.Lerp(startScale, newScale, modTime);

            yield return Timing.WaitForOneFrame;
        }

        obj.localScale = newScale;
    }
    

    //Shake--------------------------------------------------------------------------------
    ///<summary>
    ///Shakes a given transform.
    ///</summary>
    public static void Shake1(Transform obj, float time, float magnitude, GameObject cancelObject = null){
        Timing.RunCoroutine(_Shake1(obj, time, magnitude, obj.localPosition).CancelWith(obj.gameObject).CancelWith(cancelObject));
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
            t += Time.deltaTime;

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
    public static void Shake2(Transform obj, float time, float magnitude, Vector3 returnPos, GameObject cancelObject = null){
        Timing.RunCoroutine(_Shake2(obj, time, magnitude, returnPos).CancelWith(obj.gameObject).CancelWith(cancelObject));
    }
    private static IEnumerator<float> _Shake2(Transform obj, float time, float magnitude, Vector3 returnPos) {
        float startX = returnPos.x;
        float startY = returnPos.y;

        float t = 0;
        
        while (t < time) {
            if(Time.deltaTime == 0){
                yield return Timing.WaitForOneFrame;
                continue;
            }
    
            t += Time.deltaTime;          
            
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



    //SPRITE RENDERERS

    //Fade-----------------------------------------------------------------------------------
    ///<summary>
    ///Changes the alpha on the given renderer
    ///</summary>
    public static void FadeTo(SpriteRenderer renderer, float newAlpha, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_FadeTo(renderer, newAlpha, time, zeroToOneFunction).CancelWith(renderer.gameObject));
    }
    private static IEnumerator<float> _FadeTo(SpriteRenderer renderer, float newAlpha, float time, ZeroToOneFunction zeroToOneFunction){
        float startAlpha = renderer.color.a;
        
        float t = 0;
        while(t < time){
            t += Time.deltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            float alpha = Mathf.Lerp(startAlpha, newAlpha, modTime);
            Color c = renderer.color;
            renderer.color = new Color(c.r, c.g, c.b, alpha);

            yield return Timing.WaitForOneFrame;
        }

        Color cf = renderer.color;
        renderer.color = new Color(cf.r, cf.g, cf.b, newAlpha);
    }
    
    //Size-----------------------------------------------------------------------------------
    ///<summary>
    ///Changes the size of the given renderer
    ///</summary>
    public static void SizeTo(SpriteRenderer spriteRenderer, Vector2 newSize, float time, ZeroToOneFunction zeroToOneFunction){
        Timing.RunCoroutine(_SizeTo(spriteRenderer, newSize, time, zeroToOneFunction).CancelWith(spriteRenderer.gameObject));
    }
    private static IEnumerator<float> _SizeTo(SpriteRenderer spriteRenderer, Vector2 newSize, float time, ZeroToOneFunction zeroToOneFunction){
        float t = 0;
        Vector2 startSize = spriteRenderer.size;
        while(t < time){
            t += Time.deltaTime;
            float modTime = zeroToOneFunction.Invoke(t/time);

            spriteRenderer.size = Vector2.Lerp(startSize, newSize, modTime);

            yield return Timing.WaitForOneFrame;
        }

        spriteRenderer.size = newSize;
    }



    //GAME CHECKS

    /// <summary>
    /// Checks if the given collider is colliding with something in the gievn direction.
    /// Assumes the collider is a rectangle
    /// NOTE: Doesn't work well with irregularly shaped colliders or odd angles.
    /// DEVNOTE: Change later to accept a new parameter, n, as the number of raycasts to use
    /// </summary>
    /// <param name="collider2D"></param>
    /// <param name="rayDistance"></param>
    /// <returns></returns>
    public static bool SimpleCollisionCheck(Collider2D collider2D, Vector2 direction, float rayDistance = .1f){
        direction = Vector3.Normalize(direction);

        Vector2 halfBounds = collider2D.bounds.size / 2;
        Vector2 mid_pos = (Vector2)collider2D.transform.position + collider2D.offset*(Vector2)collider2D.transform.localScale + (halfBounds * direction);
        Vector2 perpendicular = new Vector2(direction.y, 0f - direction.x);
        
        //get layers that this entity can collide with
        LayerMask collisionLayers = Physics2D.GetLayerCollisionMask(collider2D.gameObject.layer);
        
        //Check each edge of the collider
        int[] ORDER = {-1, 1, 0};
        foreach(int i in ORDER){
            
            Vector2 newPos = mid_pos + i*halfBounds*perpendicular;

            RaycastHit2D[] rays = Physics2D.RaycastAll(newPos, direction, rayDistance, collisionLayers);
            //if there were no collisions, continue search
            if(rays.Length == 0)
                continue;
            //if the first collision was this collider
            if(rays[0].collider == collider2D){
                //if there are more colliders than this one, return true
                if(rays.Length > 1){
                    return true;
                }
            }
            //if not return true
            else
                return true;
        }

        return false;
    }

    /// <summary>
    /// Returns an array of Raycast hits from the given collider.
    /// Only hits colliders that the collider can collide with, and isn't itself
    /// DEVNOTE: Change later to accept a new parameter, n, as the number of raycasts to use
    /// </summary>
    /// <param name="collider2D"></param>
    /// <param name="direction"></param>
    /// <param name="rayDistance"></param>
    /// <returns></returns>
    public static RaycastHit2D[] CollisionsCheck(Collider2D collider2D, Vector2 direction, float rayDistance = .1f){
        direction = Vector3.Normalize(direction);
        
        Vector2 halfBounds = collider2D.bounds.size / 2;
        Vector2 mid_pos = (Vector2)collider2D.transform.position + collider2D.offset*(Vector2)collider2D.transform.localScale + (halfBounds * direction);
        Vector2 perpendicular = new Vector2(direction.y, 0f - direction.x);
        
        //get layers that this entity can collide with
        LayerMask collisionLayers = Physics2D.GetLayerCollisionMask(collider2D.gameObject.layer);
        
        RaycastHit2D[] rays = Physics2D.RaycastAll(mid_pos, direction, rayDistance, collisionLayers);

        List<RaycastHit2D> raysList = new List<RaycastHit2D>(rays);

        for(int i = 0; i < raysList.Count; i++){
            if(raysList[i].collider == collider2D){
                raysList.RemoveAt(i);
                break;
            }
        }


        return raysList.ToArray();;
    }
}
