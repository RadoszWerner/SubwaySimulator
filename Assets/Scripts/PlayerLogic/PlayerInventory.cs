using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public Transform inventoryPanel;
    public GameObject inventoryItemPrefab;

    private Stack<string> itemStack = new Stack<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PushItem(string item)
    {
        itemStack.Push(item);
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel) Destroy(child.gameObject);

        foreach (string item in itemStack.Reverse())
        {
            GameObject obj = Instantiate(inventoryItemPrefab, inventoryPanel);
            obj.GetComponent<Text>().text = item;
        }
    }

    public bool CheckSequence(List<string> requiredSequence)
    {
        return itemStack.SequenceEqual(requiredSequence);
    }
    public List<string> CreateSandwich()
    {
        if (itemStack.Count == 0)
            return new List<string>();

        // Zwracamy sk³adniki w kolejnoœci dodawania (od pierwszego do ostatniego)
        List<string> sandwich = itemStack.Reverse().ToList();

        // Kasujemy ekwipunek
        itemStack.Clear();
        UpdateInventoryUI();

        return sandwich;
    }
}