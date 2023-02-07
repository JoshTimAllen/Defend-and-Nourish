using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {
    [SerializeField] Unit myUnit = null;
    List<Unit> unitsHit = new List<Unit>();
    [SerializeField] float damage;
    bool ignoreUnitsHit;
    public float durationTime;
    public float maxFrames = 1;

    /// <summary>
    /// resets units hit and initializes damage this hitbox should deal to units.
    /// </summary>
    /// <param name="ignoreUnitsHit"></param>
    /// <param name="damage"></param>
    public void init(bool ignoreUnitsHit, float damage, Unit unit) {
        myUnit = unit;
        unitsHit.Clear();
        this.ignoreUnitsHit = ignoreUnitsHit;
        this.damage = damage;
        rigidbody = GetComponent<Rigidbody>();
    }
    int frameCount = 0;
    public Vector3 direction;
    new Rigidbody rigidbody;
    private void Update() {
        if (durationTime > 0) {

        }
        else {
            if (frameCount >= maxFrames) {
                gameObject.SetActive(false);
                frameCount = 0;
                return;
            }
        }
        frameCount++;
    }
    /// <summary>
    /// Checks to see if this hitbox hits a unit so that unit takes damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        Unit unit = other.GetComponent<Unit>();
        if (unit == null) { return; }
        if (unit == myUnit || (unit.isEnemy == myUnit.isEnemy)) { return; }
        if (!ignoreUnitsHit) {
            if (unitsHit.Contains(unit)) { return; }
        }
        if (!unitsHit.Contains(unit)) {
            unitsHit.Add(unit);
        }
        unit.TakeDamge(damage, myUnit);
        if (gameObject.tag == "Bullet") {
            Destroy(gameObject);
        }
    }
}