using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _RoomName;
    public RoomInfo _roomInfo;
     public void  SetInfo(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        _RoomName.text = roomInfo.Name;
    }
    public void OnClickRoomItem()
    {
        PhotonManager.Instance.JoinRoom(_roomInfo);
    }
}
