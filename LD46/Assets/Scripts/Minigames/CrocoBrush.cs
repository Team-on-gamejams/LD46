using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoBrush : MonoBehaviour
{
  private bool isDragging;
  private Vector2 mousePosition;

  Vector3 mouseCurrLocation, diff;
  Vector3 currVelocity;
  Vector3 force;



  public void OnMouseDown()
  {
    isDragging = true;
  }

  public void OnMouseUp()
  {
    isDragging = false;
    transform.GetComponent<Rigidbody2D>().velocity = new Vector2();
  }

  public void OnTriggerEnter2D(Collider2D colide) {   


    if (colide.gameObject.name.Contains("Caries"))
    {
      colide.gameObject.active = false;
    }
  }




  public void FixedUpdate()
  {

    if (isDragging)
    {
      mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

      transform.GetComponent<Rigidbody2D>().velocity = mousePosition * 5;
    }

  }


}
