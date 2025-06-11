using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SauceSelectionUI : MonoBehaviour
{
    public static SauceSelectionUI Instance;
    [SerializeField] private GameObject panel; // Przypisz SauceSelectionPanel
    [SerializeField] private Transform grid; // Przypisz SauceGrid
    [SerializeField] private GameObject sauceItemPrefab; // Przypisz prefab SauceItemUI

    private Inventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("SauceSelectionUI Instance zosta³ ustawiony.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(List<SauceItem> sauces, Inventory inventory)
    {
        if (panel == null || grid == null || sauceItemPrefab == null)
        {
            panel = GameObject.Find("SauceSelectionPanel");
            grid = panel.transform.Find("SauceGrid");
            sauceItemPrefab = Resources.Load<GameObject>("Prefabs/SauceItemUI");

            if (panel == null || grid == null || sauceItemPrefab == null)
            {
                UnityEngine.Debug.LogError("Nie uda³o siê przypisaæ komponentów w SauceSelectionUI!");
                return;
            }
            UnityEngine.Debug.Log($"Przypisano: panel={panel.name}, grid={grid.name}, prefab={sauceItemPrefab.name}");
        }

        UnityEngine.Debug.Log($"Otwieram panel z {sauces.Count} sosami.");
        playerInventory = inventory;
        panel.SetActive(true);

        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        foreach (SauceItem sauce in sauces)
        {
            GameObject itemUI = Instantiate(sauceItemPrefab, grid);
            UnityEngine.UI.Text text = itemUI.GetComponentInChildren<UnityEngine.UI.Text>();
            if (text != null) text.text = sauce.name;

            Button button = itemUI.GetComponentInChildren<Button>();
            if (button != null) button.onClick.AddListener(() => SelectSauce(sauce));
        }
    }

    public void Close()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void SelectSauce(SauceItem sauce)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(sauce);
            Close();
        }
    }
}