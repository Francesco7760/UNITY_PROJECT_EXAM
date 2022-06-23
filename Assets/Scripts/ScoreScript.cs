using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int scoreToAdd;

    int score = 0;
    int highscore = 0;
    //int kill = 0;
    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "HIGHSCORE " + highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addPoint(){
        score += 11;
        scoreText.text = score.ToString() + " POINTS";
    }
    public void countKill(){
        //kill = 1;
        scoreText.text = score.ToString() + " POINTS";
        if(highscore < score){
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void addPointFood(){
        score += 11;
        scoreText.text = score.ToString() + " POINTS";
    }
    public void addPointSoda(){
        score += 11;
        scoreText.text = score.ToString() + " POINTS";
    }
}

