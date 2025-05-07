using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SandwichStation : MonoBehaviour
{
    [SerializeField] private List<string> requiredOrder = new List<string> { "Chleb", "Szynka", "Ser" };

    public void OnInteract()
    {
        List<string> sandwich = PlayerInventory.Instance.CreateSandwich();

        if (sandwich.Count == 0)
        {
            Debug.Log("Najpierw weź składniki z lodówki!");
            return;
        }

        bool isCorrect = sandwich.SequenceEqual(requiredOrder);

        if (isCorrect)
        {
            Debug.Log("PERFEKCYJNA KANAPKA! 🥪");
            // Tutaj nagroda: np. GameManager.Instance.AddPoints(100);
        }
        else
        {
            Debug.Log("Zła kolejność! Otrzymujesz:");
            foreach (string ingredient in sandwich)
            {
                Debug.Log($"- {ingredient}");
            }
        }
    }
}