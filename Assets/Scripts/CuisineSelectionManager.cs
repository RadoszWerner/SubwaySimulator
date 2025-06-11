using UnityEngine;
using UnityEngine.UI;

public class CuisineSelectionManager : MonoBehaviour
{
    public Button mexicanButton, americanButton, italianButton, startButton;
    public static string selectedCuisine = "";

    void Start()
    {
        startButton.interactable = false;

        mexicanButton.onClick.AddListener(() => SelectCuisine("meksykańskie"));
        americanButton.onClick.AddListener(() => SelectCuisine("amerykańskie"));
        italianButton.onClick.AddListener(() => SelectCuisine("włoskie"));
    }

    void SelectCuisine(string cuisine)
    {
        selectedCuisine = cuisine;
        Debug.Log("Wybrano kuchnię: " + cuisine);
        startButton.interactable = true;
    }

    public string GetSelectedCuisine()
    {
        return selectedCuisine;
    }
}

