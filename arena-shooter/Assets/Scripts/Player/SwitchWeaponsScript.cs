using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class SwitchWeaponsScript : NetworkedBehaviour
{

    public GameObject sniperRifle;
    public GameObject pistol;
    public GameObject assaultRifle;
    public GameObject heavyRifle;

    public static string activeWeapon = Constants.NoWeapon;

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HideAllWeapons();
                ShowPistol();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HideAllWeapons();
                ShowAssaultRifle();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HideAllWeapons();
                ShowHeavyRifle();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HideAllWeapons();
                ShowSniperRifle();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                HideAllWeapons();
            }
        }
    }

    private void HideAllWeapons()
    {
        sniperRifle.SetActive(false);
        pistol.SetActive(false);
        heavyRifle.SetActive(false);
        assaultRifle.SetActive(false);
        activeWeapon = Constants.NoWeapon;
    }

    private void ShowSniperRifle()
    {
        sniperRifle.SetActive(true);
        activeWeapon = Constants.Sniper;
    }
    
    private void ShowPistol()
    {
        pistol.SetActive(true);
        activeWeapon = Constants.Pistol;
    }
    
    private void ShowAssaultRifle()
    {
        assaultRifle.SetActive(true); 
        activeWeapon = Constants.AssaultRifle;
    }
    
    private void ShowHeavyRifle()
    {
        heavyRifle.SetActive(true);
        activeWeapon = Constants.HeavyRifle;
    }
}
