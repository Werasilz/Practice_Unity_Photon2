using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("GUI")]
    [SerializeField]
    private TMP_InputField input_RoomName;
    [SerializeField]
    private TMP_Text text_RoomName;

    [Header("Room")]
    [SerializeField]
    private Transform playerListContent;
    [SerializeField]
    private GameObject playerListItemPrefab;

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
        PhotonNetwork.CreateRoom(input_RoomName.text);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

    }

    public void FindRoom()
    {
        print("[Photon] Joining Random Room");
        PhotonNetwork.JoinRandomRoom();
        MenuManager.Instance.OpenMenu(MenuName.LoadingMenu);
    }

    public override void OnJoinedRoom()
    {
        print("[Photon] Joined Room:" + PhotonNetwork.CurrentRoom.Name);
        MenuManager.Instance.OpenMenu(MenuName.RoomMenu);
        text_RoomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        // Remove previous room's player list
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        // Create player list in room
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(players[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("[Photon] Room Creation Failed: " + message);
        MenuManager.Instance.OpenMenu(MenuName.RoomFailedMenu);
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
