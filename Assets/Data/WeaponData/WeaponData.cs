using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;
    public int numberOfHits;
    public float projectileSpeed;
    public float stun;
    public float knockback;
    public float knockbackTimeWeight;
   

    public WeaponStats(WeaponStats stats)
    {
        this.damage = stats.damage;
        this.timeToAttack = stats.timeToAttack;
        this.numberOfHits = stats.numberOfHits;
        this.numberOfAttacks = stats.numberOfAttacks;
        this.projectileSpeed = stats.projectileSpeed;
        this.stun = stats.stun;
        this.knockback = stats.knockback;
        this.knockbackTimeWeight = stats.knockbackTimeWeight;
    }


    public void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        this.numberOfHits += weaponUpgradeStats.numberOfHits;
        this.projectileSpeed += weaponUpgradeStats.projectileSpeed;
        this.stun += weaponUpgradeStats.stun;
        this.knockback += weaponUpgradeStats.knockback;
        this.knockbackTimeWeight += weaponUpgradeStats.knockbackTimeWeight;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
