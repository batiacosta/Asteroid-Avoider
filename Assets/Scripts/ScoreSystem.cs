using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreMultiplier;

    private float _score = 0f;

    public bool ShouldCountScore = true;
    
    private void Update()
    {
        if (ShouldCountScore)
        {
            _score += Time.deltaTime * scoreMultiplier;
            scoreText.text = Mathf.FloorToInt(_score).ToString();
        }
    }

    public int GetScore()
    {
        ShouldCountScore = false;
        return Mathf.FloorToInt(_score);
    }

    public void RestartScore()
    {
        ShouldCountScore = true;
        _score = 0f;
        
    }

    public void StartTimer()
    {
        ShouldCountScore = true;
    }
}
