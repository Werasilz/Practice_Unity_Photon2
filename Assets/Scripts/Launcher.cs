using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("GUI")]
    [SerializeField]
    private TMP_InputField input_RoomName;
    [SerializeField]
    private TMP_Text text_RoomName;
    [SerializeField]
    private GameObject button_StartGame;

    [Header("Room")]
    [SerializeField]
    private Transform playerListContent;
    [SerializeField]
    private GameObject playerListItemPrefab;
    private List<PlayerListItem> playerLists;

    #region Lobby
    void Start()
    {
        print("[Photon] Connecting to Master");
        MenuManager.Instance.OpenMenu(MenuName.LoadingMenu);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("[Photon] Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        print("[Photon] Joined Lobby");
        MenuManager.Instance.OpenMenu(MenuName.TitleMenu);
        PhotonNetwork.NickName = "Player#" + Random.Range(0, 1000).ToString("0000");
    }
    #endregion

    #region Room
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(input_RoomName.text))
        {
            print("[Photon] Can't create room, room name is empty");
            return;
        }

        print("[Photon] Creating Room: " + input_RoomName.text);
        MenuManager.Instance.OpenMenu(MenuName.LoadingMenu);

        RoomOptions roomOptions = new()
        {
            IsVisible = true,
            MaxPlayers = 4
        };

        PhotonNetwork.CreateRoom(input_RoomName.text, roomOptions, TypedLobby.Default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("[Photon] Room Creation Failed: " + message);
        MenuManager.Instance.OpenMenu(MenuName.RoomFailedMenu);
    }

    public void FindRoom()
    {
        print("[Photon] Joining random room");
        PhotonNetwork.JoinRandomRoom();
        MenuManager.Instance.OpenMenu(MenuName.LoadingMenu);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("[Photon] Failed to join a random room");
        MenuManager.Instance.OpenMenu(MenuName.RoomFailedMenu);
    }

    public override void OnJoinedRoom()
    {
        print("[Photon] Joined Room:" + PhotonNetwork.CurrentRoom.Name);
        MenuManager.Instance.OpenMenu(MenuName.RoomMenu);
        text_RoomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        // Prepare new player lists
        playerLists = new();

        // Remove previous room's player list
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        // Create player list in room
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            PlayerListItem newPlayerList = Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>();
            newPlayerList.Setup(players[i]);
            playerLists.Add(newPlayerList);
        }

        // Show button for host only
        button_StartGame.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        button_StartGame.SetActive(PhotonNetwork.IsMasterClient);

        // Setup for new host
        for (int i = 0; i < playerLists.Count; i++)
        {
            if (playerLists[i].Player == newMasterClient)
            {
                playerLists[i].Setup(newMasterClient);
            }
        }
    }

    public void StartGame()
    {
        print("[Photon] Start Game");
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        print("[Photon] Leaving Room:" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu(MenuName.LoadingMenu);
    }

    public override void OnLeftRoom()
    {
        print("[Photon] Leave Room");
        MenuManager.Instance.OpenMenu(MenuName.TitleMenu);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("[Photon] Player is joining: " + newPlayer.NickName);
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }
    #endregion
}
