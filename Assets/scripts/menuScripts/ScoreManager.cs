using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text HighScoreText;
    [SerializeField] private Text scoreText;
    private PlayerController _playerController;
    public static float score;
    private int highscore;
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        score = _playerController.points;
        highscore = (int)score;
        scoreText.text = "Points: " + highscore.ToString();
        if (PlayerPrefs.GetInt("score") <= highscore)
        {
            PlayerPrefs.SetInt("score", highscore);
        }

        HighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("score").ToString();
    }
    

}
