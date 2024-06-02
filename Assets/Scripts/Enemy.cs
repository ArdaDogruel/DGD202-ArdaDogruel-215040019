using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int hp = 999;
    public int damage = 1;
    public int experience_reward = 300;
    public float moveSpeed = 1f;
    

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.moveSpeed = stats.moveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);

    }
}

public class Enemy : MonoBehaviour, IDamageable
{
    Transform targetDestination;
    GameObject targetGameObject;
    Character targetCharacter;  

    Rigidbody2D rgdbd2d;

    public EnemyStats stats;
    [SerializeField] EnemyData enemyData;

    float stunned;
    Vector3 knockbackVector;
    float knockbackForce;
    float knockbackTimeWeight;


    private void Awake()
    {
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (enemyData != null)
        {
            InitSprite(enemyData.animatedPrefab);
            SetStats(enemyData.stats);
            SetTarget(GameManager.Instance.playerTransform.gameObject);
        }
    }

    public void SetTarget(GameObject target) 
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }


    private void FixedUpdate()
    {
        ProcessStun();
        Move();
    }

    private void ProcessStun()
    {
        if(stunned > 0f)
        {
            
            stunned -= Time.fixedDeltaTime;
        }   
    }

    private void Move()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = CalculateMovmentVelocity(direction) + CalculateKnockBack();
    }

    private Vector3 CalculateMovmentVelocity(Vector3 direction)
    {
        return direction * stats.moveSpeed * (stunned > 0f ? 0f : 1f);
    }

    private Vector3 CalculateKnockBack()
    {
        if (knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        } 
        return knockbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 0f : 0f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject) 
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (targetCharacter == null) 
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.damage);

    }

    public void TakeDamage (int damage) 
    {
        stats.hp -= damage;

        if(stats.hp < 1)
        {
            targetGameObject.GetComponent<Level>().AddExperience(stats.experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);

        }

    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }

    internal void InitSprite(GameObject animatedPrefab)
    {
        GameObject spriteObject = Instantiate(animatedPrefab);
        spriteObject.transform.parent = transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }

    public void Stun(float stun)
    {
        stunned = stun;
    }

    public void Knockback(Vector3 vector, float force, float timeWeight)
    {
        knockbackVector = vector;
        knockbackForce = force;
        knockbackTimeWeight = timeWeight;
    }
}
 