using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PrefabSelectionUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject selectionPanel; // Assign your main UI Panel for prefab selection
    public Transform prefabButtonParent;    // The parent object where your prefab buttons will be instantiated
    // public GameObject prefabButtonPrefab; // REMOVED - We will now directly instantiate prefabs from the InventorySystem list

    [Header("Selection Logic")]
    public GameObject currentlySelectedPrefab = null; // The prefab that is currently selected by the player

    // Event to notify when a prefab is selected
    public delegate void OnPrefabSelected(GameObject selectedPrefab);
    public event OnPrefabSelected onPrefabSelectedCallback;

    private InventorySystem inventorySystem;
    private List<GameObject> uiButtons = new List<GameObject>();

    void Start()
    {
        inventorySystem = InventorySystem.instance;
        if (inventorySystem == null)
        {
            Debug.LogError("InventorySystem instance not found! PrefabSelectionUI will not function.");
            enabled = false;
            return;
        }

        // Subscribe to changes in selectable prefabs (if you implement dynamic changes)
        inventorySystem.onSelectablePrefabsChangedCallback += UpdateSelectionUI;

        if (selectionPanel != null)
        {
            selectionPanel.SetActive(false); // Start with selection panel hidden
        }
        else
        {
            Debug.LogError("Selection Panel not assigned in PrefabSelectionUI script!");
        }

        if (prefabButtonParent == null) // REMOVED: prefabButtonPrefab == null check
        {
            Debug.LogError("Prefab Button Parent not assigned!");
            enabled = false;
            return;
        }

        UpdateSelectionUI(); // Initial UI setup
    }

      void Update()
    {
        Debug.Log("PrefabSelectionUI Update method running."); // DEBUG
        // Toggle selection panel with the "I" key
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I key pressed, attempting to toggle panel."); // DEBUG
            ToggleSelectionPanel();
        }
    }

    public void ToggleSelectionPanel()
    {
        if (selectionPanel != null)
        {
            Debug.Log("ToggleSelectionPanel called. Panel current activeSelf: " + selectionPanel.activeSelf); // DEBUG
            bool isActive = selectionPanel.activeSelf;
            selectionPanel.SetActive(!isActive);
            Debug.Log("ToggleSelectionPanel: Panel new activeSelf: " + selectionPanel.activeSelf); // DEBUG
            if (!isActive) // If we just opened it, refresh the UI
            {
                Debug.Log("ToggleSelectionPanel: Panel was opened, calling UpdateSelectionUI."); // DEBUG
                UpdateSelectionUI();
            }
        }
        else
        {
            Debug.LogError("ToggleSelectionPanel: selectionPanel is null!"); // DEBUG
        }
    }


     void UpdateSelectionUI()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("UpdateSelectionUI: inventorySystem is null!");
            return;
        }
        if (prefabButtonParent == null)
        {
            Debug.LogError("UpdateSelectionUI: prefabButtonParent is null! Cannot create buttons.");
            return;
        }

        Debug.Log("UpdateSelectionUI: Clearing existing buttons...");
        // Clear existing buttons
        foreach (Transform child in prefabButtonParent)
        {
            Destroy(child.gameObject);
        }

        List<GameObject> prefabsToDisplay = inventorySystem.GetSelectablePrefabs();
        Debug.Log($"UpdateSelectionUI: Found {prefabsToDisplay.Count} prefabs to display in InventorySystem.");

        if (prefabsToDisplay.Count == 0)
        {
            Debug.LogWarning("UpdateSelectionUI: No prefabs found in InventorySystem.SelectablePrefabs. No buttons will be created.");
            return;
        }

        foreach (GameObject prefabButtonSource in prefabsToDisplay)
        {
            if (prefabButtonSource == null)
            {
                Debug.LogWarning("UpdateSelectionUI: Encountered a null prefab in the selectablePrefabs list. Skipping.");
                continue;
            }

            Debug.Log($"UpdateSelectionUI: Attempting to instantiate button for prefab: {prefabButtonSource.name}");
            GameObject newButtonInstance = Instantiate(prefabButtonSource, prefabButtonParent);
            newButtonInstance.name = $"Button_{prefabButtonSource.name}"; // Give it a clear name in hierarchy
            
            newButtonInstance.SetActive(true); // <-- ADD THIS LINE TO ENSURE IT'S ACTIVE

            Button buttonComponent = newButtonInstance.GetComponent<Button>();
            if (buttonComponent != null)
            {
                // Store the source prefab to be passed to SelectPrefab
                GameObject sourcePrefabForButton = prefabButtonSource;
                buttonComponent.onClick.RemoveAllListeners(); // Clear any existing listeners from the prefab
                buttonComponent.onClick.AddListener(() => SelectPrefab(sourcePrefabForButton));
                Debug.Log($"UpdateSelectionUI: Added click listener to button for {sourcePrefabForButton.name}");
            }
            else
            {
                Debug.LogWarning($"UpdateSelectionUI: Instantiated button for {prefabButtonSource.name} but it's missing a Button component!");
            }

            // Optional: Set button text if your button prefabs have a child Text component
            // Text buttonText = newButtonInstance.GetComponentInChildren<Text>();
            // if (buttonText != null)
            // {
            //     buttonText.text = prefabButtonSource.name;
            // }
            // else
            // {
            //     Debug.LogWarning($"UpdateSelectionUI: Button for {prefabButtonSource.name} does not have a child Text component to set its name.");
            // }
        }
        Debug.Log("UpdateSelectionUI: Finished creating buttons.");
    }

    void SelectPrefab(GameObject prefabToSelect)
    {
        currentlySelectedPrefab = prefabToSelect;
        Debug.Log($"Prefab selected: {prefabToSelect.name}");
        onPrefabSelectedCallback?.Invoke(prefabToSelect); // Notify subscribers

        // Optional: Close the selection panel after selection
        if (selectionPanel != null)
        {
            selectionPanel.SetActive(false);
        }
        
        // Optional: Add visual feedback for selection
    }

    void OnDestroy()
    {
        if (inventorySystem != null)
        {
            inventorySystem.onSelectablePrefabsChangedCallback -= UpdateSelectionUI;
        }
    }
}
