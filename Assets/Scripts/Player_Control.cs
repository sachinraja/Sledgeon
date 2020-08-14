using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player_Control : MonoBehaviour
{
    public int playerSpeed = 10;
    private bool facingRight = false;
    public int playerJumpPower = 1000;
    private float moveX;

    public SpriteRenderer spriteRenderer;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private BoxCollider2D boxCollider2d;

    public GameObject bulletToRight, bulletToLeft;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;

    public GameObject Gun;
    public static int Damage = 0;
    public static int BurstNumber = 0;

    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();

        Damage = Operator_Select.gunList[Global.Operator.Gun].Damage;
        BurstNumber = Operator_Select.gunList[Global.Operator.Gun].Burst;
        fireRate = Operator_Select.gunList[Global.Operator.Gun].Firerate;
        
        SpriteRenderer gunSprite = Gun.GetComponent<SpriteRenderer>();

        gunSprite.sprite = Resources.Load<Sprite>("Guns\\" + Operator_Select.gunList[Global.Operator.Gun].Name);

        leftSprite = Resources.Load<Sprite>("Characters\\" + Global.Operator.Name.ToString() + "\\" + Global.Operator.Name.ToString() + "_L");
        rightSprite = Resources.Load<Sprite>("Characters\\" + Global.Operator.Name.ToString() + "\\" + Global.Operator.Name.ToString() + "_R");
        spriteRenderer.sprite = rightSprite;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && GameObject.FindGameObjectsWithTag("PlayerBullet").Length < BurstNumber)
        {
            //wait a little before allowing next bullet to be fired
            nextFire = Time.time + fireRate;
            Fire();
        }
    }

    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }

        //animations
        //player direction
        if (moveX < 0.0f & facingRight == false)
        {
            //rotate player
            spriteRenderer.sprite = leftSprite;
            facingRight = !facingRight;

            //rotate gun
            Gun.transform.Rotate(0f, 180f, 0f);
            Gun.transform.position = new Vector3(transform.position.x - 0.29f, transform.position.y + 0.05f, -1);
        }

        if (moveX > 0.0f && facingRight == true)
        {
            spriteRenderer.sprite = rightSprite;
            facingRight = !facingRight;

            Gun.transform.Rotate(0f, 180f, 0f);
            Gun.transform.position = new Vector3(transform.position.x + 0.29f, transform.position.y + 0.05f, -1);
        }
        
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }

    private bool isGrounded()
    {
        //checks if player hits ground with a raycast down from the middle
        //of the player
        float extraHeight = .05f;

        RaycastHit2D raycastBottomLeft = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x - boxCollider2d.bounds.size.x / 2, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.down, boxCollider2d.bounds.extents.y + extraHeight);
        RaycastHit2D raycastBottomMiddle = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeight);
        RaycastHit2D raycastBottomRight = Physics2D.Raycast(new Vector3(boxCollider2d.bounds.center.x + boxCollider2d.bounds.size.x / 2, boxCollider2d.bounds.center.y, boxCollider2d.bounds.center.z), Vector2.down, boxCollider2d.bounds.extents.y + extraHeight);

        if (raycastBottomLeft.collider != null || raycastBottomMiddle.collider != null || raycastBottomRight.collider != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void Fire()
    {
        //sets position of bullet to player
        bulletPos = transform.position;

        //changes bullet depending on what direction the character is facing
        if (facingRight == false)
        {
            bulletPos += new Vector2(+1f, -0.043f);
            GameObject bullet = Instantiate(bulletToRight, bulletPos, Quaternion.identity);
            bullet.tag = gameObject.tag + "Bullet";
        }

        else if (facingRight == true)
        {
            bulletPos += new Vector2(-1f, -0.043f);
            GameObject bullet = Instantiate(bulletToLeft, bulletPos, Quaternion.identity);
            bullet.tag = gameObject.tag + "Bullet";
        }
    }
}
