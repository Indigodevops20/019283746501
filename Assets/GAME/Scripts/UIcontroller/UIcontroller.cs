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

    public GameObject background;
    public GameObject UI;
    public GameObject CAMPOS;
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
        //LOADING
        background.SetActive(true);
        UI.SetActive(true);
        CAMPOS.SetActive(true);
        
    }

    public void Back()
    {
        FB.LogOut();
        //LOADING
        welcomePanel.SetActive(false);
        background.SetActive(false);
        UI.SetActive(false);
        CAMPOS.SetActive(false);
        LoginPanel.SetActive(true);
    }
}

