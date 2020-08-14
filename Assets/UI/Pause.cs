using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject Player;
    public GameObject PauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.activeInHierarchy == false)
        {
            PauseMenu.SetActive(true);

            Time.timeScale = 0;
            Player.GetComponent<Player_Control>().enabled = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.activeInHierarchy == true)
        {
            Resume();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);

        Time.timeScale = 1;
        Player.GetComponent<Player_Control>().enabled = true;
    }
}
