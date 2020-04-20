using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : CrabMinigame
{
    public List<Sprite> AllSprites;
    void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr) sr.sprite = AllSprites[Random.Range(0, AllSprites.Count)];
    }

    private void Update()
    {
        transform.Translate(Vector3.left * obstacleSpeed * Time.deltaTime);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name=="crab" || other.gameObject.name == "die")
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}
