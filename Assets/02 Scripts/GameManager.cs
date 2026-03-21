using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Peeper[] peepers;
    [SerializeField] private GameObject peeperPrefab;

    [Header("Ingame Tools")]
    [SerializeField] private bool disableTutorial;


    private GameStage currentGameStage;
    private int currentPeeperIndex;
    private GameObject currentPeeper;
    public enum GameStage { stage01, stage02, stage03};


    private void Start()
    {
        if (!disableTutorial)
            uiManager.PlayTutorial();

        currentGameStage = GameStage.stage01;
        currentPeeperIndex = 0;

        StartCoroutine(StartPeeperEncounterRoutine());
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
        currentPeeperIndex++;
    }
}
