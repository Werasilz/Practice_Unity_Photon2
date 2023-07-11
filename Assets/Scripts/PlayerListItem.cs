using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [Header("GUI")]
    [SerializeField] TMP_Text text_PlayerName;

    public Player Player { get; private set; }

    public void Setup(Player _player)
    {
        Player = _player;

        if (_player.IsMasterClient)
        {
            text_PlayerName.text = _player.NickName + "[Host]";
        }
        else
        {
            text_PlayerName.text = _player.NickName;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (Player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
