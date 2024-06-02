using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenWeapon : WeaponBase
{

    [SerializeField] GameObject shurikenPrefab;


    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector3 newShurikenPosition = transform.position;
            SpawnProjectile(shurikenPrefab, newShurikenPosition);
        }
    }
}
