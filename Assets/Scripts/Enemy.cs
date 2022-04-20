using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Place your explosion VFX here, will play on enemies death")]
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [Tooltip("The amount of time for the enemy to be destroyed after being hit")] 
    [SerializeField] float destroyTime = 0.5f;
    [SerializeField] int scorePerHit = 25;
    [SerializeField] int scorePerDeath = 25;
    [Header("Enemy Health")]
    [Tooltip("Change value to increade enemies health")]
    [SerializeField] int hitPoints = 100;
    int damage = 25;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;


    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        //Don't use FindObjectOfType in update very heavy to use
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitPoints = hitPoints - damage;
        vfx.transform.parent = parentGameObject.transform;
        scoreBoard.IncreaseScore(scorePerHit);
    }
    void KillEnemy()
    {
        if (hitPoints < 1)
        {
            GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
            vfx.transform.parent = parentGameObject.transform;
            scoreBoard.IncreaseScore(scorePerDeath);
            Destroy(gameObject, destroyTime);
        }
    }
    
}
