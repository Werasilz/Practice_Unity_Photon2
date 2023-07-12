using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;

    [SerializeField]
    private int score;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();

        // Create controller and camera
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "[Photon]PlayerController"), spawnPoint.position, Quaternion.identity);
    }

    public void GetScore()
    {
        score++;
        photonView.RPC("RPC_GetScore", RpcTarget.All, score);
    }

    [PunRPC]
    private void RPC_GetScore(int score)
    {
        LeaderBoardManager.Instance.UpdateScore(score, photonView.Owner.NickName);
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.photonView.Owner == player);
    }
}
