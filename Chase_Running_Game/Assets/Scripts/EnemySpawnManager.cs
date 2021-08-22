using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefabs;
    private int enemyThreshold = 0;
    private int waveEnemy;
    void Awake(){
         waveEnemy = 1;
         enabled = false;
    }
    void Update(){
        enemyThreshold = GameObject.FindObjectsOfType<EnemyController>().Length;
        if(enemyThreshold==0){
            waveEnemy++;
            enemySpawner();
        }
    }
    public void StartSpawnEnemy(){
        enabled = true;
        enemySpawner();
    }
    void enemySpawner(){
        for(var i =0 ; i< waveEnemy ; i++){
            Vector3 spawnPosition = new Vector3(
                Random.Range(GameObject.Find("base").transform.position.x-10,GameObject.Find("base").transform.position.x+10),
                enemyPrefabs.transform.position.y
                , Random.Range(GameObject.Find("base").transform.position.z-10,GameObject.Find("base").transform.position.z+10)
            );
            Instantiate(enemyPrefabs,spawnPosition,enemyPrefabs.transform.rotation);
        }
    }
}
