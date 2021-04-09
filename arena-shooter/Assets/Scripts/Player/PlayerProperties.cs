using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties
{
    public int team;
    public string userName;
    public ulong clientID;

    public PlayerProperties(int team, string userName, ulong clientID)
    {
        this.team = team;
        this.userName = userName;
        this.clientID = clientID;
    }
}
