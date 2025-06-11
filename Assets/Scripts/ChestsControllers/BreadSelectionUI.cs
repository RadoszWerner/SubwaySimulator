using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BreadSelectionUI : MonoBehaviour
{
    public static BreadSelectionUI Instance;
    public GameObject panel; // Przypisz panel w Inspectorze
    public Transform grid; // GridLayoutGroup zamiast Content ScrollView
    public GameObject breadItemPrefab; // Prefab elementu listy
    private Inventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("BreadSelectionUI Instance zosta³ ustawiony.");
        }
        else
        {
            Destroy(gameObject);
        }

        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            UnityEngine.Debug.LogError("Panel w BreadSelectionUI nie jest przypisany!");
        }
    }

    public void Show(List<BreadItem> breads, Inventory inventory)
    {
        if (panel == null || grid == null || breadItemPrefab == null)
        {
            UnityEngine.Debug.LogError("Brak przypisanych komponentów w BreadSelectionUI (panel, grid lub breadItemPrefab)!");
            return;
        }

        playerInventory = inventory;
        panel.SetActive(true);

        // Wyczyœæ poprzedni¹ zawartoœæ
        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        // Dodaj elementy do grida
        foreach (BreadItem bread in breads)
        {
            GameObject itemUI = Instantiate(breadItemPrefab, grid);
            UnityEngine.UI.Text text = itemUI.GetComponentInChildren<UnityEngine.UI.Text>();
            if (text != null)
            {
                text.text = bread.name;
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Brak komponentu Text w prefabie {bread.name}!");
            }

            Button button = itemUI.GetComponentInChildren<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => SelectBread(bread));
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Brak komponentu Button w prefabie {bread.name}!");
            }
        }
    }

    public void Close()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            UnityEngine.Debug.Log("Panel BreadSelectionUI zamkniêty.");
        }
        else
        {
            UnityEngine.Debug.LogError("Panel jest null podczas zamykania!");
        }
    }

    private void SelectBread(BreadItem bread)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(bread);
            panel.SetActive(false);
            UnityEngine.Debug.Log($"Wybrano {bread.name}, panel zamkniêty.");
        }
        else
        {
            UnityEngine.Debug.LogError("PlayerInventory jest null w BreadSelectionUI!");
        }
    }
}