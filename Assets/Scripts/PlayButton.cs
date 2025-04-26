using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    public void Play()
    {
        gameManager.GetComponent<SceneManagerCustom>().LoadScene1();
    }

    public void PlayButtonSound()
    {
        gameManager.GetComponent<SoundManager>().PlaySFX(2);
    }
}
