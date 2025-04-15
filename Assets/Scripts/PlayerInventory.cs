using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public Text inventoryText;
    private Stack<string> ingredients = new Stack<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddIngredient(string ingredient)
    {
        ingredients.Push(ingredient);
        UpdateUI();
    }

    public List<string> CreateSandwich()
    {
        List<string> sandwich = new List<string>(ingredients);
        ingredients.Clear();
        UpdateUI();
        return sandwich; // Zwraca kolejnoœæ od najstarszego sk³adnika
    }

    void UpdateUI()
    {
        // Odwróæ kolejnoœæ, aby pokazaæ od "do³u" (pierwszy dodany sk³adnik na dole)
        string[] reversed = ingredients.ToArray();
        System.Array.Reverse(reversed);

        inventoryText.text = "Sk³adniki kanapki:\n" + string.Join("\n", reversed);
    }
}