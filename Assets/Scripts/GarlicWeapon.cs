using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicWeapon : WeaponBase
{
    [SerializeField] float attackAreaSize;

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
        ApplyDamage(colliders);
    }
}
