using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GroceryListManager : MonoBehaviour
{
    public static GroceryListManager Instance { get; private set; }
    public GameObject groceryItemOnList;
    public GameObject parentObject;
    public GameObject groceryOrange;
    public GameObject groceryTomato;
    public GameObject groceryMacAndCheese;

    private List<GameObject> spawnedGroceryObjects;
    private GameObject[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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


    void Start()
    {
        spawnedGroceryObjects = new List<GameObject> {};
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawn in spawnPoints)
        {
            int rng = UnityEngine.Random.Range(0, 3);
            if (rng == 0)
            {
                GameObject newGroceryObject = Instantiate(groceryOrange, spawn.transform);
                newGroceryObject.name = "Orange";
                spawnedGroceryObjects.Add(newGroceryObject);
            }
            if (rng == 1)
            {
                GameObject newGroceryObject = Instantiate(groceryTomato, spawn.transform);
                newGroceryObject.name = "Tomato";
                spawnedGroceryObjects.Add(newGroceryObject);
            }
            if (rng == 2)
            {
                GameObject newGroceryObject = Instantiate(groceryMacAndCheese, spawn.transform);
                newGroceryObject.name = "MacAndCheese";
                spawnedGroceryObjects.Add(newGroceryObject);
            }
        }

        if (spawnedGroceryObjects.Count > 0)
        {
            AddGroceryItem(groceryOrange.GetComponent<SpriteRenderer>().sprite,"Oranges", "OrangeList", spawnedGroceryObjects.Count(obj => obj.name == "Orange"));
            AddGroceryItem(groceryTomato.GetComponent<SpriteRenderer>().sprite,"Tomatoes", "TomatoList", spawnedGroceryObjects.Count(obj => obj.name == "Tomato"));
            AddGroceryItem(groceryMacAndCheese.GetComponent<SpriteRenderer>().sprite,"Mac and Cheeses", "MacAndCheeseList", spawnedGroceryObjects.Count(obj => obj.name == "MacAndCheese"));
        }

    }
    public GameObject AddGroceryItem(Sprite sprite, string text, string name, int quantity)
    {

        GameObject newGroceryItem = Instantiate(groceryItemOnList, parentObject.transform);

        Transform childTextTransform = newGroceryItem.transform.Find("ObjectText");

        TMP_Text groceryText = childTextTransform.GetComponent<TMP_Text>();

        groceryText.text = text;

        Transform childSpriteTransform = newGroceryItem.transform.Find("Image");

        Image grocerySprite = childSpriteTransform.GetComponent<Image>();

        grocerySprite.sprite = sprite;

        newGroceryItem.name = name;

        Transform childQuantityTransform = newGroceryItem.transform.Find("QuantityText");

        TMP_Text quantityText = childQuantityTransform.GetComponent<TMP_Text>();

        quantityText.text = quantity.ToString();

        if (quantity <= 0)
        {
            Destroy(newGroceryItem);
        }

        return newGroceryItem;
    }

    public void UpdateItemQuantity(GameObject groceryObject , int amountRemoved)
    {
        if (groceryObject.name == "Orange")
        {
            GameObject orangeItem = GameObject.Find("OrangeList");
            Transform objIntText = orangeItem.transform.Find("QuantityText");
            int objInt = int.Parse(objIntText.GetComponent<TMP_Text>().text);
            objInt -= amountRemoved;
            objIntText.GetComponent<TMP_Text>().text = objInt.ToString();

            if (objInt <= 0)
            {
                Destroy(orangeItem);
            }
        }
        if (groceryObject.name == "Tomato")
        {
            GameObject TomatoItem = GameObject.Find("TomatoList");
            Transform objIntText = TomatoItem.transform.Find("QuantityText");
            int objInt = int.Parse(objIntText.GetComponent<TMP_Text>().text);
            objInt -= amountRemoved;
            objIntText.GetComponent<TMP_Text>().text = objInt.ToString();

            if (objInt <= 0)
            {
                Destroy(TomatoItem);
            }
        }
        if (groceryObject.name == "MacAndCheese")
        {
            GameObject MacAndCheeseItem = GameObject.Find("MacAndCheeseList");
            Transform objIntText = MacAndCheeseItem.transform.Find("QuantityText");
            int objInt = int.Parse(objIntText.GetComponent<TMP_Text>().text);
            objInt -= amountRemoved;
            objIntText.GetComponent<TMP_Text>().text = objInt.ToString();

            if (objInt <= 0)
            {
                Destroy(MacAndCheeseItem);
            }
        }
        }
    }