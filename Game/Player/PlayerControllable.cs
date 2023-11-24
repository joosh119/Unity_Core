
using UnityEngine;


public abstract class PlayerControllable: MonoBehaviour
{
    //Add and remove controller from list in input manager
    public virtual void Start(){
        InputManager.instance.playersControllers.Add(this);
        //awake = true;
    }
    public virtual void OnDestroy(){
        InputManager.instance.playersControllers.Remove(this);
    }


    public bool awake;//Whether or not the object is accepting actions
    public void SetAwake(bool awake){
        this.awake = awake;
    }

    public virtual void DirectionalArrow(bool justPressed, bool isPressed, Vector2 direction){

    }
    public virtual void Space(bool justPressed, bool isPressed){

    }
    public virtual void RightMouseClick(bool justPressed, bool isPressed){

    }
    public virtual void LeftMouseClick(bool justPressed, bool isPressed){

    }

}
