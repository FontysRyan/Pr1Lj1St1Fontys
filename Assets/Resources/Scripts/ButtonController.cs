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

    private void resetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void RetryGame()
    {
        resetGame();
    }
}
