using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public Transform currentCamera;

    [Header ("References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PeeperProfile[] peepers;
    [SerializeField] private IngredientInfo[] ingredients;
    [SerializeField] private GameObject peeperPrefab;
    [SerializeField] private Transform[] cameraTransforms;
    [SerializeField] private Transform handTransform;

    [Header("Ingame Tools")]
    [SerializeField] private bool disableTutorial;


    private GameStage currentGameStage;
    private int currentPeeperIndex;
    private GameObject currentPeeper;
    private Camera currentCameraCamera;

    public GameStage CurrentGameStage { get => currentGameStage; set => currentGameStage = value; }

    public enum GameStage { stage01, stage02, stage03};

    private bool enableHandMovement;


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
        currentPeeper.transform.position = new Vector3(5f, -5, 1f);
        currentPeeper.transform.DOMove(new Vector3(5f, 0, 1f), 0.5f ).SetEase(Ease.OutSine);
        currentPeeper.transform.DORotate(new Vector3(0, -230f, 0), 0.1f);
        currentPeeper.GetComponent<Peeper>().SetPeeper(peepers[currentPeeperIndex], GetIngredientMaterial(peepers[currentPeeperIndex].Ingredient),this);

        uiManager.TriggerDialogue(peepers[currentPeeperIndex], peepers[currentPeeperIndex].St01_ingredientText);

        currentPeeperIndex++;
    }

    public void RemovePeeper()
    {
        currentPeeper.transform.DOMove(new Vector3(5f, -5, 1f), 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
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
            case PeeperProfile.Ingredients.carrot:
                return ingredients[0].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.aubergine:
                return ingredients[1].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.chickpeas:
                return ingredients[2].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.beetroot:
                return ingredients[3].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.potato:
                return ingredients[4].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.chilli:
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

                currentCamera = cameraTransforms[0];
                currentCameraCamera = currentCamera.GetComponentInChildren<Camera>();
                cameraTransforms[0].gameObject.SetActive(true);
                cameraTransforms[1].gameObject.SetActive(false);

                CurrentGameStage = GameStage.stage01;
                currentPeeperIndex = 0;
                enableHandMovement = false;
                handTransform.gameObject.SetActive(false);

                StartCoroutine(StartPeeperEncounterRoutine());
                break;
            case 2:
                CurrentGameStage = GameStage.stage02;
                currentCamera = cameraTransforms[1];
                currentCameraCamera = currentCamera.GetComponentInChildren<Camera>();
                cameraTransforms[0].gameObject.SetActive(false);
                cameraTransforms[1].gameObject.SetActive(true);
                enableHandMovement = true;
                handTransform.gameObject.SetActive(true);
                break;
            case 3:
                CurrentGameStage = GameStage.stage03;
                break;
        }
    }

    private Vector2 mousePosition;

    //private void Update()
    //{
    //   Debug.Log( Mouse.current.position.ReadValue());
    //    if (!enableHandMovement)
    //        return;

    //    var mousePos = currentCameraCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //    handTransform.position = new Vector3(mousePos.x, 0 ,mousePos.y);
    //}
}
