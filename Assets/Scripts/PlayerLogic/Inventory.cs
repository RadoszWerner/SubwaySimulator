using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<object> items = new List<object>();

    public void AddItem(object item)
    {
        UnityEngine.Debug.Log($"Próba dodania do ekwipunku: {item} ({item.GetType()})");
        if (item is BreadItem || item is VeggieItem || item is MeatItem || item is SauceItem || item is SandwichItem)
        {
            items.Add(item);
            UnityEngine.Debug.Log($"Dodano {item.GetType().Name}: {GetItemName(item)} do ekwipunku!");
            UpdateInventoryUI();
        }
    }

    public void RemoveLatestItem()
    {
        if (items.Count > 0)
        {
            object removedItem = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            UnityEngine.Debug.Log($"Usuniêto {removedItem.GetType().Name}: {GetItemName(removedItem)} z ekwipunku.");
            UpdateInventoryUI();
        }
        else
        {
            UnityEngine.Debug.Log("Ekwipunek jest pusty!");
        }
    }

    private string GetItemName(object item)
    {
        if (item is BreadItem bread) return bread.name;
        if (item is VeggieItem veggie) return veggie.name;
        if (item is MeatItem meat) return meat.name;
        if (item is SauceItem sauce) return sauce.name;
        if (item is SandwichItem sandwich) return sandwich.name;
        return "kanapka";
    }

    public void UpdateInventoryUI()
    {
        InventoryUI inventoryUI = FindFirstObjectByType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.UpdateUI(items);
        }
    }
}