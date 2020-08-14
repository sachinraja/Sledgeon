using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitch_Drone_Control : MonoBehaviour
{
    private GameObject Player;

    public float seeingDistance = 10;

    private bool isMoving = true;
    public int EnemySpeed = 5;
    public int XMoveDirection;

    public GameObject bulletToRight, bulletToLeft;
    Vector2 bulletPos;
    public float fireRate = 1f;
    float nextFire = 0.0f;
    private int Damage = 10;

    private bool BeingDestroyed = false;
    public Sprite Broken;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (BeingDestroyed == false)
        {
            if (transform.position.x - 30 <= Player.transform.position.x && Player.transform.position.x <= transform.position.x + 30)
            {
                isMoving = true;

                if (RaycastEdgeCheck())
                {
                    RayTop();
                    RaycastMiddle();
                }
            }

            else
            {
                isMoving = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (BeingDestroyed == false)
        {
            if (isMoving == true)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection * EnemySpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }

            else if (isMoving == false)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    void RayTop()
    {
        //checks if player jumps on top
        RaycastHit2D raycastTop = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + GetComponent<Collider2D>().bounds.size.y / 2),
            new Vector2(0, 1));

        if (raycastTop)
        {
            if (raycastTop.transform.gameObject.CompareTag("Player") && raycastTop.distance <= 0.5f)
            {
                StartCoroutine(DestroyOnJump(2));
            }
        }
    }

    IEnumerator DestroyOnJump(float time)
    {
        BeingDestroyed = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().sprite = Broken;

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    void RaycastMiddle()
    {
        RaycastHit2D raycastMiddle = Physics2D.Raycast(transform.position,
            new Vector2(XMoveDirection, 0));

        if (raycastMiddle)
        {
            if (raycastMiddle.transform.gameObject.CompareTag("Enemy") && raycastMiddle.distance <= 0.5f)
            {
                isMoving = false;
            }

            else if (raycastMiddle.transform.gameObject.CompareTag("Player") && raycastMiddle.distance <= seeingDistance)
            {
                isMoving = false;

                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Fire();
                }
            }

            else
            {
                isMoving = true;

                if (raycastMiddle.distance <= 0.5f)
                {
                    Flip();
                }
            }
        }
    }

    void Flip()
    {
        XMoveDirection *= -1;

        transform.Rotate(new Vector3(0, 180, 0));
    }

    void Fire()
    {
        //sets position of bullet to player
        bulletPos = transform.position;

        //changes bullet depending on what direction the character is facing
        if (XMoveDirection == 1)
        {
            bulletPos += new Vector2(+1f, -0.043f);
            GameObject bullet = Instantiate(bulletToRight, bulletPos, Quaternion.identity);
            bullet.tag = gameObject.tag + "Bullet";
            bullet.GetComponent<Bullet_Move>().EnemyDamage = Damage;
        }

        else if (XMoveDirection == -1)
        {
            bulletPos += new Vector2(-1f, -0.043f);
            GameObject bullet = Instantiate(bulletToLeft, bulletPos, Quaternion.identity);
            bullet.tag = gameObject.tag + "Bullet";
            bullet.GetComponent<Bullet_Move>().EnemyDamage = Damage;
        }
    }

    private bool RaycastEdgeCheck()
    {
        //stops enemy from walking off edge
        BoxCollider2D boxcollider2d = GetComponent<BoxCollider2D>();

        RaycastHit2D raycastEdge = Physics2D.Raycast(new Vector3(boxcollider2d.bounds.center.x - boxcollider2d.bounds.size.x / 2, boxcollider2d.bounds.center.y, boxcollider2d.bounds.center.z), Vector2.down, boxcollider2d.bounds.extents.y + 0.5f);

        if (XMoveDirection == 1)
        {
            raycastEdge = Physics2D.Raycast(new Vector3(boxcollider2d.bounds.center.x + boxcollider2d.bounds.size.x / 2, boxcollider2d.bounds.center.y, boxcollider2d.bounds.center.z), Vector2.down, boxcollider2d.bounds.extents.y + 0.5f);
        }

        if (!raycastEdge)
        {
            isMoving = true;
            Flip();

            return false;
        }

        return true;
    }
}
