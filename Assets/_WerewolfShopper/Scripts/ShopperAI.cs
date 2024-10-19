using UnityEngine;

public class ShopperAI : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    public bool _isMoving;
    public float randomX;
    public float randomY;
    private Vector3 moveTowards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isMoving)
        {
            randomX = Random.Range(-100f, 100f);
            randomY = Random.Range(-100f, 100f);

            moveTowards = new Vector3(randomX, randomY, 0);

            _isMoving = true;
        }

        if (DayNightController.Instance.IsDay)
        {
            //walk, shop
            gameObject.transform.position = Vector2.MoveTowards(transform.position, moveTowards, _walkSpeed * Time.deltaTime);

        }
        else
        {
            //run, scream
            gameObject.transform.position = Vector2.MoveTowards(transform.position, moveTowards, _runSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        _isMoving = false;
    }
}
