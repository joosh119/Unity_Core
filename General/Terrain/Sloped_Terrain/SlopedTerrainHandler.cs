#if UNITY_EDITOR

using UnityEngine;

[ExecuteInEditMode]
public class SlopedTerrainHandler : MonoBehaviour
{
    [SerializeField]private bool useFullTexture;
    [SerializeField]private Sprite edgeTexture;
    [SerializeField]private Sprite fullTexture;

    [SerializeField]private SpriteRenderer mainRenderer;
    [SerializeField]private SpriteRenderer slopedTerrainSpice;
    [SerializeField]private SpriteRenderer straightTerrainSpice;

    [SerializeField]private float offEdgeDistance;

    // Update is called once per frame
    void Update()
    {
        //Set sprite
        if(useFullTexture)
            mainRenderer.sprite = fullTexture;
        else{
            mainRenderer.sprite = edgeTexture;
        }
        

        //Set the parent to new position of mask, and move the mask back
        Vector2 newScale = transform.localScale;
        Vector2 maskPos = transform.localPosition;
        //main texture
        mainRenderer.transform.localScale = new Vector2(1/newScale.x, 1/newScale.y);
        mainRenderer.size = newScale;

        //Sloped terrain spice
        if(slopedTerrainSpice != null){
            float newAngle = Mathf.Atan2(newScale.y, newScale.x) * Mathf.Rad2Deg;
            slopedTerrainSpice.transform.localRotation = Quaternion.Euler(0, 0, newAngle);
            //slopedTerrainSpice.transform.localScale = new Vector2(1/newScale.x, 1/newScale.y);
            slopedTerrainSpice.transform.localPosition = maskPos;
            slopedTerrainSpice.size = new Vector2(newScale.magnitude + offEdgeDistance, slopedTerrainSpice.size.y);


            float xScale = 1;
            float yScale = 1;
            if(newScale.x < 0){
                xScale = -1;
                yScale = -1;
            }
            slopedTerrainSpice.transform.localScale = new Vector2(xScale, yScale);
            
        }

        //Normal terrain spice
        if(straightTerrainSpice != null){
            //straightTerrainSpice.transform.localScale = new Vector2(1/newScale.x, 1/newScale.y);
            straightTerrainSpice.size = new Vector2(newScale.x + offEdgeDistance, straightTerrainSpice.size.y);
            //float yPos = -.6f +  (.1f/newScale.y) * (newScale.y-1);
            //straightTerrainSpice.transform.localPosition = new Vector2(0, yPos);
            straightTerrainSpice.transform.localPosition = new Vector2(maskPos.x, maskPos.y - (.6f*newScale.y) + (.1f*(newScale.y-1)));
        }
    }
}

#endif