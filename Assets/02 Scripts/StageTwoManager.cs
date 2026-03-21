using UnityEngine;

public class StageTwoManager : MonoBehaviour
{
    public GameManager gameManager;
    public UIManager uiManager;

    public GameObject cookSoupButton;
    public GameObject[] buttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        cookSoupButton.SetActive(false);
    }

    public void BTN_PutInIngredient(int i)
    {
        gameManager.AddIngredientToPot(i);
        buttons[i].SetActive(false);
        buttons[i].GetComponent<Ingredient>().ingredient3D.SetActive(false);
    }

    public void BTN_ResetAll()
    {
        gameManager.ResetSoup();

        foreach (GameObject g in buttons)
        {
            g.SetActive(true);
            g.GetComponent<Ingredient>().ingredient3D.SetActive(true);
        }

    }

    public void BTN_CookSoup()
    {
        gameManager.SwitchStage(3);
        gameObject.SetActive(false);
    }

    void EnterHoverIngredientInfo()
    {

    }

    void ExitHoverIngredientInfo()
    {

    }
}
