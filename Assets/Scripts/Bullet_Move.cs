using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_Move : MonoBehaviour
{
    public float velX = 12f;
    float velY = 0f;
    Rigidbody2D rb;

    public int EnemyDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velX, velY);

        //destroy bullet after 3 seconds
        StartCoroutine(DestroyBullet(2));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kill enemy
        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("PlayerBullet"))
        {
            collision.gameObject.GetComponent<Enemy_Control>().Health -= Player_Control.Damage;

            if (collision.gameObject.GetComponent<Enemy_Control>().Health <= 0)
            {
                Destroy(collision.gameObject);
            }
        }

        else if (collision.gameObject.CompareTag("Boss") && gameObject.CompareTag("PlayerBullet"))
        {
            collision.gameObject.GetComponent<Boss_Control>().Health -= Player_Control.Damage;

            if (collision.gameObject.GetComponent<Boss_Control>().Health <= 0)
            {
                Destroy(collision.gameObject);
            }
        }

        else if (collision.gameObject.CompareTag("TwitchDrone") && gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("TwitchBossDrone") && gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("EnemyBullet"))
        {
            Player_Health.Health -= EnemyDamage;
        }

        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("BossBullet"))
        {
            Player_Health.Health -= EnemyDamage;
        }

        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("TwitchDroneBullet"))
        {
            Player_Health.Health -= EnemyDamage;
        }

        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("TwitchBossDroneBullet"))
        {
            Player_Health.Health -= EnemyDamage;
        }

        Destroy(gameObject);
    }

    IEnumerator DestroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        
        Destroy(gameObject);
    }
}
