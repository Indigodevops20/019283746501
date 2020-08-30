using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.Json;
using UnityEngine;

public class StatisticsCLOUD : MonoBehaviour
{
    public Statistics var;
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
    // OnCloudHelloWorld defined in the next code block
    private static void OnCloudUPDATEstats(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(JsonWrapper.SerializeObject(result.FunctionResult));
        PlayFab.Json.JsonObject jsonResult = (PlayFab.Json.JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}