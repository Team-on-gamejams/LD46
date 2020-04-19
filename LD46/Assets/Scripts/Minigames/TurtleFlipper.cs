using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurtleFlipper : BaseMinigame
{
  public GameObject Arrow;
  public float arrowRotateSpeed = 90;
  [SerializeField] [ReorderableList] TurtleActions[] taList = null;
  
    public Vector3 getAngle()
    {
      return Arrow.transform.eulerAngles;
    }

    public void GameCondition(){
    bool result = true;
      foreach(TurtleActions ta in taList)
      {
        if (!ta.getWinState()) result = false;
      }
    if (result)
    {
      print("WON!");
    }
    else
      {
        print("Not now!");
      }
    }

    void Rotate(GameObject obj)
    {
      obj.transform.Rotate(new Vector3(0, 0, arrowRotateSpeed) * Time.deltaTime, Space.Self);
    }


    void Update()
    {
      Rotate(Arrow);
    }
  }
