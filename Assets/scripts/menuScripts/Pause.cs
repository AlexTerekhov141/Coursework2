using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool OptionsIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject optionsUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause_menu();
            }
        }
    }
    public void Resume()
    {
        if (OptionsIsPaused == false)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        else
        {
            pauseMenuUI.SetActive(true);
            optionsUI.SetActive(false);
            OptionsIsPaused = false;
        }
    }
    void Pause_menu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Options_menu()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
        GameIsPaused = true;
        OptionsIsPaused = true;
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }
    
}
