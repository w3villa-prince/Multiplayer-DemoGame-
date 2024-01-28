using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreatePlayerController();

        }

    }

 public void CreatePlayerController()
    {

        Debug.Log($"Create Player Controller{PV.name}");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerControllerPrefabs"), Vector3.zero, Quaternion.identity);
    }
}
