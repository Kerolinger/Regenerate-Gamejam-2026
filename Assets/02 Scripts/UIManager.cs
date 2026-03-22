using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header ("Tutorial References")]
    [SerializeField] private GameObject m_tutorialContainer;
    [Space]
    [SerializeField] private Tutorial m_tutorial;
    [SerializeField] private TextMeshProUGUI m_tutorialText;
    [SerializeField] private GameObject m_tutorialBack;
    [SerializeField] private GameObject m_tutorialNext;
    [SerializeField] private GameObject m_tutorialStartGame;

    [SerializeField] private Image m_tutorialImage;

    [Header("Dialoguebox References")]

    [SerializeField] private GameObject m_DialogueContainer;
    [Space]
    [SerializeField] private TextMeshProUGUI m_DialoguePeeperName;
    [SerializeField] private TextMeshProUGUI m_DialoguePeeperText;
    [SerializeField] private GameObject m_DialoguePeeperNameContainer;
    [SerializeField] private GameObject m_DialogueNextButton;
    [SerializeField] private GameObject m_DialogueOptionsContainer;

    [Header("Stage Two UI References")]

    [SerializeField] private GameObject m_StageTwoContainer;
    [SerializeField] private TextMeshProUGUI m_stageTwoNotes;

    [Header("Stage three References (help me)")]
    [SerializeField] private GameObject m_StageThreeContainer;

    [Space]
    [Header("Other References")]
    [SerializeField] private GameObject m_Stage02Button;
    [SerializeField] private GameObject m_ENDGAMEMAMA;

    private int currentTutorialslide;
    private int currentTextSnippetIndex;
    private bool hasResponded;
    private bool hasSaidDislike;
    List<string> stage03dialogues;
    List<string> stage03names;
    private int stage03currentTextIndex;


    private PeeperProfile currentPeeper;

    public GameObject StageThreeContainer { get => m_StageThreeContainer; set => m_StageThreeContainer = value; }
    public TextMeshProUGUI StageTwoNotes { get => m_stageTwoNotes; set => m_stageTwoNotes = value; }

    public enum ConversationFlow {introduction, intro_answer, intro_goodbye };

    private const string buttonConfirm = "buttonConfirm";
    private const string buttonBack = "buttonBack";

    private const string linaTalking = "linaTalking";
    private const string tommyTalking = "tommyTalking";
    private const string qrisTalking = "qrisTalking";
    private const string poTalking = "poTalking";
    private const string maschaTalking = "maschaTalking";
    private const string juneTalking = "juneTalking";
    private const string georgyTalking = "georgyTalking";

    private void Start()
    {
        //currentTextSnippets = new List<string>();
        m_DialogueContainer.SetActive(false);
        m_Stage02Button.SetActive(false);
        m_StageTwoContainer.SetActive(false);
    }

    #region Tutorial
    public void PlayTutorial()
    {
        m_tutorialContainer.SetActive(true);
        currentTutorialslide = 0;
        UpdateTutorialPage();
        CameraRotator.instance.ChangeMouseRotation(false);

    }
    public void BTN_TutorialBack()
    {
        currentTutorialslide--;
        UpdateTutorialPage();
        AudioManager.instance.Play(buttonBack);
    }

    private void UpdateTutorialPage()
    {
        m_tutorialText.text = m_tutorial.TutorialSlides[currentTutorialslide].TutorialText;
        m_tutorialImage.sprite = m_tutorial.TutorialSlides[currentTutorialslide].TutorialImage;

        if (currentTutorialslide == 0)
        {
            m_tutorialBack.SetActive(false);
            m_tutorialStartGame.SetActive(false);
        }
        else if (currentTutorialslide == m_tutorial.TutorialSlides.Length - 1)
        {
            m_tutorialNext.SetActive(false);
            m_tutorialStartGame.SetActive(true);
        }
        else
        {
            m_tutorialNext.SetActive(true);
            m_tutorialBack.SetActive(true);
            m_tutorialStartGame.SetActive(false);
        }

    }

    public void BTN_TutorialNext()
    {
        currentTutorialslide++;
        UpdateTutorialPage();

        if (currentTutorialslide == m_tutorial.TutorialSlides.Length - 1)
            m_tutorialNext.SetActive(false);
        else
            m_tutorialNext.SetActive(true);

        AudioManager.instance.Play(buttonConfirm);
    }

    public void BTN_ToggleTutorial()
    {
        m_tutorialContainer.SetActive(!m_tutorialContainer.activeSelf);

        if(!m_tutorialContainer.activeSelf)
            CameraRotator.instance.ChangeMouseRotation(true);
    }

    #endregion

    public void TriggerDialogue(PeeperProfile newPeeper, string newTextSnippet)
    {
        hasResponded = false;
        hasSaidDislike = false;
        currentPeeper = newPeeper;
        m_DialogueContainer.SetActive(true);
        m_DialogueOptionsContainer.SetActive(false);

        m_DialoguePeeperName.text = newPeeper.Name;
        m_DialoguePeeperText.text = newTextSnippet;

        CheckForName(newPeeper.Name);
    }

    private void CheckForName(string peeperName)
    {
        AudioManager.instance.Stop(tommyTalking);
        AudioManager.instance.Stop(maschaTalking);
        AudioManager.instance.Stop(juneTalking);
        AudioManager.instance.Stop(qrisTalking);
        AudioManager.instance.Stop(georgyTalking);
        AudioManager.instance.Stop(poTalking);
        AudioManager.instance.Stop(linaTalking);

        switch (peeperName)
        {
            case "Thommy":
                AudioManager.instance.Play(tommyTalking);
                break;

            case "Mascha":
                AudioManager.instance.Play(maschaTalking);
                break;

            case "June":
                AudioManager.instance.Play(juneTalking);
                break;

            case "Qris":
                AudioManager.instance.Play(qrisTalking);
                break;

            case "Caro":
                AudioManager.instance.Play(georgyTalking);
                break;

            case "Po and Lina":
                AudioManager.instance.Play(poTalking);
                AudioManager.instance.Play(linaTalking);
                break;
        }
    }

    public void BTN_QuestionSkill()
    {
        hasResponded = true;
        m_DialogueOptionsContainer.SetActive(false);
        m_DialoguePeeperNameContainer.SetActive(true);
        m_DialogueNextButton.SetActive(true);

        m_DialoguePeeperText.text = currentPeeper.St01_skillOfferText;
    }

    public void BTN_QuestionDislike()
    {
        hasResponded = true;
        m_DialogueOptionsContainer.SetActive(false);
        m_DialoguePeeperNameContainer.SetActive(true);
        m_DialogueNextButton.SetActive(true);

        m_DialoguePeeperText.text = currentPeeper.St01_ingredientDislikeText;
    }

    public void BTN_QuestionProblem()
    {
        hasResponded = true;
       
        m_DialogueOptionsContainer.SetActive(false);
        m_DialoguePeeperNameContainer.SetActive(true);
        m_DialogueNextButton.SetActive(true);

        m_DialoguePeeperText.text = currentPeeper.St01_skillNeedText;
    }

    public void BTN_TriggerNextDialogueText()
    {
        switch (gameManager.CurrentGameStage)
        {

            case GameManager.GameStage.stage01:

                if(!hasSaidDislike)
                {
                    hasSaidDislike = true;
                    m_DialogueNextButton.SetActive(true);
                    m_DialoguePeeperText.text = currentPeeper.St01_ingredientDislikeText;
                }
                else
                {

                    if (!hasResponded)
                    {
                        m_DialoguePeeperText.text = "(pick what to ask)";
                        m_DialogueOptionsContainer.SetActive(true);
                        m_DialoguePeeperNameContainer.SetActive(false);
                        m_DialogueNextButton.SetActive(false);
                    }
                    else
                    {
                        m_DialogueContainer.SetActive(false);
                        gameManager.RemovePeeper();
                    }
                }

                break;
            case GameManager.GameStage.stage03:

                stage03currentTextIndex++;

                if (stage03currentTextIndex >= stage03dialogues.Count)
                {
                    m_ENDGAMEMAMA.SetActive(true);
                    m_DialogueOptionsContainer.SetActive(false);
                    m_DialogueContainer.SetActive(false);
                    CameraRotator.instance.ChangeMouseRotation(false);

                    AudioManager.instance.Stop(tommyTalking);
                    AudioManager.instance.Stop(maschaTalking);
                    AudioManager.instance.Stop(juneTalking);
                    AudioManager.instance.Stop(qrisTalking);
                    AudioManager.instance.Stop(georgyTalking);
                    AudioManager.instance.Stop(poTalking);
                    AudioManager.instance.Stop(linaTalking);
                }
                else
                {
                    m_DialoguePeeperName.text = stage03names[stage03currentTextIndex];
                    m_DialoguePeeperText.text = stage03dialogues[stage03currentTextIndex];
                    CheckForName(stage03names[stage03currentTextIndex]);
                }

                break;
        }

    }

    public void DisplayStage02Button()
    {
        m_Stage02Button.SetActive(true);
    }

    public void BTN_StartStage02()
    {
        m_Stage02Button.SetActive(false);
        m_StageTwoContainer.SetActive(true);
        gameManager.SwitchStage(2);
    }

    public void StartEndDialogue(List<string> stage03SetDialogues, List<string> stage03SetDialogueNames)
    {
        stage03dialogues = new List<string>();
        stage03names = new List<string>();

        stage03dialogues = stage03SetDialogues;
        stage03names = stage03SetDialogueNames;

        stage03currentTextIndex = 0;

        m_DialogueContainer.SetActive(true);
        m_DialogueOptionsContainer.SetActive(false);

        m_DialoguePeeperName.text = stage03names[stage03currentTextIndex];
        m_DialoguePeeperText.text = stage03dialogues[stage03currentTextIndex];
    }

    public void BTN_Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void BTN_MainMenu()
    {
        SceneManager.LoadScene(0);
    }




}
