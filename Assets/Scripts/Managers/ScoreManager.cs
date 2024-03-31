using System;
using Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instanceSm;
    ScoreManager()
    {
        instanceSm = this;
    }
    
    [Header("Score")]
    public ulong score = 0;
    

    [Header("Bosses")]
    public Sprite[] bossesSprites;
    [SerializeField] private Image bossImage;
    [SerializeField] private TMP_Text bossName;
    [SerializeField] private GameObject bossLayout;
    
    [Header("Level and Score data")]
    [SerializeField] private GameObject scoreObj;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Animator _animator;

    private bool _scoreIsActive = true;
    

    private void Start()
    {
        UpdateScore();
        UpdateLevelInfo();
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
        UpdateScore();
    }
    
    public void RemoveScore(int s)
    {
        score -= (ulong) s;
        UpdateScore();
    }
    public void OnSceneCheck()
    {
        if (EnemiesSpawner.instanceES.comingBoss != null)
        {
            bossLayout.SetActive(true);
            bossImage.sprite = bossesSprites[EnemiesSpawner.instanceES.currentBossId];
            bossName.text = EnemiesSpawner.instanceES.comingBoss.name;
        }
    }

    public void SetOnAnim()
    {
        _animator.SetBool("isOn",_scoreIsActive);
    }

    private void UpdateScore()
    {
        scoreText.text = "score: " + score;
    }

    private void UpdateLevelInfo()
    {
        levelText.text = "Level: " + GameManager.instanceGm.currentLevelNumber;
    }
}