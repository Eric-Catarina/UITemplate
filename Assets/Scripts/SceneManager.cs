using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerCustom : MonoBehaviour
{
    [SerializeField]
    private  SoundManager soundManager;

    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }
    public  void LoadScene1()
    {
        soundManager.PlayMusic(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fase1");
    }

    
    public  void LoadScene2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fase2");
    }

    public  void LoadScene0()
    {
        soundManager.PlayMusic(0);

        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuInicial");
    }


    public  void LoadSceneGameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public static  void QuitGame()
    {
        Application.Quit();
    }
}
