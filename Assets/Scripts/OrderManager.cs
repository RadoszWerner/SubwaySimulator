using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;
    public float minOrderInterval = 20f; //30 Minimalny czas miêdzy zamówieniami (sekundy)
    public float maxOrderInterval = 30f; //60 Maksymalny czas miêdzy zamówieniami (sekundy)
    public AudioClip orderSound; // DŸwiêk WAV
    private AudioSource audioSource;
    private SandwichOrder currentOrder; // Aktualne zamówienie
    public OllamaClient ollama;
    private string filePathSandwich;
    public string sandwichName;
    public Chests chests;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(OrderLoop());
    }

    private IEnumerator OrderLoop()
    {
        while (true)
        {
            Debug.LogWarning("lests gooo!");
            float waitTime = UnityEngine.Random.Range(minOrderInterval, maxOrderInterval);
            Debug.LogWarning("teraz!");

            string path = Path.Combine(Application.persistentDataPath, "skladniki.json");
            string[] lines = File.ReadAllLines(path);
            string promt = "Wypisz tylko jedn¹ nazwê kanapki z najczêstrzych " + lines[4] + " kanapek, bazuj¹c tylko na sk³adnikach: " + string.Join(", ", lines.Take(4)) + " oraz jej sk³adniki. Nie dodawaj wiêcej tekstu. Daj nazwe kanapki oraz sk³adniki po przecinku, nie dawaj innej interpunkcji. Pamiêtaj, ¿eby zwróciæ tylko jedn¹ linie z jedn¹ kanapk¹.";

            string jsonPayload = $@"{{
                ""model"": ""gemma3"",
                ""prompt"": ""{promt}""
            }}";

            
            //yield return StartCoroutine(FetchOrderFromAPI());

            if (ollama != null)
            {
                yield return StartCoroutine( ollama.SendOllamaRequest(jsonPayload) );
            }
            else
            {
                Debug.LogWarning("Nie przypisa³eœ referencji do OllamaClient w OrderManager!");
            }

            string kanapka = ollama.lastResponse;
            string projectRootPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            filePathSandwich = Path.Combine(projectRootPath, "kanapka.json");
            File.WriteAllText(filePathSandwich, kanapka);
            sandwichName = kanapka.Split(',')[0].Trim();
            Debug.Log(filePathSandwich);
            Debug.Log("filePathSandwich");
            PlayOrderSound();

            chests.LoadItemsFromOllama();

            yield return new WaitForSeconds(waitTime);
        }
    }

    //private IEnumerator FetchOrderFromAPI()
    //{
        // Pobierz losowy posi³ek z TheMealDB (np. z kategorii "Miscellaneous" dla ró¿norodnoœci)


        //tutaj prnt ollamy
        //StartCoroutine( ollama.SendOllamaRequest() );

        //UnityEngine.Debug.Log($"Wysy³am ¿¹danie do API: {url}");

        //using (UnityWebRequest request = UnityWebRequest.Get(url))
        //{
            //yield return request.SendWebRequest();

            //if (request.result != UnityWebRequest.Result.Success)
            //{
             //   UnityEngine.Debug.LogError($"B³¹d API: {request.error}");
            //    yield break;
            //}

            //string json = request.downloadHandler.text;
            //MealList mealList = JsonUtility.FromJson<MealList>(json);

            //if (mealList == null || mealList.meals == null || mealList.meals.Count == 0)
            //{
            //    UnityEngine.Debug.LogWarning("Brak danych z API.");
            //    yield break;
            //}

            //List<string> ingredients = mealList.meals[0].GetIngredients();
            //if (ingredients.Count == 0)
            //{
            //    UnityEngine.Debug.LogWarning("Brak sk³adników w zamówieniu.");
            //    yield break;
            //}

            //int maxIngredients = Mathf.Min(ingredients.Count, UnityEngine.Random.Range(2, 5));
            //currentOrder = new SandwichOrder
            //{
            //    name = mealList.meals[0].strMeal,
            //    requiredIngredients = ingredients.GetRange(0, maxIngredients)
            //};

            //UnityEngine.Debug.Log($"Nowe zamówienie: {currentOrder.name} z sk³adnikami: {string.Join(", ", currentOrder.requiredIngredients)}");
        //    PlayOrderSound();
        //}
    //}

    private void PlayOrderSound()
    {
        if (orderSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(orderSound);
        }
        else
        {
            UnityEngine.Debug.LogWarning("Brak dŸwiêku zamówienia lub AudioSource!");
        }
    }

    public SandwichOrder GetCurrentOrder()
    {
        return currentOrder;
    }
}

[System.Serializable]
public class SandwichOrder
{
    public string name;
    public List<string> requiredIngredients;

}