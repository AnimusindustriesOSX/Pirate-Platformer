using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    GameObject floor;
    public Transform [] spawnPoints;
    public GameObject[] enemyPrefabs;
    public DayNightCycleController dayNightCycleController;
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("Floor");
        for(int i = 0; i < 8; i++){
            spawnPoints[i] = floor.transform.GetChild(i);
        }
        dayNightCycleController = GameObject.FindGameObjectWithTag("Time").GetComponent<DayNightCycleController>();
    }

    private void Update() {
        if(dayNightCycleController.isNight){
            SpawnEnemies(5);
            dayNightCycleController.isNight = false;
        }
    }

    private void SpawnEnemies(int enemiesPerArea)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            // Ensure there are enemy prefabs to spawn
            if (enemyPrefabs.Length == 0)
            {
                Debug.Log("No Prefabs found");
                return;
            }
            for (int i = 0; i < enemiesPerArea; i++){
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];
                Instantiate(selectedEnemyPrefab, spawnPoint.position + new Vector3(Random.Range(-50,50),Random.Range(-25,25)), Quaternion.identity);
            }
        }
    }
}
