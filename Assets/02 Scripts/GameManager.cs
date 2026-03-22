using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int thisisrealthisisme;

    public Transform currentCamera;

    [Header("References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PeeperProfile[] peepers;
    [SerializeField] private IngredientInfo[] ingredients;
    [SerializeField] private GameObject peeperPrefab;
    [SerializeField] private Transform[] cameraTransforms;
    [SerializeField] private Transform[] stageThreeTransforms;
    [SerializeField] private Transform handTransform;
    [SerializeField] private GameObject stageThreeContainer;

    [SerializeField] private Transform[] peeperPos;

    [Header("Ingame Tools")]
    [SerializeField] private bool disableTutorial;


    private GameStage currentGameStage;
    private int currentPeeperIndex;
    private GameObject currentPeeper;
    private Camera currentCameraCamera;

    public StageTwoManager stageTwomanager;

    public GameStage CurrentGameStage { get => currentGameStage; set => currentGameStage = value; }
    public IngredientInfo[] Ingredients { get => ingredients; set => ingredients = value; }

    public enum GameStage { stage01, stage02, stage03 };

    private bool enableHandMovement;

    private GameObject currentIngredient;

    private List<IngredientInfo> CurrentSoup;

    private const string ambienceTutorial = "ambienceTutorial";
    private const string ambienceStageOne = "ambienceStageOne";
    private const string ambienceStageTwo = "ambienceStageTwo";
    private const string ambienceStageThree = "ambienceStageThree";
    private const string peeperPopup = "peeperPopup";
    private const string peeperLeave = "peeperLeave";
    private const string doorBell = "doorBell";
    private const string doorOpen = "doorOpen";

    private void Start()
    {
        if (!disableTutorial)
        {
            uiManager.PlayTutorial();
            AudioManager.instance.Play(ambienceTutorial);
            AudioManager.instance.Play(ambienceStageOne);
        }
        else
        {
            SwitchStage(1);
        }


        CurrentSoup = new List<IngredientInfo>();
    }

    private IEnumerator StartPeeperEncounterRoutine()
    {
        yield return new WaitForSeconds(Random.Range(1, 3f));
        AudioManager.instance.Play(doorOpen);
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.Play(doorBell);
        yield return new WaitForSeconds(Random.Range(0.7f, 1.2f));

        StartPeeperEncounter();
    }

    public void SetIngredient(GameObject gameobject)
    {
        currentIngredient = gameobject;
    }

    private void StartPeeperEncounter()
    {
        AudioManager.instance.Play(peeperPopup);

        currentPeeper = Instantiate(peeperPrefab);
        //currentPeeper.transform.position = peeperPos[currentPeeperIndex].position;
        currentPeeper.transform.rotation = peeperPos[currentPeeperIndex].rotation;

        Vector3 currPeepPos = new Vector3(peeperPos[currentPeeperIndex].position.x, peeperPos[currentPeeperIndex].position.y-5, peeperPos[currentPeeperIndex].position.z);
        currentPeeper.transform.position = currPeepPos;
        currentPeeper.transform.DOLocalMove(peeperPos[currentPeeperIndex].position, 0.5f).SetEase(Ease.OutSine);
    
  
        currentPeeper.GetComponent<Peeper>().SetPeeper(peepers[currentPeeperIndex], GetIngredientMaterial(peepers[currentPeeperIndex].Ingredient), this);

      

        uiManager.TriggerDialogue(peepers[currentPeeperIndex], peepers[currentPeeperIndex].St01_ingredientText);

        currentPeeperIndex++;
    }

    public void RemovePeeper()
    {
        AudioManager.instance.Play(peeperLeave);
        Vector3 newPeepPos = new Vector3(currentPeeper.transform.position.x, currentPeeper.transform.position.y - 5, currentPeeper.transform.position.z);

        currentPeeper.transform.DOLocalMove(newPeepPos, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            Destroy(currentPeeper);
            currentPeeper = null;

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
        switch (i)
        {
            case PeeperProfile.Ingredients.carrot:
                return Ingredients[0].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.aubergine:
                return Ingredients[1].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.potato:
                return Ingredients[2].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.beetroot:
                return Ingredients[3].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.chilli:
                return Ingredients[4].DisplayedIngredientMaterial;

            case PeeperProfile.Ingredients.chickpeas:
                return Ingredients[5].DisplayedIngredientMaterial;
        }

        return null;
    }

    public void AddIngredientToPot(int ingredientIndex)
    {
        CurrentSoup.Add(ingredients[ingredientIndex]);

        if (CurrentSoup.Count > 1)
        {
            stageTwomanager.cookSoupButton.SetActive(true);
        }
    }

    public void ResetSoup()
    {
        CurrentSoup.Clear();
        stageTwomanager.cookSoupButton.SetActive(false);
    }

    public void SwitchStage(int newStageindex)
    {

        switch (newStageindex)
        {
            case 1:

                if (disableTutorial)
                    AudioManager.instance.Play(ambienceStageOne);

                CurrentGameStage = GameStage.stage01;
                AudioManager.instance.FadeOut(ambienceTutorial, 0f);

                currentCamera = cameraTransforms[0];
                currentCameraCamera = currentCamera.GetComponentInChildren<Camera>();
                cameraTransforms[0].gameObject.SetActive(true);
                cameraTransforms[1].gameObject.SetActive(false);

                CurrentGameStage = GameStage.stage01;
                currentPeeperIndex = 0;
                enableHandMovement = false;
                handTransform.gameObject.SetActive(false);

                StartCoroutine(StartPeeperEncounterRoutine());

                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

                RenderSettings.ambientSkyColor = new Color(0.915f, 0.898f, 0.894f, 1.000f);
                RenderSettings.ambientEquatorColor = new Color(0.566f, 0.159f, 0.120f, 1.000f);
                RenderSettings.ambientGroundColor = new Color(0.858f, 0.400f, 0.239f, 1.000f);

                DynamicGI.UpdateEnvironment();

                break;
            case 2:
                CurrentGameStage = GameStage.stage02;
                AudioManager.instance.FadeOut(ambienceStageOne, 0f);
                AudioManager.instance.Play(ambienceStageTwo);
                currentCamera = cameraTransforms[1];
                currentCameraCamera = currentCamera.GetComponentInChildren<Camera>();
                cameraTransforms[0].gameObject.SetActive(false);
                cameraTransforms[1].gameObject.SetActive(true);
                enableHandMovement = true;
                handTransform.gameObject.SetActive(true);

                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

                RenderSettings.ambientSkyColor = new Color(0.514f, 0.656f, 1.000f, 1.000f);
                RenderSettings.ambientEquatorColor = new Color(0.429f, 0.696f, 0.849f, 1.000f);
                RenderSettings.ambientGroundColor = new Color(0.118f, 0.158f, 0.189f, 1.000f);

                DynamicGI.UpdateEnvironment();

                break;
            case 3:

                AudioManager.instance.FadeOut(ambienceStageTwo, 0f);
                AudioManager.instance.Play(ambienceStageThree);
                StartCoroutine(SetStagethreetiming());
                break;
        }
    }

    private IEnumerator SetStagethreetiming()
    {
        uiManager.StageThreeContainer.SetActive(true);
        stageThreeContainer.SetActive(true);
        yield return new WaitForSeconds(1f);

        PlacePeople();
        CurrentGameStage = GameStage.stage03;
        currentCamera = cameraTransforms[0];
        currentCameraCamera = currentCamera.GetComponentInChildren<Camera>();
        cameraTransforms[0].gameObject.SetActive(true);
        cameraTransforms[1].gameObject.SetActive(false);

        enableHandMovement = false;
        handTransform.gameObject.SetActive(false);

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

        RenderSettings.ambientSkyColor = new Color(0.807f, 0.479f, 0.847f, 1.0f);
        RenderSettings.ambientEquatorColor = new Color(0.301f, 0.087f, 0.209f, 1.0f);
        RenderSettings.ambientGroundColor = new Color(0.708f, 0.546f, 0.871f, 1.0f);

        DynamicGI.UpdateEnvironment();
    }
    private Vector2 mousePosition;


    private void PlacePeople()
    {
        //potential gästeliste
        List<PeeperProfile> potentialguestlist = new List<PeeperProfile>();

        for (int p = 0; p < peepers.Length; p++)
        {
            potentialguestlist.Add(peepers[p]);

            foreach (IngredientInfo i in CurrentSoup)
            {

                Debug.Log("Checking if person" + peepers[p].Name + "is coming:" + i.DisplayedIngredient + "/ dislikes" + peepers[p].IngredientDislikes);

                if (i.DisplayedIngredient == peepers[p].IngredientDislikes)
                {
                    potentialguestlist.Remove(peepers[p]);
                    stageThreeTransforms[p].gameObject.SetActive(false);
                }
            }
        }

        CheckForMatches(potentialguestlist);
    }

    private void CheckForMatches(List<PeeperProfile> guestlist)
    {
        List<string> queuedConversation = new List<string>();
        List<string> queuedConversationNames = new List<string>();

        foreach (PeeperProfile peeperInNeed in guestlist)
        {
            bool skillMatch = false;
            foreach (PeeperProfile peeperToHelp in guestlist)
            {
                Debug.Log("CHECK IF PEOPLES NEEDS ARE MET:" + peeperInNeed.SkillNeeded + "/" + peeperToHelp.SkillOffer);
                if (peeperInNeed.SkillNeeded == peeperToHelp.SkillOffer)
                {
                    queuedConversation.Add(peeperInNeed.St03_skillNeedText);
                    queuedConversationNames.Add(peeperInNeed.Name);
                    queuedConversation.Add(peeperToHelp.St03_skillOfferText);
                    queuedConversationNames.Add(peeperToHelp.Name);
                    skillMatch = true;
                }
            }

            if (skillMatch == false)
            {
                queuedConversation.Add(peeperInNeed.St03_soupResultText);
                queuedConversationNames.Add(peeperInNeed.Name);
            }
        }

        uiManager.StartEndDialogue(queuedConversation, queuedConversationNames);
    }

    private void Update()
    {
        if (!enableHandMovement)
            return;

        var mousePos = Mouse.current.position.ReadValue();
        var camPos = currentCameraCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 2));
        handTransform.position = new Vector3(camPos.x, 0, camPos.z);
    }
}
