using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GroceryListManager : MonoBehaviour
{
    public static GroceryListManager Instance { get; private set; }
    
    public GameObject groceryItemOnList;
    public GameObject parentObject;
    public GameObject groceryOrange;
    public GameObject groceryTomato;
    public GameObject groceryMacAndCheese;
    public GameObject groceryWatermelon;
    public GameObject groceryPizza;

    public List<GameObject> spawnedGroceryObjects = new List<GameObject>();
    private GameObject[] spawnPoints;
    private int minCashReward = 5;
    private int maxCashReward = 15;

    private Dictionary<string, (GameObject prefab, string displayName, string listName)> groceryData;

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

        InitializeGroceryData();
    }

    private void InitializeGroceryData()
    {
        groceryData = new Dictionary<string, (GameObject, string, string)>
        {
            { "Orange", (groceryOrange, "Oranges", "OrangeList") },
            { "Tomato", (groceryTomato, "Tomatoes", "TomatoList") },
            { "MacAndCheese", (groceryMacAndCheese, "Mac and Cheeses", "MacAndCheeseList") },
            { "Pizza", (groceryPizza, "Pizzas", "PizzaList") },
            { "Watermelon", (groceryWatermelon, "Watermelons", "WatermelonList") }
        };
    }

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        SpawnGroceries();
    }

    private void SpawnGroceries()
    {
        spawnedGroceryObjects.Clear();
        foreach (GameObject spawn in spawnPoints)
        {
            int rng = UnityEngine.Random.Range(0, groceryData.Count);
            var entry = new List<string>(groceryData.Keys)[rng];
            var groceryInfo = groceryData[entry];

            GameObject groceryObject = Instantiate(groceryInfo.prefab, spawn.transform);
            groceryObject.name = entry;
            spawnedGroceryObjects.Add(groceryObject);
        }

        foreach (var entry in groceryData)
        {
            int count = spawnedGroceryObjects.Count(obj => obj.name == entry.Key);
            if (count > 0)
            {
                AddGroceryItem(
                    entry.Value.prefab.GetComponent<SpriteRenderer>().sprite,
                    entry.Value.displayName,
                    entry.Value.listName,
                    count
                );
            }
        }
    }

    public GameObject AddGroceryItem(Sprite sprite, string text, string name, int quantity)
    {
        if (quantity <= 0) return null;

        GameObject newGroceryItem = Instantiate(groceryItemOnList, parentObject.transform);
        newGroceryItem.name = name;

        TMP_Text groceryText = newGroceryItem.transform.Find("ObjectText").GetComponent<TMP_Text>();
        groceryText.text = text;

        Image grocerySprite = newGroceryItem.transform.Find("Image").GetComponent<Image>();
        grocerySprite.sprite = sprite;

        TMP_Text quantityText = newGroceryItem.transform.Find("QuantityText").GetComponent<TMP_Text>();
        quantityText.text = quantity.ToString();

        return newGroceryItem;
    }

    public void UpdateItemQuantity(GameObject groceryObject, int amountRemoved)
    {
        string objectName = groceryObject.name;
        if (!groceryData.ContainsKey(objectName)) return;
        
        Score.Instance.PointBurst(Random.Range(minCashReward, maxCashReward));
        MoneyParticles.Instance.Burst();

        GameObject listItem = GameObject.Find(groceryData[objectName].listName);
        if (listItem == null) return;

        TMP_Text quantityText = listItem.transform.Find("QuantityText").GetComponent<TMP_Text>();
        int currentQuantity = int.Parse(quantityText.text);

        currentQuantity -= amountRemoved;
        quantityText.text = currentQuantity.ToString();

        if (currentQuantity <= 0)
        {
            Destroy(listItem);
            if (parentObject.transform.childCount <= 1)
            {
                SpawnGroceries();
            }
        }
    }
}