using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;
    public MapManager mapManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI moneyText;
    public int scoreToAdd;

    public int score = 0;
    int highscore = 0;
    public int money = 0;
    int kill;
    
    void Awake(){
        instance = this;
    }
    
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "HIGHSCORE " + highscore.ToString();

        moneyText.text = money.ToString() + " MONEY";
    }

    void Update()
    {
        kill = MapManager.instance.countFinaleKill();
        if(kill > 0){
            killText.text = kill.ToString() + " ENEMIES";
        }else{
            killText.text = "GO EXIT";
        }
        
    }
    public void addPoint(){
        score += 11;
        scoreText.text = score.ToString() + " POINTS";
    }
    public void countKill(){
        killText.text = kill.ToString() + " KILL";
    }
    public void addMoney(){
        money += 1;
        moneyText.text = money.ToString() + " MONEY";
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

