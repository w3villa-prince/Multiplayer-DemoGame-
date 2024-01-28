using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField _newRoomName;
    [SerializeField] TextMeshProUGUI _errorName;
    [SerializeField] TextMeshProUGUI _currentRoomName;
    [SerializeField] Transform _roomListTransfrom;
    [SerializeField] GameObject _roomPrefabItems;
    static public PhotonManager Instance;
    [SerializeField] Transform _playerListTransfrom;
    [SerializeField] GameObject _playerPrefabItems;
    [SerializeField] GameObject _StartGameBtn;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log($"Maked Connection ");
        PhotonNetwork.AutomaticallySyncScene = true;

    }
    public override void OnConnectedToMaster()
    {
        MenuManger.menuManger.OpenMenu("Title");
        PhotonNetwork.JoinLobby();
        Debug.Log($"Join Lobby");
        // base.OnConnectedToMaster();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log($"Joined Lobby Now You");

        // base.OnJoinedLobby();
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_newRoomName.text))
        {
            Debug.Log($" {_newRoomName.text} not set");

            return;
        }
        MenuManger.menuManger.OpenMenu("Loadding"); 
        PhotonNetwork.CreateRoom(_newRoomName.text);
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }

    public override void OnJoinedRoom()
    {
        MenuManger.menuManger.OpenMenu("Room");
        Debug.Log($" {PhotonNetwork.CurrentRoom.Name}");
        _currentRoomName.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform item in _playerListTransfrom)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < players.Count(); i++)
        { 
            Instantiate(_playerPrefabItems, _playerListTransfrom).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        _StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);

    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {

        _StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorName.text = "Room Creation Failed " + message;
        MenuManger.menuManger.OpenMenu("Error");

    }
    public  void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManger.menuManger.OpenMenu("Loadding");

    }
    public override void OnLeftRoom()
    {
        MenuManger.menuManger.OpenMenu("Title");
   
    }
    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        MenuManger.menuManger.OpenMenu("Loadding");
       


    }
    public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
    {
        foreach( Transform item  in _roomListTransfrom)
        {
            Destroy(item.gameObject);
        }
        for(int i=0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                 continue;
            Instantiate(_roomPrefabItems, _roomListTransfrom).GetComponent<RoomItem>().SetInfo(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerPrefabItems, _playerListTransfrom).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
