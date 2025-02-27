using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public enum ScoreType { Player, Bot }

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }  // Singleton instance

    [SerializeField] private int maxScore = 2;
    [SerializeField] private int maxReceivedScore = 1;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI botScoreText;

    private int scorePlayer = 0;
    private int scoreBot = 0;

    private void Awake()
    {
        // Singleton setup to prevent duplicates
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();  // Initialize UI on start
    }

    // Add score to either player or bot
    public void AddScore(ScoreType scoreType, int scoreToAdd)
    {
        // Validate the score to ensure it's within limits
        if (scoreToAdd <= 0 || scoreToAdd > maxReceivedScore)
        {
            Debug.LogWarning($"Invalid score: {scoreToAdd}");
            return;
        }

        // Add score based on the scoreType (Player or Bot)
        if (scoreType == ScoreType.Player)
        {
            scorePlayer += scoreToAdd;
        }
        else if (scoreType == ScoreType.Bot)
        {
            scoreBot += scoreToAdd;
        }

        Debug.Log($"Player score: {scorePlayer}, Bot score: {scoreBot}");

        // Check for a winner
        if (scorePlayer >= maxScore)
        {
            HandleWin(ScoreType.Player);
        }
        else if (scoreBot >= maxScore)
        {
            HandleWin(ScoreType.Bot);
        }

        // Update UI
        UpdateScoreUI();
    }

    private void HandleWin(ScoreType winner)
    {
        Debug.Log($"{winner} wins!");
        ResetScores();
    }

    private void ResetScores()
    {
        scorePlayer = 0;
        scoreBot = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (playerScoreText) playerScoreText.text = scorePlayer.ToString();
        if (botScoreText) botScoreText.text = scoreBot.ToString();
    }
}
