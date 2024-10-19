using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GroceryListManager : MonoBehaviour
{
    public GameObject groceryItem;
    public GameObject parentObject;
    
    private List<GameObject> groceryList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //groceryList = new List<GameObject> {};
        //GameObject grocery = AddGroceryItem(Color.cyan, "Cyan Items", "Cyan", 5);
        //groceryList.Add(grocery);
        //RemoveItemQuantity(grocery.name, 5);
    }

    GameObject AddGroceryItem(Color color, string text, string nam, int quantity)
    {
        GameObject newGroceryItem = Instantiate(groceryItem, parentObject.transform);

        Transform childTextTransform = newGroceryItem.transform.Find("ObjectText");

        TMP_Text groceryText = childTextTransform.GetComponent<TMP_Text>();

        groceryText.text = text;

        Transform childSpriteTransform = newGroceryItem.transform.Find("Image");

        Image grocerySprite = childSpriteTransform.GetComponent<Image>();

        grocerySprite.color = color;

        newGroceryItem.name = name;

        Transform childQuantityTransform = newGroceryItem.transform.Find("QuantityText");

        TMP_Text quantityText = childQuantityTransform.GetComponent<TMP_Text>();

        quantityText.text = quantity.ToString();

        return newGroceryItem;
    }

    void RemoveGroceryItem(string name)
    {
        foreach (GameObject item in groceryList)
        {
            if (item.name == name)
            {
                Destroy(item);
            }
        }
    }

    void RemoveItemQuantity(string itemName, int amountRemoved)
    {
        foreach (GameObject item in groceryList)
        {
            if (item.name == itemName)
            {
                Transform itemTransform = item.transform.Find("QuantityText");
                TMP_Text itemQuantityText = itemTransform.GetComponent<TMP_Text>();
                int itemQuantity = int.Parse(itemQuantityText.text);
                itemQuantity -= amountRemoved;
                itemQuantityText.text = itemQuantity.ToString();

                if (itemQuantity <= 0)
                {
                    RemoveGroceryItem(item.name);
                }
            }
    }
    }
}