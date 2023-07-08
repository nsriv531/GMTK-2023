using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceLevelUpper : MonoBehaviour
{

    Transform placement;
    GameObject enemies;
    float spawnTimer = 0;
    float spawnRate = 8;

    private void Start()
    {
        
    }

    void Update()
    {
        if (spawnRate > spawnTimer)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            Instantiate(enemies, new Vector3(40, 16, 0), Quaternion.identity);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}

