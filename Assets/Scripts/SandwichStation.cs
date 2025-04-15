using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SandwichStation : MonoBehaviour
{
    public void OnInteract()
    {
        List<string> sandwich = PlayerInventory.Instance.CreateSandwich();

        if (sandwich.Count == 0)
        {
            Debug.Log("Brak sk³adników!");
            return;
        }

        Debug.Log("Stworzono kanapkê z kolejnoœci¹:");
        foreach (string ingredient in sandwich)
        {
            Debug.Log(ingredient);
        }

        // Tutaj dodaj logikê gry (np. punktacjê)
    }
}