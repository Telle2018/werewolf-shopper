using System.Collections;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    [SerializeField] private float speed = 3f;                // Enemy movement speed
    [SerializeField] private float chaseRange = 10f;          // Range within which the enemy starts chasing the player
    [SerializeField] private float attackRange = 1.5f;        // Range within which the enemy can attack the player
    [SerializeField] private float attackCooldown = 2f;       // Cooldown between attacks
    [SerializeField] private int damage = 10;

    [SerializeField] private float delayBeforeShrink = 5;
    [SerializeField] private float shrinkDuration = 1;
    [SerializeField] private SpriteRenderer spriteRend;

    // New fields
    [SerializeField] private GameObject bulletPrefab;          // Reference to the bullet prefab
    [SerializeField] private Transform firePoint;              // The point from where the bullet will be fired
    [SerializeField] private float bulletSpeed = 10f;          // Speed of the bullet

    private Transform player;               // Reference to the player's transform
    private float lastAttackTime = 0;       // Time of last attack
    private bool isAttacking = false;       // Is the enemy currently attacking?
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Find the player by tag
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || !isAlive) return; // Exit if no player found

        float distanceToPlayer = Vector2.Distance(transform.position, player.position); // Calculate distance to player

        // Rotate towards the player
        RotateTowardsPlayer();

        // Chase the player if within chase range
        if (distanceToPlayer <= chaseRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange) // Attack if within attack range
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time; // Reset cooldown
            }
        }
    }

    // Method to rotate towards the player
    private void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Die()
    {
        spriteRend.color = Color.black;
        isAlive = false;
        StartCoroutine(Despawn());
        MoneyParticles.Instance.Burst();
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(delayBeforeShrink);
        LeanTween.scale(gameObject, Vector3.zero, shrinkDuration);
        yield return new WaitForSeconds(shrinkDuration);
        Destroy(gameObject);
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Calculate the direction towards the player
            Vector2 direction = (player.position - firePoint.position).normalized;

            // Instantiate the bullet at the fire point
            Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation)
                .GetComponent<Rigidbody2D>();

            if (bulletRigidBody != null)
            {
                // Apply velocity in the direction of the player
                bulletRigidBody.linearVelocity = direction * bulletSpeed;
            }
        }
        else
        {
            Debug.LogError("Bullet Prefab or FirePoint not assigned!");
        }
    }
}
