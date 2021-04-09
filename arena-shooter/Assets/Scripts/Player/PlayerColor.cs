using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class PlayerColor : NetworkedBehaviour
{

    public Material friendlyColor;
    public Material enemyColor;

    public GameObject gameManager;

    void Start()
    {
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if (IsLocalPlayer)
            {
                if(renderer.material.mainTexture == null){
                    renderer.material = friendlyColor;
                }
            }
            else
            {
                ulong localID = NetworkingManager.Singleton.LocalClientId;
                ulong otherID = renderer.GetComponentInParent<NetworkedObject>().NetworkId;
                if(renderer.material.mainTexture == null){
                    
                    if (gameManager.GetComponent<TeamManager>().ClientsOnSameTeam(localID, otherID))
                    {
                        renderer.material = friendlyColor;    
                    }
                    else
                    {
                        renderer.material = enemyColor;
                    }
                    
                }
            }

        }
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
