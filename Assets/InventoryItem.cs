using UnityEngine;

[System.Serializable] // Make it visible in the Inspector and saveable
public class InventoryItem
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int quantity;
    // Add other relevant item properties here, e.g.:
    // public ItemType itemType;
    // public int itemID;
    // public bool isStackable;

    public InventoryItem(string name, string description, Sprite icon, int initialQuantity = 1)
    {
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
        quantity = initialQuantity;
    }

    // Method to add to the quantity of this item
    public void AddQuantity(int amount)
    {
        quantity += amount;
    }

    // Method to remove from the quantity of this item
    public void RemoveQuantity(int amount)
    {
        quantity -= amount;
        if (quantity < 0)
        {
            quantity = 0;
        }
    }
}