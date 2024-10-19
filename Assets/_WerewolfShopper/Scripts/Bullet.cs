using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 7;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.GetComponent<PlayerHealth>()?.TakeDamage(7);
        Destroy(gameObject);
    }
}
