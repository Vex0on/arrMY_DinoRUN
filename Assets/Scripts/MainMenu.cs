using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }
    
    [Header("UI Buttons")]
    public Button startButton;
    public Button tryAgainButton;
    public Image endScreenImage;
    public TMP_Text livescoreTxt;
    public TMP_Text endscoreTxt;
    public TMP_Text gameOverTxt;

    private void Awake()
    {
        if (Instance !=  null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        if (tryAgainButton != null)
        {
            tryAgainButton.gameObject.SetActive(false);
            endScreenImage.gameObject.SetActive(false);
            livescoreTxt.gameObject.SetActive(true);
            endscoreTxt.gameObject.SetActive(false);
            gameOverTxt.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ShowTryAgain()
    {
        if (tryAgainButton != null)
        {
            tryAgainButton.gameObject.SetActive(true);
            endScreenImage.gameObject.SetActive(true);
            livescoreTxt.gameObject.SetActive(false);
            endscoreTxt.gameObject.SetActive(true);
            gameOverTxt.gameObject.SetActive(true);
        }
    }

    public void ShowFinalScore(float score)
    {
        endscoreTxt.text = $"GRATULACJE! TWÓJ WYNIK: {score:F0} PKT";
    }
}
