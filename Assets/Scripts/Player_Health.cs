using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    private int MaxHealth = 100;
    public static int Health;

    private Text healthText;
    public GameObject healthTextUI;

    private void Awake()
    {
        Health = MaxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthText = healthTextUI.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Death();
        }

        if (gameObject.transform.position.y <= -10)
        {
            Death();
        }

        healthText.text = Health.ToString();
    }

    public void Death()
    {
        SceneManager.LoadScene("Game");
    }
}
