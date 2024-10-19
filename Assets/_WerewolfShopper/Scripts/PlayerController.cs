using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDuration = .5f;
    [SerializeField] private float attackMoveSpeedBonus = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private SpriteRenderer spriteRend;
    private Vector2 movement;
    private bool isAttacking;
    private float angle;

    void Update()
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
        }
        if (Input.GetMouseButtonDown(0) && (movement.x != 0 || movement.y != 0))
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
        isAttacking = false;
    }


    void FixedUpdate()
    {
        // Apply movement to Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
