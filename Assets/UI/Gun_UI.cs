using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_UI : MonoBehaviour
{
    public GameObject gunNameUI;
    public GameObject bulletAmountUI;

    void Start()
    {
        gunNameUI.GetComponent<Text>().text = Operator_Select.gunList[Global.Operator.Gun].Name;
        bulletAmountUI.GetComponent<Text>().text = (Player_Control.BurstNumber - GameObject.FindGameObjectsWithTag("PlayerBullet").Length).ToString();
    }

    void Update()
    {
        bulletAmountUI.GetComponent<Text>().text = (Player_Control.BurstNumber - GameObject.FindGameObjectsWithTag("PlayerBullet").Length).ToString();
    }
}
