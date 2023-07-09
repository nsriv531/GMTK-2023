using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public int health;
    private int maxHealth = 100;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
