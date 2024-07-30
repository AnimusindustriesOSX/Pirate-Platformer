using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    GameObject floor;
    public Transform [] spawnPoints;
    public GameObject[] enemyPrefabs;
    public DayNightCycleController dayNightCycleController;
    public int enemiesPerArea = 5;
    public int additionalEnemiesPerAreaWhenInsane = 1;
    GameObject wave;
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
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().insanity == 100){
                SpawnEnemiesInsane(enemiesPerArea + additionalEnemiesPerAreaWhenInsane);
            }else{
                SpawnEnemies(enemiesPerArea);
            }
            dayNightCycleController.isNight = false;
        }
    }

    private void SpawnEnemies(int enemiesPerArea)
    {
        if(wave!=null){
            Destroy(wave);
        }
        wave = new GameObject("Wave");
        foreach (var spawnPoint in spawnPoints)
        {
            // Ensure there are enemy prefabs to spawn
            if (enemyPrefabs.Length == 0)
            {
                Debug.Log("No Prefabs found");
                return;
            }
            for (int i = 0; i < enemiesPerArea; i++){
                int randomIndex = Random.Range(0, enemyPrefabs.Length-1);
                GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];
                Instantiate(selectedEnemyPrefab, spawnPoint.position + new Vector3(Random.Range(-50,50),Random.Range(-25,25)), Quaternion.identity).transform.SetParent(wave.transform);
            }
        }
    }

    private void SpawnEnemiesInsane(int enemiesPerArea)
    {
        if(wave!=null){
            Destroy(wave);
        }
        wave = new GameObject("Wave");
        foreach (var spawnPoint in spawnPoints)
        {
            // Ensure there are enemy prefabs to spawn
            if (enemyPrefabs.Length == 0)
            {
                Debug.Log("No Prefabs found");
                return;
            }
            for (int i = 0; i < enemiesPerArea; i++){
                int randomIndex = Random.Range(1, enemyPrefabs.Length);
                GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];
                Instantiate(selectedEnemyPrefab, spawnPoint.position + new Vector3(Random.Range(-50,50),Random.Range(-25,25)), Quaternion.identity).transform.SetParent(wave.transform);
            }
        }
    }
}
