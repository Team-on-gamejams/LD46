using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMinigame : BaseMinigame
{

    public GameObject Crab;
    public GameObject Obstacle;
    public GameObject[] spawners;
    public Animator crabAnimator;


    //Crab
    public float lineHeight = 3.5f;
    Coroutine moveCoroutine;
    int line = 0;
    public int health = 5;

    CrabMinigameDifficulty difficulty;

    public override void Init(byte _usedDifficulty)
    {
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
            moveCoroutine = StartCoroutine(MoveCoroutine(lineHeight * line, -difficulty.speed));
        }
    }

    IEnumerator MoveCoroutine(float desiredY, float speed)
    {
        float moveTime = Mathf.Abs((desiredY - Crab.transform.position.y) / speed);

        while (moveTime > 0)
        {
            moveTime -= Time.deltaTime;

            Crab.transform.position = new Vector3(Crab.transform.position.x, Crab.transform.position.y + speed * Time.deltaTime);
            yield return null;
        }
    }
    //Crab End

    //Obstacle
    private float spawnTimeObstacle;
    public List<Sprite> AllSprites;
    GameObject obstacleClone;

    protected new void Update()
    {
        base.Update();
        if (spawnTimeObstacle <= 0 && isPlaying)
        {
            Transform activeSpawner = spawners.Random().transform;
            obstacleClone = Instantiate(Obstacle, activeSpawner.position, Quaternion.identity, activeSpawner);
            SpriteRenderer sr = obstacleClone.GetComponent<SpriteRenderer>();
            if (sr) sr.sprite = AllSprites[Random.Range(0, AllSprites.Count)];
            obstacleClone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.left * difficulty.obstacleSpeed);
            spawnTimeObstacle = difficulty.startTime;
        }
        else
        {
            spawnTimeObstacle -= Time.deltaTime;
        }
    }

    public void Lose()
    {
        Crab.transform.position = new Vector3(0, 0, 0);
        obstacleClone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.zero);
        ShowLoseAnimation();
        isPlaying = false;
    }

    protected override void ShowLoseAnimation()
    {
        Crab.GetComponent<Animator>().SetBool("Lose", true);
        LeanTween.delayedCall(3.0f, () =>
        {
            base.ShowLoseAnimation();
        });
    }

    protected override void ShowWinAnimation()
    {
        isPlaying = false;
        obstacleClone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.zero);
        Crab.transform.position = new Vector3(0, 0, 0);
        Crab.GetComponent<Animator>().SetBool("Won", true);
        LeanTween.delayedCall(2.0f, () =>
        {
            base.ShowWinAnimation();
        });
    }
}
