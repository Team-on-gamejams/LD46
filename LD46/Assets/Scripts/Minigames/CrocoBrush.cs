using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoBrush : MonoBehaviour
{
  private bool isDragging;

  public void OnMouseDown()
  {
    isDragging = true;
  }

  public void OnMouseUp()
  {
    isDragging = false;
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
