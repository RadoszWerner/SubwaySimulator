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

    string prompt = $"Wygeneruj dok�adnie 4 linie tekstu. Ka�da linia musi zawiera� dok�adnie 3 sk�adniki oddzielone przecinkami i spacj�. Ka�da linia reprezentuje jedn� z kategorii: pieczywo, mi�so, warzywa, sosy � w tej kolejno�ci. Sk�adniki maj� pochodzi� z najcz�ciej spotykanych w kanapkach typu {selected}. Nie dodawaj �adnych dodatkowych s��w, komentarzy, nag��wk�w ani wyja�nie� � tylko czyste linie sk�adnik�w.\n" +
        "Pami�taj w pierwszej lini pieczywo, w drugiej mi�sa, w trzeciej warzywa i w czwartej sosy.\n" +
        "Przyk�ad:\n" +
        "chleb �ytni, chleb bia�y, bagietka\n" +
        "mortadela, kurczak, salami pepperoni\n" +
        "sa�ata, pomidor, og�rek\n" +
        "ketchup, majonez, pesto";



    string escapedPrompt = prompt.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n");

    string jsonPayload = $@"{{
        ""model"": ""gemma3"",
        ""prompt"": ""{escapedPrompt}""
    }}";

    Debug.LogWarning("Wysy�anie zapytania do Ollamy...");

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
            Debug.Log("Odpowied� poprawnie odebrana. Przechodzenie do sceny...");
            SceneManager.LoadScene("game_interface");
        }
        else
        {
            Debug.LogWarning("Brak odpowiedzi z Ollamy � scena nie zostanie zmieniona.");
        }
    }
    else
    {
        Debug.LogWarning("Nie przypisa�e� referencji do OllamaClient w scriptManager!");
    }
}

}
