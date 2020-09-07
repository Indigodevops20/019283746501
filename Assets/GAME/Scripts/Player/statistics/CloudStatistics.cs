using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class CloudStatistics : MonoBehaviour
{
    public statistics var;
    // Build the request object and access the API
    public void StartCloudUpdatePlayerStats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStatistics", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new
            {
                Level = var.PlayerLevel,
                Kills = var.PlayerKills,
                Wins = var.PlayerWins
            }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUPDATEstats, OnErrorShared);
    }

    private static void OnCloudUPDATEstats(ExecuteCloudScriptResult result)
    {
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
