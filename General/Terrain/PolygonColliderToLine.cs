using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonColliderToLine : MonoBehaviour{
    private PolygonCollider2D polygonCollider;
    private LineRenderer lineRenderer;
    [SerializeField]private bool clockwise;
    [SerializeField]private float inwardOffset;
    [SerializeField]private uint totalPoints;
    [SerializeField]private uint pointsOffset;
    [SerializeField]private bool useAllPoints;
    [SerializeField]private bool loop;


    // In-editor, poll for collider updates so we can react 
    // to shape changes with realtime interactivity.
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if(lineRenderer == null){
            Initialize();
        }
        else {
            Reshape();
        }
    }
#endif


    private void Initialize(){
        polygonCollider = GetComponent<PolygonCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();


        Reshape();
    }


    private void Reshape(){
        

        //get polygoncollider points
        Vector2[] points2 = polygonCollider.points;
        int totalPolygonPoints = points2.Length;


        if(useAllPoints){
            totalPoints = (uint)totalPolygonPoints;
            pointsOffset = 0;
        }


        if(pointsOffset > totalPolygonPoints)
            pointsOffset = (uint)totalPolygonPoints;
        if(totalPoints > totalPolygonPoints - pointsOffset){
            totalPoints = (uint)totalPolygonPoints - pointsOffset;
        }
        else if(totalPoints == totalPolygonPoints-1){
            totalPoints = (uint)totalPolygonPoints;
        }

        //convert vector2[] to vector3[]
        Vector3[] points3 = new Vector3[totalPoints];
        for(uint i = pointsOffset; i < pointsOffset+totalPoints; i++){
            points3[i - pointsOffset] = points2[i];
        }

        lineRenderer.positionCount = (int)totalPoints;
        lineRenderer.SetPositions(MyPointsScaledWithinPolygon(points3, inwardOffset));
        lineRenderer.loop = loop;
        
    }






    public static Vector3[] MyPointsScaledWithinPolygon(Vector3[] polygonPoints, float distance)
    {
        if (polygonPoints.Length < 3)
            throw new ArgumentException("Polygon must have at least 3 points.");

        Vector3[] returnPoints = new Vector3[polygonPoints.Length];

        for (int i = 0; i < polygonPoints.Length; i++)
        {
            
            Vector3 currentPoint = polygonPoints[i];
            Vector3 prevPoint = polygonPoints[(i + polygonPoints.Length - 1) % polygonPoints.Length];
            Vector3 nextPoint = polygonPoints[(i + 1) % polygonPoints.Length];

            
            Vector2 side1 = Vector3.Normalize(currentPoint - prevPoint);
            Vector2 side2 = Vector3.Normalize(nextPoint - currentPoint);

            Vector2 normal1 = new Vector2(-side1.y, side1.x);
            Vector2 normal2 = new Vector2(-side2.y, side2.x);

            Vector3 direction = Vector3.Normalize(normal1 + normal2);


            //check if lines intersect
            //if(LineSegmentsIntersect(currentPoint, nextPoint, currentPoint + distance*direction))
            //    direction*=-1;


            returnPoints[i] = currentPoint + distance*direction;
        }

        //Vector3 center = GetCenter(polygonPoints);
        //for (int i = 0; i < polygonPoints.Length; i++){
        //    Vector3 newPoint = center +  distance*(polygonPoints[i] - center);
        //    returnPoints[i] = newPoint;
        //}


        return returnPoints;
    }
 
}
