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
            Debug.Log("Brak sk�adnik�w!");
            return;
        }

        Debug.Log("Stworzono kanapk� z kolejno�ci�:");
        foreach (string ingredient in sandwich)
        {
            Debug.Log(ingredient);
        }

        // Tutaj dodaj logik� gry (np. punktacj�)
    }
}