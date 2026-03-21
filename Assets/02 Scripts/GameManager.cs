using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PeeperProfile[] peepers;
    [SerializeField] private IngredientInfo[] ingredients;
    [SerializeField] private GameObject peeperPrefab;

    [Header("Ingame Tools")]
    [SerializeField] private bool disableTutorial;


    private GameStage currentGameStage;
    private int currentPeeperIndex;
    private GameObject currentPeeper;

    public GameStage CurrentGameStage { get => currentGameStage; set => currentGameStage = value; }

    public enum GameStage { stage01, stage02, stage03};


    private void Start()
    {
        if (!disableTutorial)
            uiManager.PlayTutorial();

        SwitchStage(1);
    }

    private IEnumerator StartPeeperEncounterRoutine()
    {
        yield return new WaitForSeconds(Random.Range(1, 3f));
        Debug.Log("Doorbell Sound!");
        yield return new WaitForSeconds(Random.Range(1, 1.5f));

        StartPeeperEncounter();
    }

    private void StartPeeperEncounter()
    {
        currentPeeper = Instantiate(peeperPrefab);
        currentPeeper.transform.position = new Vector3(2.5f, -5, 7f);
        currentPeeper.transform.DOMove(new Vector3(2.5f, 0, 7f), 0.5f ).SetEase(Ease.OutSine);
        currentPeeper.GetComponent<Peeper>().SetPeeper(peepers[currentPeeperIndex], GetIngredientMaterial(peepers[currentPeeperIndex].Ingredient));

        uiManager.TriggerDialogue(peepers[currentPeeperIndex], peepers[currentPeeperIndex].St01_ingredientText);

        currentPeeperIndex++;
    }

    public void RemovePeeper()
    {
        currentPeeper.transform.DOMove(new Vector3(2.5f, -5, 7f), 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            Destroy(currentPeeper);
            currentPeeper = null;
            Debug.Log("remove peeper: currentpeeperindex is " + currentPeeperIndex + "and length is" + peepers.Length);
            if (currentPeeperIndex >= peepers.Length)
            {
                uiManager.DisplayStage02Button();
            }
            else   
                StartCoroutine(StartPeeperEncounterRoutine());
        });
    }

    private Material GetIngredientMaterial(PeeperProfile.Ingredients i)
    {
        switch(i)
        {
            case PeeperProfile.Ingredients.ingredient01:
                return ingredients[0].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.ingredient02:
                return ingredients[1].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.ingredient03:
                return ingredients[2].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.ingredient04:
                return ingredients[3].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.ingredient05:
                return ingredients[4].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.ingredient06:
                return ingredients[5].DisplayedIngredientMaterial;
        }

        return null;
    }

    public void SwitchStage(int newStageindex)
    {

        switch (newStageindex)
        {
            case 1:
                CurrentGameStage = GameStage.stage01;

                CurrentGameStage = GameStage.stage01;
                currentPeeperIndex = 0;

                StartCoroutine(StartPeeperEncounterRoutine());
                break;
            case 2:
                CurrentGameStage = GameStage.stage02;
                break;
            case 3:
                CurrentGameStage = GameStage.stage03;
                break;
        }
    }
}
