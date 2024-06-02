using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{ 

    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;



    public override void Attack()
    {
        UpdateVectorOfAttack();
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector3 newKnifePosition = transform.position;
            if(weaponStats.numberOfAttacks > 1)
            {
                newKnifePosition.y -= (spread * (weaponStats.numberOfAttacks-1)) / 2;
                newKnifePosition.y += i * spread;
            }
           
            SpawnProjectile(knifePrefab, newKnifePosition);
        } 
    }
}
