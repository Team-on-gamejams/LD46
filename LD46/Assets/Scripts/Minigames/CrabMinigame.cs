using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMinigame : BaseBaseMinigame
{

    public GameObject Crab;

    private Vector2 targetPos;
    public float Range = 30;
    public float maxHeight;
    public float minHeight;
    public float speed;

    public void GoUp()
    {
        if (Crab.transform.position.y < maxHeight)
        {
            targetPos = new Vector2(Crab.transform.position.x, Crab.transform.position.y + Range);
            //Crab.transform.position = targetPos;
            Crab.transform.position = Vector2.MoveTowards(Crab.transform.position, targetPos, speed * Time.deltaTime);
        }

    }

    public void GoDown()
    {
        if (Crab.transform.position.y > minHeight)
        {
            targetPos = new Vector2(Crab.transform.position.x, Crab.transform.position.y - Range);
            Crab.transform.position = targetPos;
        }
    }

    public void Win()
    {
        isPlaying = false;
        ShowWinAnimation();
    }

    protected override void ShowLoseAnimation()
    {
        LeanTween.delayedCall(1.0f, () =>
        {
            base.ShowLoseAnimation();
        });
    }

    protected override void ShowWinAnimation()
    {
        LeanTween.delayedCall(1.0f, () =>
        {
            base.ShowWinAnimation();
        });
    }
}
