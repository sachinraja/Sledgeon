using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Health_Bar : MonoBehaviour
{
    private UnityEngine.UI.Slider slider;

    private GameObject Player;
    private GameObject Boss;
    private int BossHealth = 1000;

    public GameObject Fill;
    public GameObject Border;

    private bool PlayerDetect = false;
    private float BossPointX;

    void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();

        Player = GameObject.FindGameObjectWithTag("Player");
        Boss = GameObject.FindGameObjectWithTag("Boss");
        BossHealth = Boss.GetComponent<Boss_Control>().Health;

        slider.maxValue = BossHealth;
        slider.value = BossHealth;

        Fill.GetComponent<UnityEngine.UI.Image>().enabled = false;
        Border.GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    void Update()
    {
        if (Boss)
        {
            BossHealth = Boss.GetComponent<Boss_Control>().Health;
            slider.value = BossHealth;

            //detects when the player enters the boss's area and sets a point to that place
            if (PlayerDetect == false)
            {
                if (Boss.transform.position.x - 15 <= Player.transform.position.x)
                {
                    BossPointX = Player.transform.position.x;
                    Fill.GetComponent<UnityEngine.UI.Image>().enabled = true;
                    Border.GetComponent<UnityEngine.UI.Image>().enabled = true;

                    PlayerDetect = true;
                }
            }

            //if player goes back from that point and resets the process
            if (Player.transform.position.x < BossPointX)
            {
                PlayerDetect = false;

                Fill.GetComponent<UnityEngine.UI.Image>().enabled = false;
                Border.GetComponent<UnityEngine.UI.Image>().enabled = false;
            }
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
