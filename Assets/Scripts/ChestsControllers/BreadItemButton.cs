using UnityEngine;
using UnityEngine.UI;

public class BreadItemButton : MonoBehaviour
{
    [SerializeField] private Text itemNameText;
    private string itemType;
    private BreadController bread;

    public void Setup(string type, BreadController controller)
    {
        itemType = type;
        itemNameText.text = type;
        bread = controller;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick() => bread.TakeItem(itemType);
}