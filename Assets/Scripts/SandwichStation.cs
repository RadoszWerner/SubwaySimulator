using UnityEngine;

public class SandwichStation : MonoBehaviour
{
    public float interactionRange = 5f;
    private Inventory playerInventory;
    public OrderManager orderManager;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            //UnityEngine.Debug.Log($"Odleg³oœæ do stanowiska: {distance}, zakres: {interactionRange}");

            if (Input.GetKeyDown(KeyCode.E) && distance <= interactionRange)
            {
                Inventory inventory = player.GetComponent<Inventory>();
                if (inventory != null)
                {
                    CreateSandwich(inventory);
                }
                else
                {
                    UnityEngine.Debug.LogError("PlayerInventory jest null!");
                }
            }
        }
    }

    void CreateSandwich(Inventory inventory)
    {
        if (inventory.items.Count < 2)
        {
            UnityEngine.Debug.LogWarning("Za ma³o sk³adników w ekwipunku (potrzeba co najmniej 2)!");
            return;
        }

        SandwichItem sandwich = new SandwichItem();
        sandwich.name = orderManager.sandwichName;
        sandwich.ingredients.AddRange(inventory.items);
        inventory.items.Clear();
        inventory.AddItem(sandwich);
        //inventory.UpdateInventoryUI();
        UnityEngine.Debug.Log($"Stworzono kanapkê z: {sandwich.GetIngredientsSummary()}");
    }
}