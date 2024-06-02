using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatswordWeapon : WeaponBase
{

    [SerializeField] GameObject leftGreatswordObject;
    [SerializeField] GameObject rightGreatswordObject;

    PlayerMove playermove;

    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    private void Awake()
    {
        playermove = GetComponentInParent<PlayerMove>();
    }


    public override void Attack()
    {
        StartCoroutine(AttackProcess());
        
    }

    IEnumerator AttackProcess()
    {
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if (playermove.lastHorizontalDeCoupledvector > 0)
            {
                rightGreatswordObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightGreatswordObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                leftGreatswordObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftGreatswordObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            yield return new WaitForSeconds(0.3f);
        }
        
    }
} 
