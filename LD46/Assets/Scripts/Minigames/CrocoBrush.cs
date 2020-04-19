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
      colide.GetComponent<SpriteRenderer>().color = colide.GetComponent<SpriteRenderer>().color - new Color(0, 0, 0, 0.05f);

      if (colide.GetComponent<SpriteRenderer>().color.a <= 0.3f)
      {
        colide.gameObject.active = false;
      }
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
