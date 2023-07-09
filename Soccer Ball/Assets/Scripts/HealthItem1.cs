using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HealthItem1 : MonoBehaviour
{
    public GameObject ball;
    public GameObject ui;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == ball)
        {
            Debug.Log("hit ball");
            ui.GetComponent<GameUI>().health += 10;
            ball.GetComponent<PlayerBallSliding>().controllCoolDown = 0;
            Destroy(gameObject);
        }
    }
}
