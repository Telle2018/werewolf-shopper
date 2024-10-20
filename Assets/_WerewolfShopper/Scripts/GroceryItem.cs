using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GroceryItem : MonoBehaviour
{
    public GameObject gameList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && PlayerController.Instance.isHuman)
        {
            GroceryListManager.Instance.UpdateItemQuantity(gameObject, 1);
            Destroy(gameObject);
        }
    }

}
