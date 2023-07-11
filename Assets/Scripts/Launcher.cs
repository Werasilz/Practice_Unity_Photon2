using Photon.Pun;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("GUI")]
    [SerializeField]
    private TMP_InputField input_RoomName;
    [SerializeField]
    private TextMeshProUGUI text_RoomName;

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
    }

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

    public override void OnJoinedRoom()
    {
        print("[Photon] Joined Room:" + PhotonNetwork.CurrentRoom.Name);
        MenuManager.Instance.OpenMenu(MenuName.RoomMenu);
        text_RoomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
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
}
