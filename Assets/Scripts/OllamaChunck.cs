[System.Serializable]
public class OllamaChunk
{
    public string response;
    public bool done;       // nie musimy go wykorzystywa�, ale dobrze mie� w definicji, bo Ollama zwraca to pole
}