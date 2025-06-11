using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeggieSelectionUI : MonoBehaviour
{
    public static VeggieSelectionUI Instance;
    [SerializeField] private GameObject panel; 
    [SerializeField] private Transform grid; 
    [SerializeField] private GameObject veggieItemPrefab; 

    private Inventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("VeggieSelectionUI Instance zosta³ ustawiony.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(List<VeggieItem> veggies, Inventory inventory)
    {
        if (panel == null || grid == null || veggieItemPrefab == null)
        {
            panel = GameObject.Find("VeggieSelectionPanel");
            grid = panel.transform.Find("VeggieGrid");
            veggieItemPrefab = Resources.Load<GameObject>("Prefabs/VeggieItemUI");

            if (panel == null || grid == null || veggieItemPrefab == null)
            {
                UnityEngine.Debug.LogError("Nie uda³o siê przypisaæ komponentów w VeggieSelectionUI!");
                return;
            }
            UnityEngine.Debug.Log($"Przypisano: panel={panel.name}, grid={grid.name}, prefab={veggieItemPrefab.name}");
        }

        UnityEngine.Debug.Log($"Otwieram panel z {veggies.Count} warzywami.");
        playerInventory = inventory;
        panel.SetActive(true);

        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        foreach (VeggieItem veggie in veggies)
        {
            GameObject itemUI = Instantiate(veggieItemPrefab, grid);
            UnityEngine.UI.Text text = itemUI.GetComponentInChildren<UnityEngine.UI.Text>();
            if (text != null) text.text = veggie.name;

            Button button = itemUI.GetComponentInChildren<Button>();
            if (button != null) button.onClick.AddListener(() => SelectVeggie(veggie));
        }
    }

    public void Close()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void SelectVeggie(VeggieItem veggie)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(veggie);
            Close();
        }
    }
}