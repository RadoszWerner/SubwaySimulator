using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemList;
    public GameObject inventoryItemPrefab;
    public Button removeButton;

    private void Start()
    {
        if (inventoryPanel != null) inventoryPanel.SetActive(true);
        if (removeButton != null) removeButton.onClick.AddListener(RemoveLatestItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveLatestItem();
        }
    }

    public void UpdateUI(List<object> items)
    {
        if (itemList == null || inventoryItemPrefab == null) return;

        for (int i = itemList.childCount - 1; i >= 0; i--)
        {
            Destroy(itemList.GetChild(i).gameObject);
        }

        for (int i = items.Count - 1; i >= 0; i--)
        {
            object item = items[i];
            GameObject itemUI = Instantiate(inventoryItemPrefab, itemList, false);
            UnityEngine.UI.Text text = itemUI.GetComponentInChildren<UnityEngine.UI.Text>();
            text.fontSize = 22;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            //text.verticalOverflow = VerticalWrapMode.Overflow;
            if (text != null)
            {
                text.text = item switch
                {
                    BreadItem bread => bread.name,
                    VeggieItem veggie => veggie.name,
                    MeatItem meat => meat.name,
                    SauceItem sauce => sauce.name,
                    SandwichItem sandwich => sandwich.name,
                    OrderItem order => order.name,
                    _ => "Unknown"
                };
            }
        }
    }

    private void RemoveLatestItem()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null) inventory.RemoveLatestItem();
    }
}