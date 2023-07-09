using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public int health;
    private int maxHealth = 100;
    public int score;
    public GameObject scoreBar;
    public GameObject healthBar;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreBar.GetComponent<TextMeshProUGUI>().text = "Score " + score.ToString();
        healthBar.GetComponent<TextMeshProUGUI>().text = "Health " + health.ToString() + "/" + maxHealth.ToString();
    }
}
