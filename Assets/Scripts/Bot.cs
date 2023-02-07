using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ai script that can either be used for enemies or alies depending on Unit.isEnemy
/// </summary>

public class Bot : MonoBehaviour {
    [SerializeField] Unit myUnit = null;
    [SerializeField] Controller controller = null;
    bool isEnemy { get { return myUnit.isEnemy; } }
    Unit rootUnit = null;
    [SerializeField] Animate animate = null;
    [SerializeField] soilNourishment soilNourishmentPrefab = null;
    private void Awake() {
        rootUnit = GameObject.FindGameObjectWithTag("Root Tree").GetComponent<Unit>();
        myUnit.TakeDamage_Event += OnTakeDamage;
        myUnit.Death_Event += OnDeath;
    }

    private void OnDeath(Unit obj) {
        soilNourishment soilNourishment = Instantiate(soilNourishmentPrefab, transform.position, transform.rotation);
        soilNourishment.transform.eulerAngles = new Vector3(0f, 0, 0);
        if (GameManager.instance.enemyList.Contains(gameObject)) {
            GameManager.instance.enemyList.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnTakeDamage(float damage, Unit unitSourceDamage) {
        targetUnit = unitSourceDamage;
        if (UnitsHit.Contains(unitSourceDamage)) {
            int index = UnitsHit.IndexOf(unitSourceDamage);
            var temp = UnitsHit[0];
            UnitsHit[index] = UnitsHit[0];
            UnitsHit[0] = unitSourceDamage;
        }
        else {
            var temp = UnitsHit[0];
            UnitsHit[0] = unitSourceDamage;
            UnitsHit.Add(temp);
        }
    }

    void Start() {

    }
    /// <summary>
    /// All units hit in range
    /// </summary>
    /// <typeparam name="Unit"></typeparam>
    /// <returns></returns>
    List<Unit> UnitsHit = new List<Unit>();
    /// <summary>
    /// Units hit Callback function
    /// </summary>
    /// <param name="unitHits"></param>
    public void OnUnitHitsUpdate(List<Unit> unitHits) {
        UnitsHit = unitHits;
    }
    Unit targetUnit;
    private void Update() {
        /// <summary>
        /// Searches for enemy units
        /// </summary>
        /// <returns></returns>
        var enemyUnits = UnitsHit.FindAll(x => x.isEnemy != myUnit.isEnemy);

        targetUnit = null;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        if (UnitsHit.Count > 0) {
            foreach (Unit unit in enemyUnits) {
                if (unit != rootUnit) {
                    targetUnit = unit;
                    break;
                }
            }
        }

        if (isEnemy) {
            if (targetUnit == null) {
                targetUnit = rootUnit;
            }
        }

        if (targetUnit != null) {
            controller.AI_Move_To_Destination(targetUnit.transform.position);// Move((targetUnit.transform.position - transform.position).normalized * myUnit.speed * Time.deltaTime);
            controller.Rotate((targetUnit.transform.position - transform.position).normalized);

            float targetDistance = 5;
            if (rootUnit == targetUnit) {
                targetDistance = 10f;
            }

            if (Vector3.Distance(transform.position, targetUnit.transform.position) < 4f) {
                myUnit.BasicAttack();
            }
            animate.RunAnimation(true);
        }
    }
    // void OnDrawGizmosSelected() {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(transform.position, 15);
    // }
}
