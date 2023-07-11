using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        print("Connecting to Master");
        MenuManager.Instance.OpenMenu(MenuName.LoadingMenu);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.CloseMenu(MenuName.LoadingMenu);
        MenuManager.Instance.OpenMenu(MenuName.TitleMenu);
        print("Joined Lobby");
    }

    void Update()
    {

    }
}
