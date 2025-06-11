[System.Serializable]
public class OllamaChunk
{
    public string response;
    public bool done;       // nie musimy go wykorzystywaæ, ale dobrze mieæ w definicji, bo Ollama zwraca to pole
}