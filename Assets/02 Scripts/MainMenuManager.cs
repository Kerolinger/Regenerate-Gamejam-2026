using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_credits;

    public void BTN_StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BTN_ToggleCredits()
    {
        m_credits.SetActive(!m_credits.activeSelf);
    }

    public void BTN_QuitGame()
    {
        Application.Quit();
    }
}
