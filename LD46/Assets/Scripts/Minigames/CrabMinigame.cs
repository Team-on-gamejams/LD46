using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMinigame : BaseMinigame
{

    public GameObject Crab;

    public float lineHeight = 3.5f;
    Coroutine moveCoroutine;
    int line = 0;

    CrabMinigameDifficulty difficulty;

    public override void Init(byte _usedDifficulty) {
        base.Init(_usedDifficulty);
        difficulty = difficultyBase as CrabMinigameDifficulty;
    }

    public void GoUp()
    {
        if (line < 1 && isPlaying)
        {
            ++line;
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveCoroutine(lineHeight * line, difficulty.speed));
        }

    }

    public void GoDown()
    {
        if (line > -1 && isPlaying)
        {
            --line;
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            moveCoroutine =  StartCoroutine(MoveCoroutine(lineHeight * line, -difficulty.speed));
        }
    }

    IEnumerator MoveCoroutine(float desiredY, float speed) 
    {
        float moveTime = Mathf.Abs((desiredY - Crab.transform.position.y) / speed);

        while (moveTime > 0) {
            moveTime -= Time.deltaTime;

            Crab.transform.position = new Vector3(Crab.transform.position.x, Crab.transform.position.y + speed * Time.deltaTime);
            yield return null;
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
