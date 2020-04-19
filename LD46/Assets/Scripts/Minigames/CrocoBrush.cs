using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoBrush : MonoBehaviour
{
  private bool isDragging;

  public void OnMouseDown()
  {
    isDragging = true;
    transform.GetComponent<BoxCollider2D>().enabled = false;
  }

  public void OnMouseUp()
  {
    isDragging = false;
    transform.GetComponent<BoxCollider2D>().enabled = true;
  }

  public void OnTriggerEnter2D(Collider2D colide) {   
    if (colide.gameObject.name == "Top")
    {
      isDragging = false;
      transform.Translate(new Vector3(0f,-0.5f,0));
    }

    if (colide.gameObject.name == "Bottom")
    {
      isDragging = false;
      transform.Translate(new Vector3(0f, 0.5f, 0));
    }

    if (colide.gameObject.name == "Right")
    {
      isDragging = false;
      transform.Translate(new Vector3(-0.5f, 0f, 0));
    }
  }


  // Update is called once per frame
  void Update()
    {
      if (isDragging)
      {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
      }
      
    }
}
