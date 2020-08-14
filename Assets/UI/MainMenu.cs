using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI SaveTextUI;

    private void Start()
    {
        if (Global.Loaded == false)
        {
            //load data on main menu load
            Data_Management.datamanagement.LoadData();

            if (Data_Management.datamanagement.Levels.Length == 4)
            {
                Global.Levels = Data_Management.datamanagement.Levels;
            }

            Global.Loaded = true;
        }
    }

    public void PlayGame(int level)
    {
        Global.currentLevel = level;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        //save levels completed
        Data_Management.datamanagement.Levels = Global.Levels;
        Data_Management.datamanagement.SaveData();

        SaveTextUI.SetText("SAVED!");
        StartCoroutine(ResetSaveText(1.5f));
    }

    IEnumerator ResetSaveText(float time)
    {
        yield return new WaitForSeconds(time);

        SaveTextUI.SetText("SAVE");
    }
}
