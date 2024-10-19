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

    public void Kill()
    {
        spriteRend.color = Color.black;
        isAlive = false;
        StartCoroutine(Despawn());
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
        // Attack logic: could be damage to player, melee attack, or shooting
        Debug.Log("Enemy attacks the player!");

        // If your player has a health system, you can call the damage function here:
        // player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
