using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] UIManager uiManager;

    [Header("Ingame Tools")]
    [SerializeField] private bool disableTutorial;

    private void Start()
    {
        if (!disableTutorial)
            uiManager.PlayTutorial();
    }
}
