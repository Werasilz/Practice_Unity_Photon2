using UnityEngine;
using Photon.Pun;
using System.IO;
using Cinemachine;

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
        // Create controller and camera
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "[Photon]PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
