using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using MLAPI.NetworkedVar.Collections;
using MLAPI.Transports.UNET;
using UnityEngine;
using UnityEngine.UI;


public class MenuScript : NetworkedBehaviour
{

    public GameObject menuPanel;
    public GameObject gameManager;
    
    public InputField ipTextInputField;
    public InputField portTextInputField;
    public InputField usernameTextInputField;
    
    //List of players can only be edited by the Server
    public NetworkedList<string> players = new NetworkedList<string>(new NetworkedVarSettings{ReadPermission = NetworkedVarPermission.Everyone, WritePermission = NetworkedVarPermission.ServerOnly});
    //public NetworkedVar<List<string>> playerList = 
    //    new NetworkedVar<List<string>>(new NetworkedVarSettings{ReadPermission = NetworkedVarPermission.Everyone, WritePermission = NetworkedVarPermission.Everyone});
    public NetworkedList<string> teamAList = 
        new NetworkedList<string>(new NetworkedVarSettings{ReadPermission = NetworkedVarPermission.Everyone, WritePermission = NetworkedVarPermission.Everyone});
    public NetworkedList<string> teamBList = 
        new NetworkedList<string>(new NetworkedVarSettings{ReadPermission = NetworkedVarPermission.Everyone, WritePermission = NetworkedVarPermission.Everyone});

    public void ClickHost()
    {
        string username = GetValidUsernameServerRpc(usernameTextInputField.text);
        NetworkingManager.Singleton.StartHost();
        gameManager.GetComponent<TeamManager>().OnClientJoin(username, NetworkingManager.Singleton.LocalClientId);
        menuPanel.SetActive(false);
    }

    public void ClickJoin()
    {
        string username = GetValidUsernameServerRpc(usernameTextInputField.text);
        string validIP = GetValidIPAdress(ipTextInputField.text);
        int validPort = GetValidPort(portTextInputField.text);
        NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = validIP;
        NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectPort = validPort;
        NetworkingManager.Singleton.StartClient();
        gameManager.GetComponent<TeamManager>().OnClientJoin(username, NetworkingManager.Singleton.LocalClientId);
        menuPanel.SetActive(false);
    }

    [ServerRPC]
    private string GetValidUsernameServerRpc(string username)
    {
        string result = username;
        int i = 1;
        while (players.Contains(username) || username.Length < 1)
        {
            result = username + i;
            i++;
        }
        return result;
    }

    private string GetValidIPAdress(string ip)
    {
        ip = ip.Replace(" ", "");
        string[] blocks = ip.Split('.');
        
        if (blocks.Length != 4)
        {
            return "127.0.0.1";
        }
        
        foreach (string block in blocks)
        {
            if (! int.TryParse(block, out int j) || j < 0 || j > 255)
            {
                return "127.0.0.1";
            }
        }
        
        return ip;
    }

    private int GetValidPort(string port)
    {
        port = port.Replace(" ", "");
        if (int.TryParse(port, out int intPort) && intPort <= 65535 && intPort >= 0)
        {
            return intPort;
        }
        else
        {
            return 7777;
        }
    }
    
}
