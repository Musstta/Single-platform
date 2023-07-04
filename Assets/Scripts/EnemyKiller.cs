using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKiller : MonoBehaviour
{ 
   

    public Text ScoreText;
     int Score;
    int highscore;
    public Text highScoreText;
 

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Score = Score + 1;

            ScoreText.text = "Score : " + Score.ToString();
            highscore = PlayerPrefs.GetInt("highscore", 0);
            if (highscore < Score)
            {
                PlayerPrefs.SetInt("HighScore", Score);

            }
            highScoreText.text = "HIGHSCORE: " + highscore;
            Destroy(other.gameObject);
            
        }
        if (other.CompareTag("BadElement"))
        {

            Character2DController.isGameOver = true;

        }
       
    }
}
