using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;
using UnityEngine.UIElements;
using MLAPI.NetworkedVar;

public class PlayerShooting : NetworkedBehaviour
{

    public ParticleSystem bulletParticleSystem;
    private ParticleSystem.EmissionModule em;
    public float fireRate = 10f;
    private float shootTimer = 0f;

    private NetworkedVarBool isShooting = new NetworkedVarBool(new NetworkedVarSettings{WritePermission = NetworkedVarPermission.OwnerOnly}, false);
    
    
    //Audio
    public AudioClip sniperSound;
    
    
    void Start()
    {

        em = bulletParticleSystem.emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            isShooting.Value = Input.GetMouseButton(0) && !PauseMenuScript.isPaused;
            shootTimer += Time.deltaTime;

            if (isShooting.Value && shootTimer >= 1f/fireRate)
            {
                shootTimer = 0f;
                InvokeServerRpc(Shoot);
            }
            
        }
        em.rateOverTime = isShooting.Value ? fireRate : 0f;
    }

    private void PlaySniperRifleSound(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(sniperSound, pos, 1);
    }

    [ServerRPC]
    void Shoot()
    {
        InvokeClientRpcOnEveryone(PLayRifleSoundClientRpc, new Vector3(transform.position.x,transform.position.y, transform.position.z));
        Camera playerCamera = GetComponentInChildren<Camera>();
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            var player = hit.collider.GetComponent<PlayerHealth>();
            ulong enemyPlayerID = hit.collider.GetComponent<PlayerShooting>().NetworkId;
            
            
            if (player != null)
            {
                float dmg = 10f;
                player.TakeDamage(dmg);
            }
            
        }
        
    }

    [ClientRPC]
    void PLayRifleSoundClientRpc(Vector3 pos)
    {
        PlaySniperRifleSound(pos);
    }
}
