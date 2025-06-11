using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Nazwa przedmiotu, np. "Chleb"
    public Sprite icon;     // Ikona przedmiotu wy�wietlana w slocie
    // Mo�esz doda� inne w�a�ciwo�ci, np. typ przedmiotu czy ilo��
}