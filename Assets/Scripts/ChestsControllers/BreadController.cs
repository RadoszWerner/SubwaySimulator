using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BreadController : MonoBehaviour
{
    public GameObject breadUI;
    public Transform itemsParent;
    public GameObject itemButtonPrefab;

    private HashSet<string> breadItems = new HashSet<string>() { "Chleb", "Szynka", "Ser" };
    private bool playerInRange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            breadUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            breadUI.SetActive(!breadUI.activeSelf);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        foreach (Transform child in itemsParent) Destroy(child.gameObject);

        foreach (string item in breadItems)
        {
            GameObject button = Instantiate(itemButtonPrefab, itemsParent);
            button.GetComponent<BreadItemButton>().Setup(item, this);
        }
    }

    public void TakeItem(string itemType)
    {
        breadItems.Remove(itemType);
        PlayerInventory.Instance.PushItem(itemType);
        UpdateUI();
    }
}