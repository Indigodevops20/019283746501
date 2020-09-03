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
        var requestLeaderBoard1 = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerLevel" , MaxResultsCount = 10 };
        var requestLeaderBoard2 = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerKills"};
        var requestLeaderBoard3 = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "PlayerWins"};
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard1, OnGetLeaderBoard, OnErrorLeaderboard );
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard2, OnGetLeaderBoard, OnErrorLeaderboard);
        PlayFabClientAPI.GetLeaderboard(requestLeaderBoard3, OnGetLeaderBoard, OnErrorLeaderboard);

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
            LL.PlayerKills.text = player.StatValue.ToString();
            LL.PlayerWins.text = player.StatValue.ToString();
            
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
