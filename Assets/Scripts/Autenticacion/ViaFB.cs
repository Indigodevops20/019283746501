using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
#region using facebook with unity
#if FACEBOOK
using Facebook.Unity;
#endif
#endregion

public class ViaFB : MonoBehaviour
{
    #region SINGLETON
    public static ViaFB LFB;

    private void OnEnable()
    {
        if (LFB == null)
        {
            LFB = this;
        }
        else
        {
            if (LFB != this)
            {
                Destroy(this.gameObject);
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region VARIABLES
    public GameObject LoginPanel;
    public GameObject welcomePanel;
    public GameObject loadingPanel;
    public Text usernameTEXT;
    public GameObject scriptFB;
    public StadisticsData sData;
    //Lo de abajo para instanciar la clase en donde se encuentran los datos que setearas u obtendras de la base de datos(playfab)

    #endregion

    #region INICIALIZANDO FACEBOOK...
    void Awake()
    {

        DontDestroyOnLoad(scriptFB);
#if FACEBOOK
        if (!FB.IsInitialized)
        {

            FB.Init(OnFacebookInitialized, OnHideUnity);
            Debug.Log("Iniciando el SDK de Facebook...");
   
        }
        else
        {

            FB.ActivateApp();
         
        }
#endif
    }
    #endregion

    #region START
    void Start()
    {
        sData = GetComponent<StadisticsData>();
    }
    #endregion

    #region FACEBOOK LOGIN
    private void OnFacebookInitialized()
    {

        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");
        }
        else
        {
            Debug.Log("Facebook tuvo problemas al iniciar el SDK");
        }


        if (FB.IsLoggedIn)
        {
            LoginPanel.SetActive(false);
            welcomePanel.SetActive(true);
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");






        }
        else
        {
            LoginPanel.SetActive(true);
            welcomePanel.SetActive(false);
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void BotonLoginFacebook()
    {

        var permissions = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(permissions, OnFacebookLoggedIn);


    }

    private void OnFacebookLoggedIn(ILoginResult result)
    {

        if (result == null || string.IsNullOrEmpty(result.Error))
        {

            var LoginFacebookRequest = new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString };
            PlayFabClientAPI.LoginWithFacebook(LoginFacebookRequest, OnPlayFabLoginWithFacebookSuccess, OnPlayFabLoginWithFacebookFailure);
            LoginPanel.SetActive(false);
            welcomePanel.SetActive(true);
            BienvenidaAlJugadorEnFacebook(FB.IsLoggedIn);

        }
        else
        {

            Debug.LogError("El inicio de sesion con Facebook fracaso por el siguiente error: " + result.Error + "\n" + result.RawResult);
            LoginPanel.SetActive(true);
            welcomePanel.SetActive(false);

        }
    }

    private void OnPlayFabLoginWithFacebookSuccess(PlayFab.ClientModels.LoginResult result)
    {
        Debug.Log("Facebook PlayFab inicio sesion correctamente. Ticket de sesion: " + result.SessionTicket);
        sData.GetUserData();
    }

    private void OnPlayFabLoginWithFacebookFailure(PlayFabError error)
    {

        Debug.LogError("Facebook PlayFab no pudo iniciar sesion debido al siguiente error");
        Debug.LogError(error.GenerateErrorReport());

    }

    void BienvenidaAlJugadorEnFacebook(bool IsLoggedIn)
    {

        FB.API("/me?fields=first_name", HttpMethod.GET, obtenerUsernameFacebook);

    }

    private void obtenerUsernameFacebook(IResult result)
    {

        if (result.Error == null)
        {

            Debug.Log("Bienvenido" + result.ResultDictionary["first_name"]);

            PlayerPrefs.SetString("USERNAME", Convert.ToString(result.ResultDictionary["first_name"]));
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");

            Debug.Log("KEY ALMACENADA:");
            Debug.Log(PlayerPrefs.GetString("USERNAME"));






        }
        else
        {

            Debug.Log("Acaba de ocurrir un error:");
            Debug.Log(result.Error);

        }
    }
    #endregion

    #region cerrar sesion // ir al cliente
    public void GoToClient()
    {

        welcomePanel.SetActive(false);
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("cliente");
        sData.GetUserData();

    }

    public void CloseSession()
    {
        PlayerPrefs.DeleteKey("USERNAME");
        LoginPanel.SetActive(true);
        welcomePanel.SetActive(false);
        FB.LogOut();

    }
    #endregion

}