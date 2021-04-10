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
            {WritePermission = NetworkedVarPermission.Everyone, ReadPermission = NetworkedVarPermission.Everyone});
    private NetworkedList<PlayerProperties> teamB = new NetworkedList<PlayerProperties>(
        new NetworkedVarSettings
            {WritePermission = NetworkedVarPermission.Everyone, ReadPermission = NetworkedVarPermission.Everyone});  
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
        return (teamAIDs.Contains(client1) && teamAIDs.Contains(client2)) ||
               (teamBIDs.Contains(client1) && teamBIDs.Contains(client2));
    }

    public void OnClientJoin(string name, ulong clientID)
    {
        StartCoroutine(JoinClientIfConnected(name, clientID));
    }

    IEnumerator JoinClientIfConnected(string name, ulong clientID)
    {
        yield return new WaitUntil(IsConnected);
        //InvokeServerRpc(OnClientJoinServerRpc, name, clientID);
        OnClientJoinServer(name, clientID);
    }

    bool IsConnected()
    {
        if (NetworkingManager.Singleton.IsHost)
        {
            return true;
        }
        else
        {
            return NetworkingManager.Singleton.IsConnectedClient;
        }
    }
    
    [ServerRPC]
    public void OnClientJoinServerRpc(string name, ulong clientID)
    {
        if (teamAIDs.Count < teamBIDs.Count)
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
    
    public void OnClientJoinServer(string name, ulong clientID)
    {
        print("given id: " + clientID);
        print(teamAIDs.Contains(clientID));
        print("List length: " + teamAIDs.Count);
        teamAIDs.Add(clientID);
        
        PrintList(teamAIDs);
    }

    private void PrintList(NetworkedList<ulong> list)
    {
        print("List length: " + teamAIDs.Count);
        foreach (ulong entry in list)
        {
            print(entry);
        }
    }
}
