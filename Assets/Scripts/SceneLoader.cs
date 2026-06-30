using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("BowlingGame");
    }

    public void QuitGame()
    {
        Application.Quit();

        // This is only visible in the Unity Editor.
        Debug.Log("Quit Game");
    }
}