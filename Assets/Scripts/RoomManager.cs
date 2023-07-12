using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        // Game Scene
        if (scene.buildIndex == 1)
        {
            print("[Photon] Create Player Manager");
            GameObject newPlayerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "[Photon]PlayerManager"), Vector3.zero, Quaternion.identity);

            // Setup GUI
            LeaderBoardManager.Instance.SetupBoard();
        }
    }
}
