#if UNITY_EDITOR

using UnityEngine;

[ExecuteInEditMode]
public class TerrainHandler : MonoBehaviour
{
    [SerializeField]private bool useFullTexture;
    [SerializeField]private Sprite edgeTexture;
    [SerializeField]private Sprite fullTexture;

    
    [SerializeField]private SpriteRenderer mainRenderer;
    [SerializeField]private SpriteRenderer topTerrainSpice;
    [SerializeField]private SpriteRenderer bottomTerrainSpice;

    [SerializeField]private float offEdgeDistance;//how far off the sides it goes
    [SerializeField]private float topExtendDistance;//How far off the top the top element goes
    [SerializeField]private float bottomExtendDistance;//How far off the bottom the bottom element goes

    // Update is called once per frame
    void Update()
    {
        //Set sprite
        if(useFullTexture)
            mainRenderer.sprite = fullTexture;
        else{
            mainRenderer.sprite = edgeTexture;
        }



        Vector2 mainSize = mainRenderer.size;

        //top
        if(topTerrainSpice != null){
            float topYSize = topTerrainSpice.size.y;
            topTerrainSpice.size = new Vector2(mainSize.x + offEdgeDistance, topYSize);
            topTerrainSpice.transform.localPosition = new Vector2(0, mainSize.y/2 - (topYSize/2) + topExtendDistance);
        }

        //bottom
        if(bottomTerrainSpice != null){
            float bottomYSize = bottomTerrainSpice.size.y;
            bottomTerrainSpice.size = new Vector2(mainSize.x + offEdgeDistance, bottomTerrainSpice.size.y);
            bottomTerrainSpice.transform.localPosition = new Vector2(0, -mainSize.y/2 + (bottomYSize/2) - bottomExtendDistance);
        }
    }
}

#endif