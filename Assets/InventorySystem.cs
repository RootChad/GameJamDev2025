using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    [Header("Prefab Management")]
    public List<GameObject> selectablePrefabs = new List<GameObject>(); // List of all prefabs you want to be able to select

    // Optional: Event to notify if the list of selectable prefabs changes (e.g., if unlocked dynamically)
    public delegate void OnSelectablePrefabsChanged();
    public event OnSelectablePrefabsChanged onSelectablePrefabsChangedCallback;

    #region Singleton
    public static InventorySystem instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of InventorySystem found! Destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        // DontDestroyOnLoad(gameObject); // Optional: if you want this manager to persist between scenes
    }
    #endregion

    // Method to get the list of prefabs
    public List<GameObject> GetSelectablePrefabs()
    {
        return selectablePrefabs;
    }

    // Example: If you were to add/remove prefabs dynamically at runtime
    public void AddPrefabToSelection(GameObject prefab)
    {
        if (!selectablePrefabs.Contains(prefab))
        {
            selectablePrefabs.Add(prefab);
            onSelectablePrefabsChangedCallback?.Invoke();
        }
    }

    public void RemovePrefabFromSelection(GameObject prefab)
    {
        if (selectablePrefabs.Remove(prefab))
        {
            onSelectablePrefabsChangedCallback?.Invoke();
        }
    }

    // --- All methods related to BlockData, InventoryItem, AddItem, RemoveItem, etc., have been removed ---
    // --- as the system is now focused on managing a list of selectable GameObjects. ---
}