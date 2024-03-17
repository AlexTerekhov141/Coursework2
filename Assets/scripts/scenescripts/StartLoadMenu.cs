using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static float gameTime = 0f; // Статическая переменная для хранения времени

    public void PlayGame(int numbScene)
    {
        SceneManager.LoadScene(numbScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
