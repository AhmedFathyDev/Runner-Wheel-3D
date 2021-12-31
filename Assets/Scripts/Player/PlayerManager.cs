using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool GameOver;
    public static bool IsGameStarted;
    public static int NumberOfCoins;

    public GameObject GameOverPanel;
    public GameObject StartingText;
    public Text CoinsText;

    // Start is called before the first frame update
    private void Start()
    {
        GameOver = false;
        Time.timeScale = 1;
        IsGameStarted = false;
        NumberOfCoins = 0;
        CoinsText.text = "Coins: 0";
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameOver)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }

        CoinsText.text = "Coins: " + NumberOfCoins;

        if (SwipeManager.Tap)
        {
            IsGameStarted = true;
            Destroy(StartingText);
        }
    }
}
