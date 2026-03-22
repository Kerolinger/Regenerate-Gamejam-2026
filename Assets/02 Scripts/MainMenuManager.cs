using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_credits;

    private void Start()
    {
        AudioManager.instance.Play("wholesomeTheme");
        AudioManager.instance.Play("ambienceStageThree");
    }
    public void BTN_StartGame()
    {
        SceneManager.LoadScene(1);
        AudioManager.instance.Play("buttonConfirm");
    }

    public void BTN_ToggleCredits()
    {
        m_credits.SetActive(!m_credits.activeSelf);

        if (m_credits.activeSelf)
            AudioManager.instance.Play("buttonConfirm");
        else
            AudioManager.instance.Play("buttonBack");
    }

    //public void BTN_QuitGame()
    //{
    //    Application.Quit();
    //}
}
