using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    private string _name;
    private RecoilPattern _recoilPattern;
    private float _fireRate;
    private AudioClip _shootingSound;
    private AudioClip _equipSound;

    protected Weapon(string name, RecoilPattern recoilPattern, float fireRate, AudioClip shootingSound, AudioClip equipSound)
    {
        _name = name;
        _recoilPattern = recoilPattern;
        _fireRate = fireRate;
        _shootingSound = shootingSound;
        _equipSound = equipSound;
    }

    public AudioClip GetShootingSound()
    {
        return _shootingSound;
    }

    public AudioClip GetEquipSound()
    {
        return _equipSound;
    }

    public string GetName()
    {
        return _name;
    }
    
    public float GetFireRate()
    {
        return _fireRate;
    }
    
    public RecoilPattern GetRecoilPattern()
    {
        return _recoilPattern;
    }
    
    public void PlayWeaponSound(Vector3 pos)
    {
        
    }
    
    public void PlayEquipSound(Vector3 pos)
    {
        
    }
}
