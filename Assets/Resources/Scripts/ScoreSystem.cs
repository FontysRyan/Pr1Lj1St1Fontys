using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[System.Serializable]
public enum ScoreType { Player, Bot }

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }  // Singleton instance

    [Header("Score Settings")]
    [SerializeField] private int maxScore = 2;
    [SerializeField] private int maxReceivedScore = 1;
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI botScoreText;
    [SerializeField] private TextMeshProUGUI stylingText;
    [SerializeField] private TextMeshProUGUI botNameText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [Header("Win/Lose UI")]
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private Button RetryButton;


    private int scorePlayer = 0;
    private int scoreBot = 0;

    private GameObject ball;
    private GameObject[] paddles;

    private void Awake()
    {
        ball = GameObject.FindWithTag("Ball");
        paddles = GameObject.FindGameObjectsWithTag("Paddle");
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
        ball.GetComponent<BallController>().ResetBallPosition();
        Debug.Log($"{winner} wins!");
        DisablePlayingField();
        if (winner == ScoreType.Bot)
        {
            loseText.gameObject.SetActive(true);
        }
        else
        {
            winText.gameObject.SetActive(true);
        }
        RetryButton.gameObject.SetActive(true);
    }
    private void UpdateScoreUI()
    {
        if (playerScoreText) playerScoreText.text = scorePlayer.ToString();
        if (botScoreText) botScoreText.text = scoreBot.ToString();
    }

    private void DisablePlayingField()
    {
        playerScoreText.gameObject.SetActive(false);
        botScoreText.gameObject.SetActive(false);
        stylingText.gameObject.SetActive(false);
        botNameText.gameObject.SetActive(false);
        playerNameText.gameObject.SetActive(false);



        if (ball != null) ball.SetActive(false);
        foreach (GameObject paddle in paddles)
        {
            if (paddle != null) paddle.SetActive(false);
        }
    }





}
