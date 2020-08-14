using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas_Grenade : MonoBehaviour
{
    private GameObject Player;

    public Sprite Gas;
    private bool Activated = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Activated == false)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) <= 5)
            {
                //show gas cloud
                Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Activated == true)
        {
            if (collision.CompareTag("Player"))
            {
                //start method to damage the player (15 damage every half second)
                InvokeRepeating("DamagePlayer", 0, 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Activated == true)
        {
            if (collision.CompareTag("Player"))
            {
                //cancel the method so the player stops taking damage
                CancelInvoke("DamagePlayer");
            }
        }
    }

    void Activate()
    {
        GetComponent<SpriteRenderer>().sprite = Gas;
        transform.localScale = Vector3.one;

        Activated = true;

        //change back to trigger if grenade is smoke's
        GetComponent<BoxCollider2D>().isTrigger = true;
        Destroy(GetComponent<Rigidbody2D>());
        StartCoroutine(DestroyGas(3));
    }

    IEnumerator DestroyGas(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }

    void DamagePlayer()
    {
        Player_Health.Health -= 15;
    }
}
