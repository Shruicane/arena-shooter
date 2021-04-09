using System.Collections;
using System.Collections.Generic;
using MLAPI.Messaging;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _team;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ClientRPC]
    public void UpdatePlayerMeshs()
    {
        //updates the color of each player 
        //likely to be called when a new player joins the game
    }

    [ClientRPC]
    public void setTeam(int team)
    {
        _team = team;
    }

    public int getTeam()
    {
        return _team;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
