using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

//namespace simplification
namespace PGGE.Multiplayer
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        #region Variables

        private const string gameVersion = "1";


        //changed variable names to be more clear

        //added a header for readability and organisation
        //logic: Controls game-related settings and parameters
        [Header("Logic")]
        [SerializeField] private byte maxPlayersPerRoom = 5;

        //added a header for readability and organisation
        //UI: Handles references to UI elements
        [Header("UI")]
        [SerializeField] private Text joinButtonText;
        [SerializeField] private GameObject backButton;

        //added a header for readability and organisation
        //Flags: Handles checks with booleans
        [Header("Flags")]
        private bool isConnecting = false;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        #endregion

        #region Connection Logic

        //added functions instead of the code itself for readability
        //easier to understand when looked at
        public void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                //new function
                StartConnecting();
            }

            //new function
            JoinRandomRoom();
        }

        //function to connect the player to the server
        private void StartConnecting()
        {
            isConnecting = true;
            joinButtonText.text = "Connecting...";
            backButton.SetActive(false);
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        #endregion

        #region Room Management

        //function to join a random room
        private void JoinRandomRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("OnConnectedToMaster() was called by PUN");
                PhotonNetwork.JoinRandomRoom();
            }
        }

        //added functions instead of the code itself for readability
        //easier to understand when looked at
        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom() called by PUN. Client is in a room.");
            if (PhotonNetwork.IsMasterClient)
            {
                //new function
                LoadDefaultRoom();
            }
        }

        //function to load the default room which is the multiplayer map
        private void LoadDefaultRoom()
        {
            Debug.Log("We load the default room for multiplayer");
            PhotonNetwork.LoadLevel("MultiplayerMap00");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
            isConnecting = false;
        }

        //added functions instead of the code itself for readability
        //easier to understand when looked at
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by PUN. " +
                "No random room available" +
                ", so we create one by Calling: " +
                "PhotonNetwork.CreateRoom");

            //new function
            CreateNewRoom();
        }

        //function to create a new room
        private void CreateNewRoom()
        {
            PhotonNetwork.CreateRoom(null,
                new RoomOptions
                {
                    MaxPlayers = maxPlayersPerRoom
                });
        }

        #endregion

        #region Scene Navigation

        public void ToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        #endregion
    }
}
