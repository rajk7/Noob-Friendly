using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManages : MonoBehaviour
{
    public static LevelManages Instance {set;get;}
    private int hitpoint = 3;
    private int score = 0;

    public Text scoreText;
    public Text hitpointText;

    public Transform spawnPosition;
    public Transform playerTransform;
     
    private void Awake() 
    {
        Instance = this;
        scoreText.text = "Current Score : " + score.ToString();
        hitpointText.text = "Hitpoint : " + hitpoint.ToString();
    }

    private void Update()
    {
        //scoreText.text = "Current Score" + score.ToString();
        if(playerTransform.position.y < -10)
        {
            playerTransform.position = spawnPosition.position;
            hitpoint--;
            hitpointText.text = "" +hitpoint.ToString();
            if(hitpoint <= 0)
            {
                Application.LoadLevel("Menu");
            }
        }
    }
    public void Win() {
        {
            if(score > PlayerPrefs.GetInt ("PlayerScore"))
            {
                PlayerPrefs.SetInt("PlayerScore", score);
            }
            Application.LoadLevel("Menu");
        }
    }
    public void CollectCoin()
    {
        score++;
        scoreText.text = "Current score : " +score.ToString();
    }
}
