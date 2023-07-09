using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSpawners : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap map;
    public int healthSpawnrate;
    public int enemySpawnrate;
    public GameObject healthPickUp1;
    public GameObject enemies;
    public float timer = 0;
    public float spawnTick = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            int spawnType = Random.Range(1, 1000);
            Vector3 spawnPos = Random.insideUnitCircle * 22;
            Debug.Log("Spawn Attempt" + spawnType + ", " + spawnPos);
            if ((spawnType % enemySpawnrate) == 0)
            {
                Instantiate(healthPickUp1, spawnPos, Quaternion.identity);
            }
            if ((spawnType % healthSpawnrate) == 0)
            {
                Instantiate(enemies, spawnPos, Quaternion.identity);
            }
            timer = spawnTick;
        }
        else {
            timer -= Time.deltaTime;
        }
    }
}
