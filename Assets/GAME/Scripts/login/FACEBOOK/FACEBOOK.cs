using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices.ComTypes;
#region using facebook with unity
#if FACEBOOK
using Facebook.Unity;
#endif
#endregion

public class FACEBOOK : MonoBehaviour
{
    public Main mn;
    public string standar = "WILDONES";

    #region SINGLETON
    public static FACEBOOK LFB;

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


    }
    #endregion

    #region VARIABLES
    public UIcontroller UI;
    public Text usernameTEXT;
    public Text usernameClient;

    #endregion

    #region INICIALIZANDO FACEBOOK...
    void Awake()
    {

      
#if FACEBOOK

        if (!FB.IsInitialized)
        {

            FB.Init(OnFacebookInitialized, OnHideUnity);
   
        }
        else
        {
            FB.ActivateApp();
        }
#endif
    }
    #endregion

    #region FACEBOOK LOGIN
    private void OnFacebookInitialized()
    {

        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");
            usernameClient.text = PlayerPrefs.GetString("USERNAME");


        }
        else
        {
            Debug.Log("Facebook tuvo problemas al iniciar el SDK");
        }


        if (FB.IsLoggedIn)
        {
            UI.success();
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");
            usernameClient.text = PlayerPrefs.GetString("USERNAME");
            mn.ObtenerStats();
        }
        else
        {
            UI.error();
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
            BienvenidaAlJugadorEnFacebook(FB.IsLoggedIn);
           // mn.ObtenerDatos();
            UI.success();

        }
        else
        {
            UI.error();
        }
    }

    private void OnPlayFabLoginWithFacebookSuccess(PlayFab.ClientModels.LoginResult result)
    {
        mn.ObtenerStats();
        standar = PlayerPrefs.GetString("USERNAME");
        Debug.Log(standar);
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = standar }, OnDisplayName, OnPlayFabLoginWithFacebookFailure);

    }

   public void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + "is your name");
    }

   public void OnPlayFabLoginWithFacebookFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    void BienvenidaAlJugadorEnFacebook(bool IsLoggedIn)
    {

        FB.API("/me?fields=first_name", HttpMethod.GET, GetFirstName);

    }

    public void GetFirstName(IResult result)
    {

        if (result.Error == null)
        {

            PlayerPrefs.SetString("USERNAME", Convert.ToString(result.ResultDictionary["first_name"]));
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");
            usernameClient.text = PlayerPrefs.GetString("USERNAME");


        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    
    #endregion

    #region cerrar sesion // ir al cliente
    public void GoToClient()
    {
        UI.GO();
    }

    public void CloseSession()
    {
        
        UI.Back();
        PlayerPrefs.DeleteKey("USERNAME");
        Destroy(mn);

    }
    #endregion

}