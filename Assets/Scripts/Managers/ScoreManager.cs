using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instanceSm;
    ScoreManager()
    {
        instanceSm = this;
    }
    
    public ulong score = 0;
    
    [SerializeField] private GameObject scoreObj;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Animator _animator;
    private bool _scoreIsActive = true;

    private void Awake()
    {
        UpdateText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _scoreIsActive = !_scoreIsActive;
            _animator.SetBool("isOn",_scoreIsActive);
        }
    }

    public void AddScore(int s)
    {
        score += (ulong) s;
        UpdateText();
    }
    
    public void RemoveScore(int s)
    {
        score -= (ulong) s;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = "score: " + score;
    }
}