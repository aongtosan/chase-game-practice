using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startScreen;
    public UnityEvent OnGameStart;
    private EnemySpawnManager enemyGenerator;
    private PlayerController player;
    void Start()
    {
      enemyGenerator = FindObjectOfType<EnemySpawnManager>();
     // player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(){
        enemyGenerator.StartSpawnEnemy();
        OnGameStart.Invoke();
        startScreen.SetActive(false);
    }
}
