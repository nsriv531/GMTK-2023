using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public int health;
    private int maxHealth = 100;
    public int score;
    public float time;
    public GameObject scoreBar;
    public GameObject healthBar;
    public GameObject timer;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        scoreBar.GetComponent<TextMeshProUGUI>().text = "Score " + score.ToString();
        healthBar.GetComponent<TextMeshProUGUI>().text = "Health " + health.ToString() + "/" + maxHealth.ToString();
        timer.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(time).ToString() + ":" + Mathf.RoundToInt((time - Mathf.FloorToInt(time)) * 100).ToString();
    }
}
