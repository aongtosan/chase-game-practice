using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGennerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject powerUpPrefabs;
    Coroutine cooldown;

    // Update is called once per frame
    void Update()
    {        
        if(GameObject.FindObjectsOfType<PowerUpController>().Length<GameObject.FindObjectsOfType<EnemyController>().Length){
            if(cooldown==null){
                cooldown =  StartCoroutine(nameof(powerUpGenerateCoolDown));
                powerUpDropping();
            }
        }   
    }
    void powerUpDropping(){
            Vector3 spawnPosition = new Vector3(
                Random.Range(GameObject.Find("base").transform.position.x-10,GameObject.Find("base").transform.position.x+10),
                powerUpPrefabs.transform.position.y
                , Random.Range(GameObject.Find("base").transform.position.z-10,GameObject.Find("base").transform.position.z+10)
            );
        Instantiate(powerUpPrefabs,spawnPosition,powerUpPrefabs.transform.rotation);
    }
    IEnumerator powerUpGenerateCoolDown(){
        yield return new WaitForSeconds(4.0f);
        cooldown = null;
    }
}
