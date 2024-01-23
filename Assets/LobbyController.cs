using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    //reference to UI buttons
    public GameObject JoinBtn;
    public GameObject CreateBtn;

    //reference to lobby panels
    public GameObject JoinRoomPanel;
    public GameObject CreateRoomPanel;


    //shows join panel
    public void JoinPanel()
    {
        if (JoinRoomPanel.activeSelf == false)
        {
            CreateRoomPanel.SetActive(false);
            JoinRoomPanel.SetActive(true);
        }
    }

    //shows create panel
    public void CreatePanel()
    {
        if (CreateRoomPanel.activeSelf == false)
        {
            JoinRoomPanel.SetActive(false);
            CreateRoomPanel.SetActive(true);
        }
    }

}
