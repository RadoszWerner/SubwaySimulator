using UnityEngine;

public class ExchangeStation : MonoBehaviour
{
    public float interactionRange = 5f;
    public int pointsPerSandwich = 10; // Punkty za kanapkê

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            //UnityEngine.Debug.Log($"Odleg³oœæ do stanowiska wymiany: {distance}, zakres: {interactionRange}");

            if (Input.GetKeyDown(KeyCode.E) && distance <= interactionRange)
            {
                Inventory inventory = player.GetComponent<Inventory>();
                if (inventory != null)
                {
                    ExchangeSandwich(inventory);
                }
                else
                {
                    UnityEngine.Debug.LogError("PlayerInventory jest null!");
                }
            }
        }
    }

    void ExchangeSandwich(Inventory inventory)
    {
        if (inventory.items.Count == 0)
        {
            UnityEngine.Debug.LogWarning("Ekwipunek jest pusty!");
            return;
        }

        foreach (object item in inventory.items)
        {
            if (item is SandwichItem sandwich && sandwich.ingredients.Count >= 2) // Minimum 2 sk³adniki
            {
                ScoreManager.Instance.AddScore(pointsPerSandwich);
                inventory.items.Remove(item);
                inventory.UpdateInventoryUI();
                UnityEngine.Debug.Log($"Wymieniono kanapkê za {pointsPerSandwich} punktów. Sk³adniki: {sandwich.GetIngredientsSummary()}");
                return; // Wymieniono jedn¹ kanapkê
            }
        }
        UnityEngine.Debug.LogWarning("Brak kanapki z co najmniej 2 sk³adnikami w ekwipunku!");
    }
}