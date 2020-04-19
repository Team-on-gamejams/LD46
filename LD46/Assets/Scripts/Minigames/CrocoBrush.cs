using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoBrush : MonoBehaviour
{
  private bool isDragging;
  private Vector2 mousePosition;
  public void OnMouseDown()
  {
    isDragging = true;
    transform.GetComponent<BoxCollider2D>().enabled = false;
  }

  public void OnMouseUp()
  {
    isDragging = false;
    transform.GetComponent<BoxCollider2D>().enabled = true;
    transform.GetComponent<Rigidbody2D>().velocity = new Vector2();
  }

  public void OnTriggerEnter2D(Collider2D colide) {   
    if (colide.gameObject.name == "Top")
    {
      //isDragging = false;
      transform.GetComponent<Rigidbody2D>().velocity *= -1.2f;
    }

    if (colide.gameObject.name == "Bottom")
    {

      transform.GetComponent<Rigidbody2D>().velocity *= -1.2f;
      ;
    }

    if (colide.gameObject.name == "Right")
    {
      transform.GetComponent<Rigidbody2D>().velocity *= -1.2f;

    }
    if (colide.gameObject.name.Contains("Caries"))
    {
      colide.gameObject.active = false;
    }
  }


  // Update is called once per frame
  void Update()
    {
      if (isDragging)
      {
      mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      //transform.Translate(mousePosition);
      transform.GetComponent<Rigidbody2D>().velocity = mousePosition * 5;
      }
      
    }
}
