using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardPANEL;
    public GameObject listingPrefab;
    public Transform listingContainer;
    public void GetLeaderBoard()
    {
        var requestLeaderBoard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerLevel", MaxResultsCount = 10 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard, OnGetLeaderBoard, OnErrorLeaderboard );

    }

    void OnGetLeaderBoard(GetLeaderboardResult result)
    {
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            leaderboardPANEL.SetActive(true);
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            Clasification LL = tempListing.GetComponent<Clasification>();
            LL.PlayerName.text = player.DisplayName;
            LL.PlayerLevel.text = player.StatValue.ToString();
            Debug.Log(player.DisplayName + ": " + player.StatValue);
        }
    } 

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void CloseLeaderBoard()
    {
        leaderboardPANEL.SetActive(false);
        for (int i = listingContainer.childCount -1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
    }
}
