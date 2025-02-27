using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button retryGameButton;

    private void Start()
    {
        // Ensure button is assigned and add a listener
        if (retryGameButton != null)
        {
            retryGameButton.onClick.AddListener(RetryGame);
        }
        else
        {
            Debug.LogError("Retry Game Button is not assigned in the Inspector!");
        }
    }

    public void RetryGame()
    {
        // Check if ScoreSystem instance exists before calling
        if (ScoreSystem.Instance != null)
        {
            ScoreSystem.Instance.EnablePlayingField();
        }
        else
        {
            Debug.LogError("ScoreSystem instance not found!");
        }
    }
}
