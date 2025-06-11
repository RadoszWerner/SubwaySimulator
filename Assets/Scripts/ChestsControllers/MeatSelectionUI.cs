using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeatSelectionUI : MonoBehaviour
{
    public static MeatSelectionUI Instance;
    [SerializeField] private GameObject panel; 
    [SerializeField] private Transform grid; 
    [SerializeField] private GameObject meatItemPrefab; 

    private Inventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("MeatSelectionUI Instance zosta³ ustawiony.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(List<MeatItem> meats, Inventory inventory)
    {
        if (panel == null || grid == null || meatItemPrefab == null)
        {
            panel = GameObject.Find("MeatSelectionPanel");
            grid = panel.transform.Find("MeatGrid");
            meatItemPrefab = Resources.Load<GameObject>("Prefabs/MeatItemUI");

            if (panel == null || grid == null || meatItemPrefab == null)
            {
                UnityEngine.Debug.LogError("Nie uda³o siê przypisaæ komponentów w MeatSelectionUI!");
                return;
            }
            UnityEngine.Debug.Log($"Przypisano: panel={panel.name}, grid={grid.name}, prefab={meatItemPrefab.name}");
        }

        UnityEngine.Debug.Log($"Otwieram panel z {meats.Count} miêsami.");
        playerInventory = inventory;
        panel.SetActive(true);

        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        foreach (MeatItem meat in meats)
        {
            GameObject itemUI = Instantiate(meatItemPrefab, grid);
            UnityEngine.UI.Text text = itemUI.GetComponentInChildren<UnityEngine.UI.Text>();
            if (text != null) text.text = meat.name;

            Button button = itemUI.GetComponentInChildren<Button>();
            if (button != null) button.onClick.AddListener(() => SelectMeat(meat));
        }
    }

    public void Close()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void SelectMeat(MeatItem meat)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(meat);
            Close();
        }
    }
}