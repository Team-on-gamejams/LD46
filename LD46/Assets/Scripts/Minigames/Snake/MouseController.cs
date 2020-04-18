using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Vector3 gameObjectSreenPoint;
    public Vector3 mousePreviousLocation;
    public Vector3 mouseCurLocation;
    private bool dragging = false;
    private float distance;


    void OnMouseEnter()
    {
    }

    void OnMouseExit()
    {
    }

    void OnMouseDown()
    {
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        dragging = true;
    }

    public Vector3 force;
    public Vector3 objectCurrentPosition;
    public Vector3 objectTargetPosition;
    public float topSpeed = 10;

    void OnMouseDrag()
    {
        mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
        mousePreviousLocation = mouseCurLocation;
    }

    void OnMouseUp()
    {
        dragging = false;
        if (GetComponent<Rigidbody2D>().velocity.magnitude > topSpeed)
            force = GetComponent<Rigidbody2D>().velocity.normalized * topSpeed;
    }

    public void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = force;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }
}
