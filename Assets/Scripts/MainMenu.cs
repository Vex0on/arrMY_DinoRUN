using JetBrains.Annotations;
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
        }
    }
}
