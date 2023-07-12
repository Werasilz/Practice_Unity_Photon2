using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView photonView;

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
}
