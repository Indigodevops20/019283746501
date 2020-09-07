// Import statements introduce all the necessary classes for this example.
using Facebook.Unity;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using LoginResult = PlayFab.ClientModels.LoginResult;

public class PlayfabFacebookAuth : MonoBehaviour
{
    // holds the latest message to be displayed on the screen

    public UIcontroller ui;
    public CreateUser user;

    private void Start()
    {
      FB.Init(OnFacebookInitialized);  
    }
   

    public void OnFacebookInitialized()
    {
   

        // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.
        if (FB.IsLoggedIn) {

            ui.LoggedIn();
        
        }
        else
        {
        FB.LogInWithReadPermissions(null, OnFacebookLoggedIn);
        }

        // We invoke basic login procedure and pass in the callback to process the result
        
    }

    private void OnFacebookLoggedIn(ILoginResult result)
    {
        // If result has no errors, it means we have authenticated in Facebook successfully
        if (result == null || string.IsNullOrEmpty(result.Error))
        {
            PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString },
                OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
        }
        else
        {
            // If Facebook authentication failed, we stop the cycle with the message
            Debug.Log("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult);
        }
    }

    // When processing both results, we just set the message, explaining what's going on.
    public void OnPlayfabFacebookAuthComplete(LoginResult result)
    {
        Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
        user.GetPlayerProfile();
       
    }

    private void OnPlayfabFacebookAuthFailed(PlayFabError error)
    {
        Debug.Log("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport());
        ui.LoginError();
    }


    public void FBlogout()
    {
        ui.LogoutFB();
    }
}