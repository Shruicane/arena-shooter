using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ConnectionManager : NetworkedBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameMenu;
    public void DisconnectAllClients()
    {
        SendDisconnectToHostServerRpc(NetworkingManager.Singleton.LocalClientId);
    }
    
    [ServerRPC]
    public void SendDisconnectToHostServerRpc(ulong hostClientID)
    {
        InvokeClientRpcOnEveryone(ForceDisconnectClientRpc);
    }
    
    [ClientRPC]
    public void ForceDisconnectClientRpc()
    {
        print("executed");
        if (NetworkingManager.Singleton.IsHost)
        {
            print("host");
            //PauseMenuScript.isPaused = true;
        }
        else
        {
            print("client");
            NetworkingManager.Singleton.StopClient();
        }
        gameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        PauseMenuScript.isPaused = false;
        pauseMenu.SetActive(false);
    }

    string GetPlayerName(ulong networkID)
    {
        return "";
    }

    ulong getPlayerNetworkID(string Name)
    {
        return 0;
    }
}
