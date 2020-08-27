using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
#region using facebook with unity
#if FACEBOOK
using Facebook.Unity;
#endif
#endregion

public class FACEBOOK : MonoBehaviour
{
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
        }
        else
        {
            Debug.Log("Facebook tuvo problemas al iniciar el SDK");
        }


        if (FB.IsLoggedIn)
        {
            UI.success();
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");
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
            UI.success();

        }
        else
        {
            UI.error();
        }
    }

    private void OnPlayFabLoginWithFacebookSuccess(PlayFab.ClientModels.LoginResult result)
    {
      //hola     
    }

    private void OnPlayFabLoginWithFacebookFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    void BienvenidaAlJugadorEnFacebook(bool IsLoggedIn)
    {

        FB.API("/me?fields=first_name", HttpMethod.GET, GetFirstName);

    }

    private void GetFirstName(IResult result)
    {

        if (result.Error == null)
        {

            PlayerPrefs.SetString("USERNAME", Convert.ToString(result.ResultDictionary["first_name"]));
            usernameTEXT.text = PlayerPrefs.GetString("USERNAME");


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
        PlayerPrefs.DeleteKey("USERNAME");
        UI.Back();

    }
    #endregion

}