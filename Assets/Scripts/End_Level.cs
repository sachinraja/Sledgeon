using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class End_Level : MonoBehaviour
{
    public GameObject Player;
    public GameObject EndLevel_UI;
    public TextMeshProUGUI OperatorUnlockedTextUI;
    public GameObject OperatorImageUI;

    private bool LevelEnded = false;

    void Update()
    {
        //if boss dies end level
        if (GameObject.FindGameObjectsWithTag("Boss").Length == 0 && LevelEnded == false)
        {
            LevelEnded = true;
            EndLevel_UI.SetActive(true);
            OperatorUnlockedTextUI.SetText("Unlocked\n" + Operator_Select.operatorList[Global.currentLevel + 1].Name.ToUpper());
            OperatorImageUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Characters\\" + Operator_Select.operatorList[Global.currentLevel + 1].Name + "\\" + Operator_Select.operatorList[Global.currentLevel + 1].Name + "_CS");
            Player.GetComponent<Player_Control>().enabled = false;

            Debug.Log(Global.currentLevel);
            Global.Levels[Global.currentLevel] = true;
            Time.timeScale = 0;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
