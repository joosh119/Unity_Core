#if UNITY_EDITOR

using UnityEngine;

[ExecuteInEditMode]
public class ChildColliderHandler : MonoBehaviour
{

    
    [SerializeField]private SpriteRenderer parentRenderer;
    [SerializeField]private BoxCollider2D childCollider;

    [SerializeField]private Vector2 minAnchor;
    [SerializeField]private Vector2 maxAnchor;

    // Update is called once per frame
    void Update()
    {
        Vector2 parentSize = parentRenderer.size;
        //Vector2 ratio = parentSize/baseParentSize;
        //Vector2 baseDiff = baseParentSize - baseChildSize;
//
        //childCollider.size = baseChildSize*ratio ;// +  baseDiff * ratio;
        Vector2 size = parentSize - (maxAnchor + minAnchor);
        childCollider.size = size;
        Vector2 offset = (minAnchor-maxAnchor)/2;
        childCollider.offset = offset;
    }
}

#endif