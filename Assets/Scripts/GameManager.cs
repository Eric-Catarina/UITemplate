using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject hitEffect;
    public GameObject explosionEffect;

    private void Awake()
    {
        Time.timeScale = 1f;
    }


    public void SlowTime()
    {
        StartCoroutine(SlowTimeCoroutine());
    }

    public void SpeedUpTime()
    {
        StartCoroutine(SpeedUpTimeCoroutine());
    }

    // Gradually speeds time scale up from 0.2 to 1 over 3 seconds
    private IEnumerator SpeedUpTimeCoroutine()
    {
        float timeScale = 0.2f;
        while (timeScale < 1f)
        {
            timeScale += 0.1f;
            Time.timeScale = timeScale;
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Gradually slows time scale down from 1 to 0.2 over 1.5 seconds 
    private IEnumerator SlowTimeCoroutine()
    {
        float timeScale = 1f;
        while (timeScale > 0.2f)
        {
            timeScale -= 0.1f;
            Time.timeScale = timeScale;
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void SpawnHitEffect(Vector3 position)
    {
        GameObject hitEffectInstance = Instantiate(hitEffect, position, Quaternion.identity);
        Destroy(hitEffectInstance, 0.5f);
    }

    public void SpawnExplosionEffect(Vector3 position)
    {
        GameObject explosionEffectInstance = Instantiate(explosionEffect, position, Quaternion.identity);
        Destroy(explosionEffectInstance, 1f);
    }





}
