using System;
using System.Collections.Generic;

[Serializable]
public class MealList
{
    public List<Meal> meals;
}

[Serializable]
public class Meal
{
    public string idMeal;
    public string strMeal;
    public string strIngredient1;
    public string strIngredient2;
    public string strIngredient3;
    public string strIngredient4;
    public string strIngredient5;
    public string strIngredient6;
    public string strIngredient7;
    public string strIngredient8;
    public string strIngredient9;
    public string strIngredient10;
    public string strIngredient11;
    public string strIngredient12;
    public string strIngredient13;
    public string strIngredient14;
    public string strIngredient15;
    public string strIngredient16;
    public string strIngredient17;
    public string strIngredient18;
    public string strIngredient19;
    public string strIngredient20;

    public List<string> GetIngredients()
    {
        List<string> ingredients = new List<string>();
        if (!string.IsNullOrEmpty(strIngredient1)) ingredients.Add(strIngredient1);
        if (!string.IsNullOrEmpty(strIngredient2)) ingredients.Add(strIngredient2);
        if (!string.IsNullOrEmpty(strIngredient3)) ingredients.Add(strIngredient3);
        if (!string.IsNullOrEmpty(strIngredient4)) ingredients.Add(strIngredient4);
        if (!string.IsNullOrEmpty(strIngredient5)) ingredients.Add(strIngredient5);
        if (!string.IsNullOrEmpty(strIngredient6)) ingredients.Add(strIngredient6);
        if (!string.IsNullOrEmpty(strIngredient7)) ingredients.Add(strIngredient7);
        if (!string.IsNullOrEmpty(strIngredient8)) ingredients.Add(strIngredient8);
        if (!string.IsNullOrEmpty(strIngredient9)) ingredients.Add(strIngredient9);
        if (!string.IsNullOrEmpty(strIngredient10)) ingredients.Add(strIngredient10);
        if (!string.IsNullOrEmpty(strIngredient11)) ingredients.Add(strIngredient11);
        if (!string.IsNullOrEmpty(strIngredient12)) ingredients.Add(strIngredient12);
        if (!string.IsNullOrEmpty(strIngredient13)) ingredients.Add(strIngredient13);
        if (!string.IsNullOrEmpty(strIngredient14)) ingredients.Add(strIngredient14);
        if (!string.IsNullOrEmpty(strIngredient15)) ingredients.Add(strIngredient15);
        if (!string.IsNullOrEmpty(strIngredient16)) ingredients.Add(strIngredient16);
        if (!string.IsNullOrEmpty(strIngredient17)) ingredients.Add(strIngredient17);
        if (!string.IsNullOrEmpty(strIngredient18)) ingredients.Add(strIngredient18);
        if (!string.IsNullOrEmpty(strIngredient19)) ingredients.Add(strIngredient19);
        if (!string.IsNullOrEmpty(strIngredient20)) ingredients.Add(strIngredient20);
        return ingredients;
    }
}