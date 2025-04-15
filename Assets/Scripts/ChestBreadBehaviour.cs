using UnityEngine;
using UnityEngine.UI;

public class BreadBoxInteraction : MonoBehaviour
{
    public GameObject breadSelectionPanel;
    public Button[] breadButtons;
    private bool isPlayerNear;

    void Start()
    {
        breadSelectionPanel.SetActive(false);
        SetupBreadButtons();
    }

    void Update()
    {
        // Automatycznie zamykaj menu jeœli gracz odszed³
        if (!isPlayerNear && breadSelectionPanel.activeSelf)
        {
            breadSelectionPanel.SetActive(false);
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.Return))
        {
            ToggleBreadMenu();
        }
    }

    void SetupBreadButtons()
    {
        breadButtons[0].onClick.AddListener(() => SelectBread("Baguette"));
        breadButtons[1].onClick.AddListener(() => SelectBread("Ciabatta"));
    }

    void ToggleBreadMenu()
    {
        breadSelectionPanel.SetActive(!breadSelectionPanel.activeSelf);
    }

    void SelectBread(string breadType)
    {
        PlayerInventory.Instance.AddIngredient(breadType);
        breadSelectionPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}