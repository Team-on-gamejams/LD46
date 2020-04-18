using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurtleActions : MonoBehaviour
{
  public Transform Turtle;
  public TurtleFlipper angle;
  public float winAngle;
  private bool isJumping = false;
  private bool isFaling = false;

  private float startPosition;

  void Setup()
  {
    startPosition = Turtle.transform.position.y;
  }

  void Fly()
  {
    if (isJumping)
    {
      Turtle.transform.position += new Vector3(0, 14, 0) * Time.deltaTime;
      return;
    }
    
    if (isFaling)
    {
      Turtle.transform.position -= new Vector3(0, 12, 0) * Time.deltaTime;
      return;
    }
    

  }

  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.name == "MaxHeight") isJumping = false;
    if (collision.gameObject.name == "MinHeight") isFaling = false;
  }

  void OnMouseDown()
  {
    if (gameObject.tag == "Turtle")
    {
      if (!isFaling && !isJumping) { isJumping = true; isFaling = true; }
      Vector3 curRotation = angle.getAngle();
      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
      RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

      float rndAng = Random.RandomRange(-100f, 100f);

      if (curRotation.z <= winAngle || curRotation.z >= 360 - winAngle)
      {
        Turtle.transform.rotation = Quaternion.Euler(curRotation);
        Vector3 startPosition = Turtle.transform.position;

      }
      else
      {
        Turtle.transform.rotation = new Quaternion(0f, 0f, rndAng, 1f);
      }
    }
    else
    {
      Debug.Log("Did not hit anything");
    }
  }


  void Update()
    {

    Fly();

  }
}
