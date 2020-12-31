using PlayFab;
using PlayFab.ClientModels;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CustomUserName : MonoBehaviour
{
    public Text ID;

    public UIcontroller ui;
    public Text userInput;
    private string GetUsername;

    
    public void GetPlayerProfile()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true,
                ShowCreated = true,
                ShowLastLogin = true,
            }
        },
        result => { 
            GetUsername = result.PlayerProfile.DisplayName;
            ID.text = result.PlayerProfile.PlayerId;

            CheckExist();
        },
        
        error => Debug.LogError(error.GenerateErrorReport()));;
    }


    public void CheckExist()
    {
        if (GetUsername == null)
        {
            ui.LoginSucces();
            UpdateDisplayName();
        }
        else
        {
            
            ui.CustomPlayerSuccess();
        }
    }  

    public void UpdateDisplayName()
     {
         PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
         {
             DisplayName = userInput.text
         }, result =>
         {
             Debug.Log("The player's display name is: " + result.DisplayName);
             ui.CustomPlayerSuccess();
         }, error => Debug.Log("No tiene nombre de usuario!!")) ;
     }
}
    

