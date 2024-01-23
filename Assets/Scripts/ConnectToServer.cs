using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace PGGE
{
    namespace Multiplayer
    {
        public class ConnectToServer : MonoBehaviourPunCallbacks
        {
            //references to UI
            public InputField usernameInput;
            public Text buttonText;
            public GameObject BackBtn;


            //connects to server
            public void OnClickConnect()
            {
                //checks if the player input a username
                if (usernameInput.text.Length >= 1)
                {
                    //stores the username
                    PhotonNetwork.NickName = usernameInput.text;

                    //shows that it is connecting
                    buttonText.text = "Connecting...";

                    //deactivates back button so process won't be interupted
                    BackBtn.SetActive(false);

                    //connects to server
                    PhotonNetwork.ConnectUsingSettings();
                }
                else
                {
                    Debug.Log("No Username");
                }
            }

            //loads the lobby
            public override void OnConnectedToMaster()
            {
                SceneManager.LoadScene("Multiplayer_Lobby");
            }

            //back button
            public void ToMenu()
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}

