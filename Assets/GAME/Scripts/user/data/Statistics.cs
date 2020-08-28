using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    
    public static Statistics PLAYERstatistics;

    private void OnEnable()
    {
        if (Statistics.PLAYERstatistics == null)
        {
            Statistics.PLAYERstatistics = this;
        }
        else
        {
            if (Statistics.PLAYERstatistics != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public int PlayerLevel;
    public int PlayerKills;
    public int PlayerWins;

   /* public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerLevel", Value = PlayerLevel },
        new StatisticUpdate { StatisticName = "PlayerKills", Value = PlayerKills },
        new StatisticUpdate { StatisticName = "PlayerWins", Value = PlayerWins },
    }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }
    */
    public void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "PlayerLevel":
                    PlayerLevel = eachStat.Value;
                    break;

                case "PlayerKills":
                    PlayerKills = eachStat.Value;
                    break;

                case "PlayerWins":
                    PlayerWins = eachStat.Value;
                    break;
            }
        }
    }
}
