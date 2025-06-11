using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class scriptManager : MonoBehaviour
{
    public string sceneName;
    public OllamaClient ollama;
    public string skladniki;
    private string filePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        StartCoroutine(ollamaReqAndChangeScene());
    }

    public IEnumerator ollamaReqAndChangeScene()
{
    string selected = CuisineSelectionManager.selectedCuisine;

    string prompt = $"Wygeneruj dok³adnie 4 linie tekstu. Ka¿da linia musi zawieraæ dok³adnie 3 sk³adniki oddzielone przecinkami i spacj¹. Ka¿da linia reprezentuje jedn¹ z kategorii: pieczywo, miêso, warzywa, sosy — w tej kolejnoœci. Sk³adniki maj¹ pochodziæ z najczêœciej spotykanych w kanapkach typu {selected}. Nie dodawaj ¿adnych dodatkowych s³ów, komentarzy, nag³ówków ani wyjaœnieñ — tylko czyste linie sk³adników.\n" +
        "Pamiêtaj w pierwszej lini pieczywo, w drugiej miêsa, w trzeciej warzywa i w czwartej sosy.\n" +
        "Przyk³ad:\n" +
        "chleb ¿ytni, chleb bia³y, bagietka\n" +
        "mortadela, kurczak, salami pepperoni\n" +
        "sa³ata, pomidor, ogórek\n" +
        "ketchup, majonez, pesto";



    string escapedPrompt = prompt.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n");

    string jsonPayload = $@"{{
        ""model"": ""gemma3"",
        ""prompt"": ""{escapedPrompt}""
    }}";

    Debug.LogWarning("Wysy³anie zapytania do Ollamy...");

    if (ollama != null)
    {
        yield return StartCoroutine(ollama.SendOllamaRequest(jsonPayload));
        Debug.Log("Otrzymany tekst: " + ollama.lastResponse);
        skladniki = ollama.lastResponse + selected;

        string projectRootPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        //string pathsand = Path.Combine(projectRootPath, "kanapka.json");

        filePath = Path.Combine(projectRootPath, "skladniki.json");
        Debug.Log(filePath);
        Debug.Log("filePath");
        File.WriteAllText(filePath, skladniki);
        Debug.Log(skladniki);


        if (!string.IsNullOrEmpty(ollama.lastResponse))
        {
            Debug.Log("OdpowiedŸ poprawnie odebrana. Przechodzenie do sceny...");
            SceneManager.LoadScene("game_interface");
        }
        else
        {
            Debug.LogWarning("Brak odpowiedzi z Ollamy – scena nie zostanie zmieniona.");
        }
    }
    else
    {
        Debug.LogWarning("Nie przypisa³eœ referencji do OllamaClient w scriptManager!");
    }
}

}
