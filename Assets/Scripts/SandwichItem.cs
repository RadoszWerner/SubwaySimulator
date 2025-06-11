using System.Collections.Generic;
using UnityEngine;

public class SandwichItem
{
    public string name = "Kanapka";
    public List<object> ingredients = new List<object>();

    public string GetIngredientsSummary()
    {
        if (ingredients.Count == 0) return "Brak sk�adnik�w";

        List<string> ingredientNames = new List<string>();
        foreach (object item in ingredients)
        {
            string itemName = "Nieznany sk�adnik";
            if (item is BreadItem bread) itemName = bread.name;
            else if (item is VeggieItem veggie) itemName = veggie.name;
            else if (item is MeatItem meat) itemName = meat.name;
            else if (item is SauceItem sauce) itemName = sauce.name;
            ingredientNames.Add(itemName);
        }
        return string.Join(", ", ingredientNames);
    }
}