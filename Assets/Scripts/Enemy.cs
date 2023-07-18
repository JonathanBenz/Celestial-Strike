using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem deathExplosionVFX;
    [SerializeField] ParticleSystem hitpointVFX;
    GameObject parent;
    [SerializeField] int enemyScore = 15;
    [SerializeField] int enemyHP = 3;

    Scoreboard scoreboard;
    Rigidbody rb;

    private void Awake()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        rb = gameObject.AddComponent<Rigidbody>();
        parent = GameObject.FindWithTag("Enemy Explosions Container");
    }

    private void Start()
    {
        rb.useGravity = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (enemyHP < 1)
            DestroyEnemy();
        else
            TakeDamage();
    }

    void TakeDamage()
    {
        enemyHP--;
        ParticleSystem vfx = Instantiate(hitpointVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
    }

    void DestroyEnemy()
    {
        scoreboard.IncreaseScore(enemyScore);
        ParticleSystem vfx = Instantiate(deathExplosionVFX, this.transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }
}
