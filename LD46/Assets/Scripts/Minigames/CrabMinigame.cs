using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrabMinigame : BaseMinigame
{

    public GameObject Crab;
    public GameObject Obstacle;
    public GameObject[] spawners;
    public Animator crabAnimator;
    public AudioClip winClip;
    public GameObject BlackLines;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI LoseText;

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
        if (!isPlaying)
            return;
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        Crab.transform.position = new Vector3(0, 0, 0);
        Destroy(obstacleClone.gameObject);
        ShowLoseAnimation();
        isPlaying = false;
        LeanTweenEx.ChangeTextAlpha(LoseText, 1.0f, 0.2f);
    }

    protected override void ShowLoseAnimation()
    {
        BlackLines.SetActive(false);
        crabAnimator.SetBool("Lose", true);
        LeanTween.delayedCall(3.0f, () =>
        {
            base.ShowLoseAnimation();
        });
    }

    protected override void ShowWinAnimation()
    {
        BlackLines.SetActive(false);
        LeanTweenEx.ChangeTextAlpha(winText, 1.0f, 0.2f);
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        isPlaying = false;
        Destroy(obstacleClone.gameObject);
        Crab.transform.position = new Vector3(0, 0, 0);
        crabAnimator.SetBool("Won", true);

        float volume = AudioManager.Instance.GetVolume(AudioManager.AudioChannel.Music);
        AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, 0.0f);
        AudioManager.Instance.Play(winClip, channel: AudioManager.AudioChannel.Sound);
        LeanTween.delayedCall(4.0f, () =>
        {
            AudioManager.Instance.SetVolume(AudioManager.AudioChannel.Music, volume);
            base.ShowWinAnimation();
        });
    }
}
