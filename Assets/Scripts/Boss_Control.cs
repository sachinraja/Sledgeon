using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boss_Control : MonoBehaviour
{
    public bool Montagne = false;
    public bool Twitch = false;
    public bool Smoke = false;

    private bool newDrone;
    public GameObject Drone;
    public GameObject RoomParent;

    private bool newGrenade;
    public GameObject GasGrenade;

    public bool Static = false;
    public float seeingDistance = 10;

    public int EnemySpeed;
    public int XMoveDirection;

    public Sprite rightSprite;
    public Sprite leftSprite;

    public GameObject bulletToRight, bulletToLeft;
    Vector2 bulletPos;
    public float fireRate = 0.05f;
    float nextFire = 0.0f;
    public GameObject Gun;
    public int gunType = 0;
    private int Damage = 0;

    private bool isMoving = true;

    public int Health = 1000;

    private float extraHeight = .05f;

    private float FlipDistance;

    private void Awake()
    {
        Damage = Operator_Select.gunList[gunType].Damage;

        if (Montagne == true)
        {
            FlipDistance = 1;
        }

        else
        {
            FlipDistance = 0.5f;
        }
    }

    void Update()
    {
        if (RaycastEdgeCheck())
        {
            RaycastTop();
            RaycastMiddle();
            RaycastBottom();
        }

        if (Twitch == true)
        {
            DroneCheck();
        }

        if (Smoke == true)
        {
            GrenadeCheck();
        }
    }

    private void LateUpdate()
    {
        if (Static == false)
        {
            if (isMoving == true)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
            }

            else if (isMoving == false)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    void Flip()
    {
        if (Static == false)
        {
            XMoveDirection *= -1;

            if (Montagne == false)
            {
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
    }

    void Fire()
    {
        //sets position of bullet to player
        bulletPos = transform.position;

        //changes bullet depending on what direction the character is facing
        if (XMoveDirection == 1 || Montagne == true)
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
            if (raycastTop.distance <= FlipDistance && raycastTop.collider.gameObject.CompareTag("Enemy") == false && raycastTop.collider.gameObject.CompareTag("Player") == false)
            {
                isMoving = true;

                Flip();
            }
        }
    }

    void RaycastMiddle()
    {
        int MoveDirection = XMoveDirection;

        if (Montagne == true)
        {
            MoveDirection = 1;
        }

        RaycastHit2D raycastMiddle = Physics2D.Raycast(transform.position,
            new Vector2(MoveDirection, 0));

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

                if (raycastMiddle.distance <= FlipDistance)
                {
                    Flip();
                }
            }
        }
    }

    void RaycastBottom()
    {
        int MoveDirection = XMoveDirection;

        if (Montagne == true)
        {
            MoveDirection = 1;
        }

        RaycastHit2D raycastBottom = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - GetComponent<Collider2D>().bounds.size.y / 2 + extraHeight, transform.position.z),
            new Vector2(MoveDirection, 0));

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

                if (raycastBottom.distance <= FlipDistance)
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

    void DroneCheck()
    {
        if (GameObject.FindGameObjectsWithTag("TwitchBossDrone").Length == 0 && newDrone == false)
        {
            StartCoroutine(InstantiateDrone(2));
        }
    }

    IEnumerator InstantiateDrone(float time)
    {
        newDrone = true;

        yield return new WaitForSeconds(time);

        GameObject d = Instantiate(Drone, new Vector3(transform.position.x + 12.51f, 3.2f, 0), Quaternion.identity, RoomParent.transform);
        d.tag = "TwitchBossDrone";

        newDrone = false;
    }

    void GrenadeCheck()
    {
        if (GameObject.FindGameObjectsWithTag("SmokeGrenade").Length == 0 && newGrenade == false)
        {
            StartCoroutine(InstantiateGrenade(3));
        }
    }

    IEnumerator InstantiateGrenade(float time)
    {
        newGrenade = true;

        yield return new WaitForSeconds(time);

        GameObject g = Instantiate(GasGrenade, new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, 0), Quaternion.identity, RoomParent.transform);

        newGrenade = false;                                 
    }
}
