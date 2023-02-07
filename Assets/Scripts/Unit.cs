using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all units that can take damage and has stats such as speed and damage.
/// 
/// </summary>

public class Unit : MonoBehaviour {
    public bool isEnemy;
    public float Health;
    public float MaxHealth;
    public float speed;
    public float damage = 1;
    [SerializeField] float baseSpeed;
    [Space]
    [Space]
    // public int priority;
    /// <summary>
    /// Hit box for a basic attack
    /// </summary>
    [SerializeField] HitBox basicAttackHitbox = null;
    /// <summary>
    /// The object this unit is carrying
    /// </summary>
    /// <returns></returns>
    public Transform objectInHand = null;
    void Start() {

    }
    private void Update() {
        if (objectInHand != null) {
            speed = 0.7f * baseSpeed;
            objectInHand.position = transform.position + transform.forward;
            Vector3 pos = objectInHand.transform.position;
            pos.y += 3f;
            objectInHand.transform.position = pos;
            objectInHand.rotation = transform.rotation;
        }
        else {
            speed = baseSpeed;
        }

        Health += 1 * Time.deltaTime;

    }
    /// <summary>
    /// Returns the unit when health is and below 0
    /// </summary>
    public event System.Action<Unit> Death_Event;
    public event System.Action<float, Unit> TakeDamage_Event;
    /// <summary>
    /// Call for unit to take damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamge(float damage, Unit unitSourceDamage) {
        if (Health <= 0) { return; }
        Health -= damage;
        TakeDamage_Event?.Invoke(damage, unitSourceDamage);
        if (Health <= 0) {
            Death_Event?.Invoke(this);
        }
    }
    /// <summary>
    /// Initializes and activates the hitbox
    /// </summary>
    public void BasicAttack() {
        basicAttackHitbox.init(false, damage, this);
        basicAttackHitbox.gameObject.SetActive(true);
    }
    /// <summary>
    /// Puts object in hand
    /// </summary>
    /// <param name="unit"></param>
    public void pickUp(Unit unit) {
        objectInHand = unit.transform;
        if (spawnSeeds.Instance.mySeeds.Contains(unit.gameObject)) {
            spawnSeeds.Instance.mySeeds.Remove(unit.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {

    }
    internal void dropObject() {
        objectInHand = null;
    }
}