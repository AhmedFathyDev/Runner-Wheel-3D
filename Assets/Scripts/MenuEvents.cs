using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuEvents : MonoBehaviour
{
    public Text InputFalid;
    public static string Username;

    public void PlayGame()
    {
        Username = InputFalid.text;
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
