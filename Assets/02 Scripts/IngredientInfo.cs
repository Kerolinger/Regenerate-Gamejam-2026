using UnityEngine;

[CreateAssetMenu(fileName = "IngredientInfo", menuName = "Scriptable Objects/IngredientInfo")]
public class IngredientInfo : ScriptableObject
{
    [SerializeField] private PeeperProfile.Ingredients displayedIngredient;
    [SerializeField] private Material displayedIngredientMaterial;

    public PeeperProfile.Ingredients DisplayedIngredient { get => displayedIngredient; set => displayedIngredient = value; }
    public Material DisplayedIngredientMaterial { get => displayedIngredientMaterial; set => displayedIngredientMaterial = value; }
}
