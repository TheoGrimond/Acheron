using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> inventory;
    int capacity = 20;

    private class Item
    {
        //should this be an enum, or something else? like currency, armor,etc
        string category;
        string name;
        float price;
        //eg currency vs weapon
        bool stackable;
        int quantity;

        public Item(string category, string name, float price, bool stackable, int quantity)
        {
            this.category = category;
            this.name = name;
            this.price = price;
            this.stackable = stackable;
            this.quantity = quantity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<Item>();
        inventory.Add(new Item("Currency", "Coins", 1.0f, true, 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
