using System.Collections;
using UnityEngine;

public class ShopperAI : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;          // Speed when walking during the day
    [SerializeField] private float _runSpeed;           // Speed when running at night
    [SerializeField] private float _shoppingPause = 2f; // Length of time to pause to shop upon collision
    [SerializeField] private Color deadColor;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private float delayBeforeShrink = 5;
    [SerializeField] private float shrinkDuration = 1;

    public bool _isMoving;          // Indicates if player is moving
    public float randomX;           // Random X for random direction
    public float randomY;           // Random Y for random direction
    private Vector3 moveTowards;    // Direction to move towards
    private Transform player;       // Reference to the player's transform
    private bool isAlive = true;
    private int pointValueMin = 5;
    private int pointValueMax = 50;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isMoving = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Find the player by tag
        StartCoroutine(PauseToShop());
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (!_isMoving)
            {
                // Get new random destination point to move towards
                randomX = Random.Range(-100f, 100f);
                randomY = Random.Range(-100f, 100f);
                moveTowards = new Vector3(randomX, randomY, 0);
            }


            if (DayNightController.Instance.IsDay)
            {
                if (_isMoving)
                {
                    //walk in random direction
                    gameObject.transform.position = Vector2.MoveTowards(transform.position, moveTowards, _walkSpeed * Time.deltaTime);
                }
            }
            else
            {
                //run away from the player
                moveTowards = new Vector3(-player.position.x, -player.position.y, 0);
                transform.position = Vector2.MoveTowards(transform.position, moveTowards, _runSpeed * Time.deltaTime);

                // scream at random intervals
            }
        }
    }

    IEnumerator PauseToShop()
    {
        // shopping animation and/or speach bubble

        yield return new WaitForSeconds(_shoppingPause);

        // Start moving again
        _isMoving = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DayNightController.Instance.IsDay)
        {
            // Stop moving
            _isMoving = false;

            // Pause movement to shop
            StartCoroutine(PauseToShop());
        }
    }
    public void Die()
    {
        if (isAlive)
        {
            spriteRend.color = deadColor;
            isAlive = false;
            StartCoroutine(Despawn());
            MoneyParticles.Instance.Burst();
            Score.Instance.PointBurst(Random.Range(pointValueMin, pointValueMax));
        }
    }
    
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(delayBeforeShrink);
        LeanTween.scale(gameObject, Vector3.zero, shrinkDuration);
        yield return new WaitForSeconds(shrinkDuration);
        Destroy(gameObject);
    }
}
