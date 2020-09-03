using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using System;
using UnityEngine;
using UnityEngine.UI;

public class id : MonoBehaviour
{
    private string idPlayer;
    public Text ID;

    // Build the request object and access the API
    public void GetId()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "GetUserID", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { inputValue = "YOUR NAME" }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnGetId, OnErrorID);
    }
    // OnCloudHelloWorld defined in the next code block
    public void OnGetId(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        idPlayer = Convert.ToString((string)messageValue);
        ID.text = idPlayer;
    }

    private static void OnErrorID(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
