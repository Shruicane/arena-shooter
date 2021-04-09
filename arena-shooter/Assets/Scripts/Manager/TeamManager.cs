using System.Collections;
using System.Collections.Generic;
using MLAPI.NetworkedVar;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar.Collections;

public class TeamManager : NetworkedBehaviour
{
    private NetworkedList<PlayerProperties> teamA = new NetworkedList<PlayerProperties>(
        new NetworkedVarSettings
            {WritePermission = NetworkedVarPermission.ServerOnly, ReadPermission = NetworkedVarPermission.Everyone});
    private NetworkedList<PlayerProperties> teamB = new NetworkedList<PlayerProperties>(
        new NetworkedVarSettings
            {WritePermission = NetworkedVarPermission.ServerOnly, ReadPermission = NetworkedVarPermission.Everyone});  
    private NetworkedList<ulong> teamAIDs = new NetworkedList<ulong>(
        new NetworkedVarSettings
            {WritePermission = NetworkedVarPermission.Everyone, ReadPermission = NetworkedVarPermission.Everyone});
    private NetworkedList<ulong> teamBIDs = new NetworkedList<ulong>(
        new NetworkedVarSettings
            {WritePermission = NetworkedVarPermission.Everyone, ReadPermission = NetworkedVarPermission.Everyone});   
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ClientsOnSameTeam(ulong client1, ulong client2)
    {
        print("ID1: " + client1 + " ID2: " + client2);
        print(teamAIDs.Count);
        return (teamAIDs.Contains(client1) && teamAIDs.Contains(client2)) ||
               (teamBIDs.Contains(client1) && teamBIDs.Contains(client2));
    }

    public void OnClientJoin(string name, ulong clientID)
    {
        StartCoroutine(JoinClientIfConnected(name, clientID));
    }

    IEnumerator JoinClientIfConnected(string name, ulong clientID)
    {
        yield return new WaitUntil(isConnected);
        InvokeServerRpc(OnClientJoinServerRpc, name, clientID);
    }

    bool isConnected()
    {
        return NetworkingManager.Singleton.IsConnectedClient;
    }
    
    [ServerRPC]
    public void OnClientJoinServerRpc(string name, ulong clientID)
    {
        if (teamA.Count < teamB.Count)
        {
            //teamA.Add(new PlayerProperties(Constants.TeamA, name, clientID));
            teamAIDs.Add(clientID);
            print("Added " + clientID + " name: " + name);
        }
        else
        {
            teamAIDs.Add(clientID);
            print("Added " + clientID + " name: " + name);
            //teamBIDs.Add(clientID);
            //teamB.Add(new PlayerProperties(Constants.TeamB, name, clientID));
        }
    }
}
