using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy_Control : MonoBehaviour
{
    public bool Static = false;
    public float seeingDistance = 10;

    public int EnemySpeed;
    public int XMoveDirection;

    public SpriteRenderer spriteRenderer;
    public Sprite rightSprite;
    public Sprite leftSprite;

    public GameObject bulletToRight, bulletToLeft;
    Vector2 bulletPos;
    public float fireRate = 1f;
    float nextFire = 0.0f;
    public GameObject Gun;
    public int gunType = 0;
    private int Damage = 0;

    private bool isMoving = true;

    public int Health = 100;

    float extraHeight = .05f;

    public GameObject Player;

    private void Awake()
    {
        Damage = Operator_Select.gunList[gunType].Damage;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Math.Abs(transform.position.x - Player.transform.position.x) <= 30)
        {
            isMoving = true;
             
            if (RaycastEdgeCheck())
            {
                RaycastTop();
                RaycastMiddle();
                RaycastBottom();
            }
        }

        else
        {
            isMoving = false;
        }
    }

    private void LateUpdate()
    {
        if (Static == false)
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

        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void Flip()
    {
        if (Static == false)
        {
            XMoveDirection *= -1;

            if (XMoveDirection == 1)
            {
                GetComponent<SpriteRenderer>().sprite = rightSprite;

                Gun.transform.Rotate(0f, 180f, 0f);
                Gun.transform.position = new Vector3(transform.position.x + 0.29f, transform.position.y + 0.05f, -1);
            }

            else if (XMoveDirection == -1)
            {
                GetComponent<SpriteRenderer>().sprite = leftSprite;

                Gun.transform.Rotate(0f, 180f, 0f);
                Gun.transform.position = new Vector3(transform.position.x - 0.29f, transform.position.y + 0.05f, -1);
            }
        }
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

    void RaycastTop()
    {
        //fires ray to check for collision
        RaycastHit2D raycastTop = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + GetComponent<Collider2D>().bounds.size.y / 2, transform.position.z),
            new Vector2(XMoveDirection, 0));

        if (raycastTop)
        {
            if (raycastTop.distance <= 0.5f && raycastTop.collider.gameObject.CompareTag("Enemy") == false && raycastTop.collider.gameObject.CompareTag("Player") == false)
            {
                isMoving = true;

                Flip();
            }
        }
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

    void RaycastBottom()
    {
        RaycastHit2D raycastBottom = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - GetComponent<Collider2D>().bounds.size.y / 2 + extraHeight, transform.position.z),
            new Vector2(XMoveDirection, 0));

        if (raycastBottom)
        {
            if (raycastBottom.transform.gameObject.CompareTag("Enemy") && raycastBottom.distance <= 0.5f)
            {
                isMoving = false;
            }

            else if (raycastBottom.transform.gameObject.CompareTag("Player") && raycastBottom.distance <= seeingDistance)
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

                if (raycastBottom.distance <= 0.5f)
                {
                    Flip();
                }
            }
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
