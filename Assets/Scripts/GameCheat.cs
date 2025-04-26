using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameCheat : MonoBehaviour
{

    private SceneManagerCustom sceneManager;
    void Start()
    {
        sceneManager = GetComponent<SceneManagerCustom>();
    }
    void Update()
    {
        // Change scene when player press "1"
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadScene0();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            LoadSceneGameOver();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            InfiniteLife();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SpawnBoss();
        }

    }

    public  void LoadScene1()
    {
        sceneManager.LoadScene1();
    }
    
    public  void LoadScene2()
    {
        sceneManager.LoadScene2();
    }

    public void LoadScene0()
    {
        sceneManager.LoadScene0();
    }

    public void LoadSceneGameOver()
    {
        sceneManager.LoadSceneGameOver();
    }

    public void InfiniteLife()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.IncreaseMaxHealth(9999);
        player.IncreaseHealth(9999);
    }

    public void SpawnBoss(){
        GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>().SpawnBossCheat();
        GameObject.Find("EnemyGenerator").SetActive(false);
    }

}
