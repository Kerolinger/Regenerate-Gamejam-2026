using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private IngredientInfo ingredientInfo;
    [SerializeField] private string HoverText;
    [SerializeField] public GameObject ingredient3D;

    public void BTN_HoverInNotes()
    {
        uiManager.StageTwoNotes.text = HoverText;
    }

    public void BTN_HoverOutNotes()
    {
        uiManager.StageTwoNotes.text = "";
    }

}
