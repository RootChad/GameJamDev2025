
using UnityEngine;
using System.Collections.Generic;
using System; // Required for Action

public class ShopSystem : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string itemName = "New Shop Item";
        public GameObject itemPrefab; // The prefab that will be added to the inventory/selection system
        public float faithPrice = 10f;
        // Future additions: public Sprite itemIcon;
    }

    [Header("Shop Configuration")]
    public List<ShopItem> itemsForSale = new List<ShopItem>();

    // Event to notify when an item is successfully purchased
    public event Action<ShopItem> OnItemPurchasedSuccessfully;

    #region Singleton
    public static ShopSystem instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of ShopSystem found! Destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        // DontDestroyOnLoad(gameObject); // Optional: if you want this manager to persist
    }
    #endregion

    void Start()
    {
        // Basic validation
        if (FaithSystem.instance == null)
        {
            Debug.LogError("ShopSystem: FaithSystem instance not found! Purchases will not work.");
        }
        if (InventorySystem.instance == null)
        {
            Debug.LogError("ShopSystem: InventorySystem instance not found! Purchased items cannot be added to inventory.");
        }
    }
void Update()
{
    if (Input.GetKeyDown(KeyCode.P)) // Press P to try and buy the first shop item
    {
        if (ShopSystem.instance != null && ShopSystem.instance.itemsForSale.Count > 0)
        {
            ShopSystem.instance.AttemptPurchase(ShopSystem.instance.itemsForSale[0]);
        }
    }
}
    public bool AttemptPurchase(ShopItem itemToPurchase)
    {
        if (itemToPurchase == null || itemToPurchase.itemPrefab == null)
        {
            Debug.LogError("ShopSystem: Attempted to purchase a null or invalid ShopItem.");
            return false;
        }

        if (FaithSystem.instance == null)
        {
            Debug.LogError("ShopSystem: FaithSystem not available to process purchase.");
            return false;
        }

        if (InventorySystem.instance == null)
        {
            Debug.LogError("ShopSystem: InventorySystem not available to add purchased item.");
            return false;
        }

        // Check if player has enough faith
        if (FaithSystem.instance.GetCurrentFaith() >= itemToPurchase.faithPrice)
        {
            // Attempt to spend faith
            if (FaithSystem.instance.SpendFaith(itemToPurchase.faithPrice))
            {
                // Add item to inventory/selection system
                InventorySystem.instance.AddPrefabToSelection(itemToPurchase.itemPrefab);
                
                Debug.Log($"ShopSystem: Successfully purchased '{itemToPurchase.itemName}' ({itemToPurchase.itemPrefab.name}) for {itemToPurchase.faithPrice} faith. Item added to inventory.");
                OnItemPurchasedSuccessfully?.Invoke(itemToPurchase);
                return true;
            }
            else
            {
                // This case should ideally not be hit if GetCurrentFaith check passed, but good for safety.
                Debug.LogWarning($"ShopSystem: Purchase of '{itemToPurchase.itemName}' failed. Could not spend faith (FaithSystem reported failure).");
                return false;
            }
        }
        else
        {
            Debug.LogWarning($"ShopSystem: Not enough faith to purchase '{itemToPurchase.itemName}'. Needs {itemToPurchase.faithPrice}, has {FaithSystem.instance.GetCurrentFaith()}.");
            return false;
        }
    }

    // Helper to get all items if needed for UI
    public List<ShopItem> GetItemsForSale()
    {
        return itemsForSale;
    }

    // Example of how you might call this from a UI button (you'd need to know which item the button corresponds to)
    // public void PurchaseItemByIndex(int itemIndex)
    // {
    //     if (itemIndex >= 0 && itemIndex < itemsForSale.Count)
    //     {
    //         AttemptPurchase(itemsForSale[itemIndex]);
    //     }
    //     else
    //     {
    //     Debug.LogError($"ShopSystem: Invalid item index {itemIndex} for purchase.");
    //     }
    // }
}
