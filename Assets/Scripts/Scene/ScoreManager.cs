using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI scoreTMP;

    private float score;
    // Start is called before the first frame update
    void Start()
    {
        SetScore(0);
    }

    private float UpdateScoreTMP(){
        scoreTMP.text = "Score: " + score.ToString();

        return score;
    }
    public float SetScore(float targetScore){
        score = targetScore;
        UpdateScoreTMP();
        return targetScore;
    }

    public float AddScore(float scoreToSum){
        score += scoreToSum;
        UpdateScoreTMP();
        return score;
    }
    
}
