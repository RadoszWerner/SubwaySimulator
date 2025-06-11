using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Chests : MonoBehaviour
{
    public float interactionRange = 5f; // Zasiêg interakcji
    public string category = "Bread";   // Kategoria skrzynki
    private Inventory playerInventory;
    private static Chests activeChest = null; // Œledzi aktualnie aktywn¹ skrzynkê
    public OllamaClient ollamaClient;
    public scriptManager scriptM;
    private bool itemsLoaded = false;

    public List<BreadItem> breadItems;
    public List<MeatItem> meatItems;
    public List<VeggieItem> veggieItems;
    public List<SauceItem> sauceItems;
    public List<OrderItem> orderItems;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            //UnityEngine.Debug.Log($"Odleg³oœæ do skrzynki {category}: {distance}, zakres: {interactionRange}");

            // Ci¹g³e sprawdzanie odleg³oœci
            if (distance > interactionRange && activeChest == this)
            {
                UnityEngine.Debug.Log($"Gracz oddali³ siê od {category}, zamykam panel.");
                CloseSelectionPanel();
                activeChest = null; // Resetuj aktywn¹ skrzynkê
            }

            // Otwieranie panelu przy naciœniêciu E, jeœli w zasiêgu
            if (Input.GetKeyDown(KeyCode.E) && distance <= interactionRange)
            {
                playerInventory = player.GetComponent<Inventory>();
                if (playerInventory != null)
                {
                    UnityEngine.Debug.Log($"Otwieram panel dla kategorii: {category}");
                    OpenSelectionPanel();
                    activeChest = this; // Ustaw tê skrzynkê jako aktywn¹
                }
                else
                {
                    UnityEngine.Debug.LogError("PlayerInventory jest null!");
                }
            }
        }
    }

    void OpenSelectionPanel()
    {
        if (!itemsLoaded)
        {
            Debug.Log($"category: {category}");
            Debug.Log($"BreadSelectionUI.Instance: {BreadSelectionUI.Instance}");
            Debug.Log($"VeggieSelectionUI.Instance: {VeggieSelectionUI.Instance}");
            Debug.Log($"MeatSelectionUI.Instance: {MeatSelectionUI.Instance}");
            Debug.Log($"SauceSelectionUI.Instance: {SauceSelectionUI.Instance}");

            //Debug.LogError(""{ollamaClient.lastResponse}"");
            //Debug.LogError(scriptM.skladniki);
            if (ollamaClient == null || string.IsNullOrEmpty(ollamaClient.lastResponse))
            //{
                //Debug.LogError("Brak danych z Ollama, nie mo¿na za³adowaæ sk³adników!");
              //  return;
            //}
            LoadItemsFromOllama();
            itemsLoaded = true;
        }

        Debug.Log($"Próba otwarcia panelu dla {category}, Instancje: Bread={BreadSelectionUI.Instance != null}, Veggie={VeggieSelectionUI.Instance != null}, Meat={MeatSelectionUI.Instance != null}, Sauce={SauceSelectionUI.Instance != null}");
    
        switch (category.ToLower())
        {
            case "bread":
                if (BreadSelectionUI.Instance != null)
                    BreadSelectionUI.Instance.Show(breadItems, playerInventory);
                else
                    Debug.LogError("BreadSelectionUI.Instance jest null!");
                break;
            case "veggie":
                if (VeggieSelectionUI.Instance != null)
                    VeggieSelectionUI.Instance.Show(veggieItems, playerInventory);
                else
                    Debug.LogError("VeggieSelectionUI.Instance jest null!");
                break;
            case "meat":
                if (MeatSelectionUI.Instance != null)
                    MeatSelectionUI.Instance.Show(meatItems, playerInventory);
                else
                    Debug.LogError("MeatSelectionUI.Instance jest null!");
                break;
            case "sauce":
                if (SauceSelectionUI.Instance != null)
                    SauceSelectionUI.Instance.Show(sauceItems, playerInventory);
                else
                    Debug.LogError("SauceSelectionUI.Instance jest null!");
                break;
            case "order":
                if (OrderUI.Instance != null)
                    OrderUI.Instance.Show(orderItems, playerInventory);
                else
                    Debug.LogError("SauceSelectionUI.Instance jest null!");
                break;
            default:
                Debug.LogWarning($"Nieznana kategoria: {category}");
                break;
        }
    }

    void CloseSelectionPanel()
    {
        UnityEngine.Debug.Log("Zamykam panel.");
        if (BreadSelectionUI.Instance != null) BreadSelectionUI.Instance.Close();
        if (VeggieSelectionUI.Instance != null) VeggieSelectionUI.Instance.Close();
        if (MeatSelectionUI.Instance != null) MeatSelectionUI.Instance.Close();
        if (SauceSelectionUI.Instance != null) SauceSelectionUI.Instance.Close();
        if (OrderUI.Instance != null) OrderUI.Instance.Close();
    }


    public void LoadItemsFromOllama()
    {
        string projectRootPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        string path = Path.Combine(projectRootPath, "skladniki.json");

        if (!File.Exists(path))
        {
            Debug.LogError("Nie znaleziono pliku ze sk³adnikami: " + path);
            return;
        }

        string[] lines = File.ReadAllLines(path);

        if (lines.Length < 4)
        {
            Debug.LogError("Plik powinien zawieraæ co najmniej 4 linie (chleb, miêso, warzywa, sosy)");
            return;
        }

        breadItems = new List<BreadItem>();
        meatItems = new List<MeatItem>();
        veggieItems = new List<VeggieItem>();
        sauceItems = new List<SauceItem>();
        orderItems = new List<OrderItem>();

        // Parsowanie kategorii
        foreach (var item in lines[0].Split(','))
            breadItems.Add(new BreadItem { name = item.Trim() });

        foreach (var item in lines[1].Split(','))
            meatItems.Add(new MeatItem { name = item.Trim() });

        foreach (var item in lines[2].Split(','))
            veggieItems.Add(new VeggieItem { name = item.Trim() });

        foreach (var item in lines[3].Split(','))
            sauceItems.Add(new SauceItem { name = item.Trim() });

        //string pathsand = Path.Combine(Application.persistentDataPath, "kanapka.json");

        projectRootPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        string pathsand = Path.Combine(projectRootPath, "kanapka.json");

        if (!File.Exists(pathsand))
        {
            Debug.LogError("Nie znaleziono pliku ze sk³adnikami: " + pathsand);
            return;
        }

        string[] linesSan = File.ReadAllLines(pathsand);

        //Debug.Log("Œcie¿ka do pliku: " + pathsand);
        //Debug.Log("Zawartoœæ pliku:");
        //foreach (var line in linesSan)
        //{
          //  Debug.Log(line);
        //}

        foreach (var item in linesSan[0].Split(','))
        {
            var trimmed = item.Trim();
            orderItems.Add(new OrderItem { name = trimmed });
          //  Debug.Log($"Dodano do orderItems: '{trimmed}'");
        }


        Debug.Log("Za³adowano sk³adniki z pliku.");
    }
}