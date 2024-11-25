using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public int score = 0;
    public int money = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score added: " + value + ". New score: " + score);
        UpdateScoreUI();

    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Money added: " + amount + ". Amount Of Money " + money);
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + money;
        }
    }


}
