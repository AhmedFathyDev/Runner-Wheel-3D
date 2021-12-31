using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEvents : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
