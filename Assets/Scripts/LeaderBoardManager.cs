using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LeaderBoardManager : Singleton<LeaderBoardManager>
{
    [Header("Profile")]
    [SerializeField]
    private Transform playerProfileContent;
    [SerializeField]
    private GameObject playerProfileItemPrefab;
    private List<PlayerProfileItem> playerProfiles;

    public void SetupBoard()
    {
        // Prepare new player lists
        playerProfiles = new();

        // Create player profile list
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            PlayerProfileItem newPlayerProfile = Instantiate(playerProfileItemPrefab, playerProfileContent).GetComponent<PlayerProfileItem>();
            newPlayerProfile.Setup(players[i]);
            playerProfiles.Add(newPlayerProfile);
        }
    }

    public void UpdateScore(int score, string nickName)
    {
        for (int i = 0; i < playerProfiles.Count; i++)
        {
            if (playerProfiles[i].playerProfileName == nickName)
            {
                playerProfiles[i].SetScoreText(score);
            }
        }
    }
}
