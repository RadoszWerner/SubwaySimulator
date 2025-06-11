using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    public static OrderUI Instance;
    [SerializeField] private GameObject panel; 
    [SerializeField] private Transform grid; 
    [SerializeField] private GameObject meatItemPrefab; 

    private Inventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UnityEngine.Debug.Log("OrderUI Instance zosta³ ustawiony.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(List<OrderItem> meats, Inventory inventory)
    {   
        UnityEngine.Debug.Log(meats);
        UnityEngine.Debug.Log(meats[0].name);
        UnityEngine.Debug.Log("ikuasdfbhkljuabns lkbnasdlkj bnaljk");
        UnityEngine.Debug.Log(panel);
        UnityEngine.Debug.Log(grid);
        UnityEngine.Debug.Log(meatItemPrefab);
        //panel = GameObject.Find("OrderSelectionPanel");
        //grid = panel.transform.Find("MeatGrid");


        if (panel == null || grid == null || meatItemPrefab == null)
        {
            panel = GameObject.Find("MeatSelectionPanel");
            grid = panel.transform.Find("MeatGrid");
            meatItemPrefab = Resources.Load<GameObject>("Prefabs/MeatItemUI");
            UnityEngine.Debug.Log("dupa ikuasdfbhkljuabns lkbnasdlkj bnaljk");


            if (panel == null || grid == null || meatItemPrefab == null)
            {
                UnityEngine.Debug.LogError("Nie uda³o siê przypisaæ komponentów w MeatSelectionUI!");
                return;
            }
            UnityEngine.Debug.Log($"Przypisano: panel={panel.name}, grid={grid.name}, prefab={meatItemPrefab.name}");

            UnityEngine.Debug.Log("kupa dupa ikuasdfbhkljuabns lkbnasdlkj bnaljk");

        }

        UnityEngine.Debug.Log($"Otwieram panel z {meats.Count} orderami.");
        playerInventory = inventory;
        panel.SetActive(true);

        for (int i = grid.childCount - 1; i >= 0; i--)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        for (int i = 0; i < meats.Count; i++)
        {
            OrderItem meat = meats[i];
            GameObject itemUI = Instantiate(meatItemPrefab, grid);

            Text text = itemUI.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = meat.name;

                if (i == 0)
                {
                    // Wyrównanie na górze i na œrodku
                    text.fontStyle = FontStyle.Bold;
                }
            }

            Button button = itemUI.GetComponentInChildren<Button>();
            if (button != null) button.onClick.AddListener(() => SelectMeat(meat));
        }
    }

    public void Close()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void SelectMeat(OrderItem meat)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(meat);
            Close();
        }
    }
}