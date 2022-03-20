using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("The amount of time for the enemy to be destroyed after being hit")] 
    [SerializeField] float destroyTime = 0.5f;
    private void OnParticleCollision(GameObject other) 
    {
        Debug.Log(this.name + "--I'm hit by--" + other.gameObject.name);
        Destroy(gameObject, destroyTime);
    }
}
