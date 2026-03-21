using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    [SerializeField] private TextMeshProUGUI m_DialoguePeeperName;
    [SerializeField] private TextMeshProUGUI m_DialoguePeeperText;
    [SerializeField] private GameObject m_DialogueOptionsContainer;

    private int currentTutorialslide;

    private void Start()
    {
    }

    public void PlayTutorial()
    {
        m_tutorialContainer.SetActive(true);
        currentTutorialslide = 0;
        UpdateTutorialPage();
    }
    public void BTN_TutorialBack()
    {
        currentTutorialslide--;
        UpdateTutorialPage();
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
    }

    public void BTN_ToggleTutorial()
    {
        m_tutorialContainer.SetActive(!m_tutorialContainer.activeSelf);
    }
}
