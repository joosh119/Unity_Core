using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonicMotion : MonoBehaviour
{
    public Vector3 amplitude1;
    public Vector3 amplitude2;
    public Vector3 timeOffset;//not needed, only for setting manually in editor
    public Vector3 period;//not needed, only for setting manually in editor
    private Vector3 lastPos;//previous sin position

    //'Constants'
    private Vector3 periodRatio;// 2*PI / period
    private Vector3 k;//slopes
    private Vector3 yMax;//max y from slopes
    private float startTime;
    private const float twoPI = Mathf.PI*2;
    private const float PItwo = Mathf.PI/2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        setBob(amplitude1, amplitude2, period);
        startTime = Time.time;
    }
    public void setBob(Vector3 amplitude1, Vector3 amplitude2, Vector3 period){
        //Set base values
        this.amplitude1 = amplitude1;
        this.amplitude2 = amplitude2;
        this.period = period;
        

        //Set ratios that converts time in seconds into radians
        float xRatio = 0;
        float yRatio = 0;
        float zRatio = 0;
        if(period.x != 0)
            xRatio = twoPI/period.x;
        if(period.y != 0)
            yRatio = twoPI/period.y;
        if(period.z != 0)
            zRatio = twoPI/period.z;

        this.periodRatio = new Vector3(xRatio, yRatio, zRatio);

        //start position; each starting point is the minimum value of the sin wave
        lastPos = -amplitude1;
        
        //float k = amplitude2.x/Mathf.Asin(amplitude2.x/amplitude1.x);
        k = new Vector3(amplitude2.x/Mathf.Asin(amplitude2.x/amplitude1.x), amplitude2.y/Mathf.Asin(amplitude2.y/amplitude1.y), amplitude2.z/Mathf.Asin(amplitude2.z/amplitude1.z));
        //float yMax = k*(Mathf.PI/2);
        yMax = new Vector3(k.x*PItwo, k.y*PItwo, k.z*PItwo);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float elapsedTime = Time.time-startTime;
        //float mod_time = ((periodRatio.x*Time.time)%(2*Mathf.PI))-(Mathf.PI/2);
        Vector3 mod_time = new Vector3(((periodRatio.x*(timeOffset.x + elapsedTime))%twoPI)-PItwo, ((periodRatio.y*(timeOffset.y + elapsedTime))%twoPI)-PItwo, ((periodRatio.z*(timeOffset.x + elapsedTime))%twoPI)-PItwo);        
        Vector3 sin = new Vector3(Mathf.Sin(mod_time.x), Mathf.Sin(mod_time.y), Mathf.Sin(mod_time.z));
        
        
        //Position on curve
        Vector3 ampPos = Vector3.Scale(amplitude1, sin);
        

        //If ampPos on sin is within amplitude2
        //x
        if(Mathf.Abs(ampPos.x) < amplitude2.x){
            float sign = Mathf.Sign(Mathf.Cos(mod_time.x));
            ampPos.x = (sign*k.x*(mod_time.x-PItwo))+yMax.x;
        }
        //y
        if(Mathf.Abs(ampPos.y) < amplitude2.y){
            float sign = Mathf.Sign(Mathf.Cos(mod_time.y));
            ampPos.y = (sign*k.y*(mod_time.y-PItwo))+yMax.y;
        }
        //z
        if(Mathf.Abs(ampPos.z) < amplitude2.z){
            float sign = Mathf.Sign(Mathf.Cos(mod_time.z));
            ampPos.z = (sign*k.z*(mod_time.z-PItwo))+yMax.z;
        }


        //Add change in position
        //deltaPos = ampPos-lastPos;
        transform.position += (ampPos-lastPos);
        lastPos = ampPos;
    }
}
