using UnityEngine;

[System.Serializable]
public class TutorialSlide 
{
    [SerializeField] private string tutorialText;
    [SerializeField] private Sprite tutorialImage;

    public string TutorialText { get => tutorialText; set => tutorialText = value; }
    public Sprite TutorialImage { get => tutorialImage; set => tutorialImage = value; }
}
