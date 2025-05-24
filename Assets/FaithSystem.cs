
using UnityEngine;
using System; // Required for Action

public class FaithSystem : MonoBehaviour
{
    [Header("Faith Settings")]
    [SerializeField] private float initialFaith = 0f;
    [SerializeField] private float faithPerIncrement = 1f;
    [SerializeField] private float incrementIntervalSeconds = 5f;

    private float currentFaith;
    private float timer;

    // Event to notify when faith amount changes
    public event Action<float> OnFaithChanged;

    #region Singleton
    public static FaithSystem instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of FaithSystem found! Destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        // DontDestroyOnLoad(gameObject); // Optional: if you want this manager to persist between scenes
    }
    #endregion

    void Start()
    {
        currentFaith = initialFaith;
        timer = 0f;
        OnFaithChanged?.Invoke(currentFaith); // Notify initial faith amount
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= incrementIntervalSeconds)
        {
            timer -= incrementIntervalSeconds; // Reset timer, keeping any overshoot
            AddFaith(faithPerIncrement);
        }
    }

    public void AddFaith(float amount)
    {
        if (amount <= 0) return;
        currentFaith += amount;
        Debug.Log($"Faith increased by {amount}. Current Faith: {currentFaith}");
        OnFaithChanged?.Invoke(currentFaith);
    }

    public bool SpendFaith(float amountToSpend)
    {
        if (amountToSpend <= 0)
        {
            Debug.LogWarning("Cannot spend zero or negative faith.");
            return false;
        }

        if (currentFaith >= amountToSpend)
        {
            currentFaith -= amountToSpend;
            Debug.Log($"Spent {amountToSpend} faith. Remaining Faith: {currentFaith}");
            OnFaithChanged?.Invoke(currentFaith);
            return true;
        }
        else
        {
            Debug.LogWarning($"Not enough faith to spend {amountToSpend}. Current Faith: {currentFaith}");
            return false;
        }
    }

    public float GetCurrentFaith()
    {
        return currentFaith;
    }

    // Optional: For loading/saving or direct setting
    public void SetFaith(float amount)
    {
        currentFaith = Mathf.Max(0, amount); // Ensure faith isn't negative
        OnFaithChanged?.Invoke(currentFaith);
    }
}
