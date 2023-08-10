using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Patrol_Behavior : MonoBehaviour
{
    public Transform pointA;//point for start
    public Transform pointB;//point for end
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed = 5.0f; //speed for moveing
    private Vector2 distanseVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB;
        //set start point
        transform.position = pointA.position;
        distanseVector = (pointB.position - pointA.position).normalized;
    }


    void Update()
    {
        //Moving
        if (currentPoint == pointB)
        {

            rb.velocity = distanseVector * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = -distanseVector * speed * Time.fixedDeltaTime;
        }


        //Update current point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB)
        {
            currentPoint = pointA;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA)
        {
            currentPoint = pointB;
        }
    }

    //for easy drawing in edit mode(it must destroyed before main biuld)
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.position, 0.5f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }

    //private void flip()
    //{
    //    Vector3 localScale = transform.localScale;
    //    localScale.x *= -1;
    //    transform.localScale = localScale;
    //}
}
