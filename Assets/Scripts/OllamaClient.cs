using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class OllamaClient : MonoBehaviour
{
    [SerializeField] private string ollamaUrl = "http://localhost:11434/api/generate";
    public string lastResponse;

    public IEnumerator SendOllamaRequest(string jsonPayload)
{
    using (UnityWebRequest request = new UnityWebRequest(ollamaUrl, "POST"))
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Wysyłam zapytanie do Ollama: " + ollamaUrl);
        yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
        {
            Debug.LogError($"Ollama request error: {request.error}");
            lastResponse = null; // <- upewnij się, że to puste
            yield break;
        }

        string fullStream = request.downloadHandler.text;
        string[] lines = fullStream.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();

        foreach (string line in lines)
        {
            try
            {
                OllamaChunk chunk = JsonUtility.FromJson<OllamaChunk>(line);
                if (chunk != null && !string.IsNullOrEmpty(chunk.response))
                {
                    sb.Append(chunk.response);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Nie udało się sparsować linii: {line}\nBłąd: {e.Message}");
            }
        }

        string finalText = sb.ToString();
        Debug.Log("Otrzymana odpowiedź (tekst): " + finalText);

        // ⬇⬇⬇ KLUCZOWE ⬇⬇⬇
        lastResponse = finalText;
    }
}
}
