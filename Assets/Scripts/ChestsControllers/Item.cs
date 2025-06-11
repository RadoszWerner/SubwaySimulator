using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Nazwa przedmiotu, np. "Chleb"
    public Sprite icon;     // Ikona przedmiotu wyœwietlana w slocie
    // Mo¿esz dodaæ inne w³aœciwoœci, np. typ przedmiotu czy iloœæ
}