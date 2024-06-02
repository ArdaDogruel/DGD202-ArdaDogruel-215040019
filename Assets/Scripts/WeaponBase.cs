using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;


public enum DirectionOfAttack
{
    None,
    Forward,
    LeftRight,
    UpDown
}


public abstract class WeaponBase : MonoBehaviour
{
    PlayerMove playerMove;

    public WeaponData weaponData;

    public WeaponStats weaponStats;

    
    float timer;

    Character wielder;
    public Vector2 vectorOfAttack;
    [SerializeField] DirectionOfAttack attackDirection;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    

    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }


    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();

        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if (e != null)
            {
                ApplyDamage(colliders[i].transform.position, damage, e);
            }

        }
    }

    public void ApplyDamage(Vector3 position, int damage, IDamageable e)
    {
        PostDamage(damage, position);
        e.TakeDamage(damage);
        ApplyAdditionalEffects(e, position);
    }

    private void ApplyAdditionalEffects(IDamageable e, Vector3 enemyPosition)
    {
        e.Stun(weaponStats.stun);
        e.Knockback((enemyPosition - transform.position).normalized, weaponStats.knockback, weaponStats.knockbackTimeWeight);    
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        

        weaponStats = new WeaponStats(wd.stats);
    }


    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (int)(weaponData.stats.damage * wielder.damageBonus);
        return damage;
    }



    public virtual void PostDamage(int damage, Vector3 targetPosition) 
    {
        MessageSystem.instance.PostMassage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public void UpdateVectorOfAttack()
    {
        if(attackDirection == DirectionOfAttack.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Forward:
                vectorOfAttack.x = playerMove.lastHorizontalCoupledvector;
                vectorOfAttack.y = playerMove.lastVerticalCoupledvector;
                break;
            case DirectionOfAttack.LeftRight:
                vectorOfAttack.x = playerMove.lastHorizontalDeCoupledvector;
                vectorOfAttack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMove.lastVerticalDeCoupledvector;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }

    public GameObject SpawnProjectile(GameObject projectilePrefab, Vector3 position)
    {
        GameObject projectileGO = Instantiate(projectilePrefab);
        projectileGO.transform.position = position;

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.SetDirection(
            vectorOfAttack.x,
            vectorOfAttack.y
            );
        projectile.SetStats(this);

        return projectileGO;
    }
} 
