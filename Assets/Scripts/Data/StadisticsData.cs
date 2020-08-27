using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class StadisticsData : MonoBehaviour
{

    #region OBTENIENDO INFORMACION DEL USUARIO...

    #region VARIABLES DE USUARIO

    public int PlayerLevel;

    #endregion

    #region ACTUALIZANDO INFORMACION DE USUARIO
    public void SetUserData()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {

            Statistics = new List<StatisticUpdate> {

            new StatisticUpdate { StatisticName = "PLAYER LEVEL", Value = PlayerLevel },

        }
        },
            result => { Debug.Log("User statistics updated"); },
            error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    #endregion

    #region UTILIZANDO INFORMACION DE USUARIO
    public void GetUserData()

    {
        PlayFabClientAPI.GetPlayerStatistics(
        new GetPlayerStatisticsRequest(),
        OnGetStatistics,
        error => Debug.LogError(error.GenerateErrorReport()));
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);

            switch (eachStat.StatisticName)
            {
                case "PLAYER LEVEL":
                    PlayerLevel = eachStat.Value;
                    Debug.Log("NIVEL OBTENIDO");
                    break;
            }
        }
    }

    #endregion

    #endregion

}
