using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Singleton pattern
    public static InputManager instance;
    void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    public List<PlayerControllable> playersControllers = new List<PlayerControllable>();
    public bool active;


    //Pushes all updates to player controllable objects in list
    void Update()
    {   
        if(!active)
            return;

            
        //UP
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            DirectionalArrowActions(true, true, Vector2.up);
        }
        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            DirectionalArrowActions(false, true, Vector2.up);
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
            DirectionalArrowActions(false, false, Vector2.up);
        }

        //DOWN
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            DirectionalArrowActions(true, true, Vector2.down);
        }
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            DirectionalArrowActions(false, true, Vector2.down);
        }
        else if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            DirectionalArrowActions(false, false, Vector2.down);
        }

        //LEFT
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            DirectionalArrowActions(true, true, Vector2.left);
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            DirectionalArrowActions(false, true, Vector2.left);
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
            DirectionalArrowActions(false, false, Vector2.left);
        }

        //RIGHT
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            DirectionalArrowActions(true, true, Vector2.right);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            DirectionalArrowActions(false, true, Vector2.right);
        }
        else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)){
            DirectionalArrowActions(false, false, Vector2.right);
        }


        //SPACE
        if(Input.GetKeyDown(KeyCode.Space)){
            SpaceActions(true, true);
        }
        else if(Input.GetKey(KeyCode.Space)){
            SpaceActions(false, true);
        }
        else if(Input.GetKeyUp(KeyCode.Space)){
            SpaceActions(false, false);
        }


        //RIGHT MOUSE
        if(Input.GetMouseButtonDown(1)){
            RightMouseClickActions(true, true);
        }
        else if(Input.GetMouseButton(1)){
            RightMouseClickActions(false, true);
        }
        else if(Input.GetMouseButtonUp(1)){
            RightMouseClickActions(false, false);
        }

        //LEFT MOUSE
        if(Input.GetMouseButtonDown(0)){
            LeftMouseClickActions(true, true);
        }
        else if(Input.GetMouseButton(0)){
            LeftMouseClickActions(false, true);
        }
        else if(Input.GetMouseButtonUp(0)){
            LeftMouseClickActions(false, false);
        }


    }


    private void DirectionalArrowActions(bool justPressed, bool isPressed, Vector2 direction){
        foreach(PlayerControllable playerController in playersControllers){
            if(playerController.awake)
                playerController.DirectionalArrow(justPressed, isPressed, direction);
        }
    }

    private void SpaceActions(bool justPressed, bool isPressed){
        foreach(PlayerControllable playerController in playersControllers){
            if(playerController.awake)
                playerController.Space(justPressed, isPressed);
        }
    }

    private void RightMouseClickActions(bool justPressed, bool isPressed){
        foreach(PlayerControllable playerController in playersControllers){
            if(playerController.awake)
                playerController.RightMouseClick(justPressed, isPressed);
        }
    }
    private void LeftMouseClickActions(bool justPressed, bool isPressed){
        foreach(PlayerControllable playerController in playersControllers){
            if(playerController.awake)
                playerController.LeftMouseClick(justPressed, isPressed);
        }
    }



    //Getters
    private static Vector2 worldSpacePosition;
    private static float lastTimeChecked;
    public static Vector2 mouseWorldSpacePosition {
        get{
            if(Time.unscaledDeltaTime != lastTimeChecked){
                lastTimeChecked = Time.unscaledDeltaTime;

                Vector2 mousePos = Input.mousePosition;
                worldSpacePosition = Camera.main.ScreenToWorldPoint(mousePos);
            }
            
            return worldSpacePosition;
        }
    }
}
