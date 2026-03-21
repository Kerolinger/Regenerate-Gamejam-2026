using UnityEngine;

[CreateAssetMenu(fileName = "IngredientInfo", menuName = "Scriptable Objects/IngredientInfo")]
public class IngredientInfo : ScriptableObject
{
    [SerializeField] private Peeper.Ingredients displayedIngredient;
    [SerializeField] private Material displayedIngredientMaterial;

    public Peeper.Ingredients DisplayedIngredient { get => displayedIngredient; set => displayedIngredient = value; }
    public Material DisplayedIngredientMaterial { get => displayedIngredientMaterial; set => displayedIngredientMaterial = value; }
}
