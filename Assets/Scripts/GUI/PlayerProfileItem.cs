using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerProfileItem : MonoBehaviourPunCallbacks
{
    [Header("GUI")]
    [SerializeField] TMP_Text text_PlayerName;
    [SerializeField] TMP_Text text_PlayerScore;
    public string playerProfileName { get; private set; }

    public Player Player { get; private set; }

    public void Setup(Player _player)
    {
        Player = _player;
        text_PlayerName.text = _player.NickName;
        playerProfileName = _player.NickName;
        text_PlayerScore.text = "0";
    }

    public void SetScoreText(int score)
    {
        text_PlayerScore.text = score.ToString();
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
