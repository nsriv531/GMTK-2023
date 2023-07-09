using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public int score;
    public float time;
    public GameObject scoreBar;
    public GameObject healthBar;
    public GameObject timer;
    public GameObject endMSG;
    public GameObject game;
    public GameObject continueButton;
    public GameObject retryButton;
    public bool gameFinished = false;
    public string current = "Game Level 1";


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth) {
            health = maxHealth;
        }
        if (!gameFinished) {
            time += Time.deltaTime;
        }
        scoreBar.GetComponent<TextMeshProUGUI>().text = "Score " + score.ToString();
        healthBar.GetComponent<TextMeshProUGUI>().text = "Health " + health.ToString() + "/" + maxHealth.ToString();
        timer.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(time).ToString() + ":" + Mathf.RoundToInt((time - Mathf.FloorToInt(time)) * 100).ToString();
        if (health <= 0) {
            gameFinished = true;
            Debug.Log("Game Over");
            endMSG.GetComponent<TextMeshProUGUI>().enabled = true;
            retryButton.SetActive(true);
            endMSG.GetComponent<TextMeshProUGUI>().text = "Game Over";
            game.SetActive(false);
        }
        if (time > 100)
        {
            gameFinished = true;
            Debug.Log("Vicotry");
            endMSG.GetComponent<TextMeshProUGUI>().enabled = true;
            continueButton.SetActive(true);
            retryButton.SetActive(true);
            endMSG.GetComponent<TextMeshProUGUI>().text = "You Win";
            game.SetActive(false);
        }
    }

    public void continuing()
    {
        gameFinished = false;
        endMSG.GetComponent<TextMeshProUGUI>().enabled = false;
        continueButton.SetActive(false);
        retryButton.SetActive(false);
        game.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
