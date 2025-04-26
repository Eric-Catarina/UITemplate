using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<SoundManager>().PlayMusic(6);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        gameManager.GetComponent<SceneManagerCustom>().LoadScene0();
    }
}
