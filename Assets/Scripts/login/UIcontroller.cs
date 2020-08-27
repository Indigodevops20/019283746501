using UnityEngine;
using UnityEngine.SceneManagement;
#region using facebook with unity
#if FACEBOOK
using Facebook.Unity;
#endif
#endregion


public class UIcontroller : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject welcomePanel;
    public GameObject loadingPanel;

    public void success()
    {
        LoginPanel.SetActive(false);
        welcomePanel.SetActive(true);
    }

    public void error()
    {
        LoginPanel.SetActive(true);
        welcomePanel.SetActive(false);
    }

    public void GO()
    {
        welcomePanel.SetActive(false);
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("cliente");
    }

    public void Back()
    {
        LoginPanel.SetActive(true);
        welcomePanel.SetActive(false);
        FB.LogOut();
    }
}

