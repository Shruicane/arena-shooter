using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;

public class PlayerHealth : NetworkedBehaviour
{
    //public NetworkedVar<float> playerHealth = new NetworkedVar<float>(new NetworkedVarSettings {WritePermission = NetworkedVarPermission.Everyone, ReadPermission = NetworkedVarPermission.Everyone}, 100f);
    public NetworkedVarFloat playerHealth = new NetworkedVarFloat(new NetworkedVarSettings {WritePermission = NetworkedVarPermission.Everyone, ReadPermission = NetworkedVarPermission.Everyone}, 100f);

    public GameObject playerSpawnTeamA;
    public GameObject playerSpawnTeamB;

    private MeshRenderer[] _meshRenderers;
    private CharacterController _cc;

    private void Start()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _cc = GetComponent<CharacterController>();
    }

    public void TakeDamage(float damage)
    {
        playerHealth.Value -= damage;

        if (playerHealth.Value <= 0)
        {
            //Vector3 pos = playerSpawnTeamA.transform.position;
            Vector3 pos = new Vector3(-50, 60, 0);
            playerHealth.Value = 100;
            InvokeClientRpcOnEveryone(ClientRespawn, pos);
            print("Respawn");
        }
    }
    
    public float GetHP()
    {
        return playerHealth.Value;
    }
    
    [ClientRPC]
    private void ClientRespawn(Vector3 respawnPosition)
    {
        StartCoroutine(Respawn(respawnPosition));
    }

    IEnumerator Respawn(Vector3 pos)
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            meshRenderer.enabled = false;
        }
        yield return new WaitForSeconds(4);
        _cc.enabled = false;
        transform.position = pos;
        _cc.enabled = true;
        

        foreach (var meshRenderer in _meshRenderers)
        {
            meshRenderer.enabled = true;
        }
    }
}
