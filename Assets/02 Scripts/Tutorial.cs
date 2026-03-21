using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "Scriptable Objects/Tutorial")]
public class Tutorial : ScriptableObject
{
    [SerializeField] private TutorialSlide[] tutorialSlides;

    public TutorialSlide[] TutorialSlides { get => tutorialSlides; set => tutorialSlides = value; }
}
