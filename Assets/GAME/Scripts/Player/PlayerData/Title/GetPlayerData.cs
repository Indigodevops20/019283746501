using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerData : MonoBehaviour
{
    public Text username;
   /* public Text DateLogin;
    public Text LastLogin;*/
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
            username.text = result.PlayerProfile.DisplayName;
            /*
            DateLogin.text = Convert.ToString(result.PlayerProfile.Created);
            LastLogin.text = Convert.ToString(result.PlayerProfile.LastLogin);
            */

        },

        error => Debug.LogError(error.GenerateErrorReport())); ;
    }

}
