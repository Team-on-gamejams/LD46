using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurtleFlipper : MonoBehaviour
{
  public GameObject Arrow;

    public Vector3 getAngle()
      {
        return Arrow.transform.eulerAngles;
      }

    void Rotate(GameObject obj)
    {
      obj.transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime, Space.Self);
    }

    void Update()
    {
      Rotate(Arrow);
    }
  }
