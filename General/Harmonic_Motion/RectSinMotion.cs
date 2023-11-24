using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectSinMotion : MonoBehaviour
{
    [SerializeField]private Vector2 amplitude;
    [SerializeField]private Vector2 period;
    [SerializeField]private Vector2 phase;

    private Vector2 startPos;
    private float startTime;
    private Vector2 timeRatio;
    // Start is called before the first frame update
    void Start()
    {
        Set(amplitude, period, phase);
    }
    public void Set(Vector2 amplitude, Vector2 period, Vector2 phase){
        this.amplitude = amplitude;
        this.period = period;
        this.phase = phase;


        startTime = Time.unscaledTime;
        startPos = transform.localPosition;

        float ratioX = period.x == 0 ? 0 : (2*Mathf.PI)/period.x;
        float ratioY = period.y == 0 ? 0 : (2*Mathf.PI)/period.y;
        timeRatio = new Vector2(ratioX, ratioY);

    }

    // Update is called once per frame
    void Update()
    {
        float elapsed = Time.unscaledTime - startTime;

        Vector2 sin = new Vector2(Mathf.Sin(timeRatio.x*(elapsed+phase.x)), Mathf.Sin(timeRatio.y*(elapsed+phase.y)));


        transform.localPosition = startPos + Vector2.Scale(sin, amplitude);
    }
}
