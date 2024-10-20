using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDuration = .5f;
    [SerializeField] private float attackBuffer = .25f; //how much time after attack is player collider still a hurtbox
    [SerializeField] private float attackMoveSpeedBonus = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private SpriteRenderer wolfSpriteRend;
    [SerializeField] private SpriteRenderer humanSpriteRend;
    private Vector2 movement;
    private bool isAttacking;
    private float angle;
    public bool isHuman;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        BecomeHuman();
    }

    void LateUpdate()
    {
        if (!isAttacking)
        {
            // Get input from WASD or arrow keys
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            print("lookdir: " + lookDir.x + " " + lookDir.y);
            if (lookDir.x < 0)
            {
                humanSpriteRend.flipY = true;
            }
            else
            {
                humanSpriteRend.flipX = false;
                humanSpriteRend.flipY = false;
            }
        }
        if (!isHuman && Input.GetMouseButtonDown(0) && (movement.x != 0 || movement.y != 0))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        spriteRend.color = Color.red;
        moveSpeed += attackMoveSpeedBonus;
        yield return new WaitForSeconds(attackDuration);
        moveSpeed -= attackMoveSpeedBonus;
        spriteRend.color = Color.white;
        yield return new WaitForSeconds(attackBuffer);
        isAttacking = false;
    }

    void FixedUpdate()
    {
        // Apply movement to Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking)
        {
            collision.collider.GetComponent<PoliceAI>()?.Die();
        }
    }

    public void BecomeHuman()
    {
        isHuman = true;
        wolfSpriteRend.gameObject.SetActive(false);
        humanSpriteRend.gameObject.SetActive(true);
    }

    public void BecomeWolf()
    {
        isHuman = false;
        wolfSpriteRend.gameObject.SetActive(true);
        humanSpriteRend.gameObject.SetActive(false);
    }
}
