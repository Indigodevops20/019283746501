using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class statistics : MonoBehaviour
{
    public static statistics PLAYERstatistics;

    private void OnEnable()
    {
        if (statistics.PLAYERstatistics == null)
        {
            statistics.PLAYERstatistics = this;
        }
        else
        {
            if (statistics.PLAYERstatistics != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public int PlayerLevel;
    public int PlayerKills;
    public int PlayerWins;

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
        Debug.Log("cargando estadisticas...");
        foreach (var eachStat in result.Statistics)
        {
            // Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
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
