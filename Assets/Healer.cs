using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour {
    List<Unit> allyUnits = new List<Unit>();
    private void OnTriggerEnter(Collider other) {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null) {
            if (!unit.isEnemy) {
                if (!allyUnits.Contains(unit)) {
                    allyUnits.Add(unit);
                }
            }
        }
    }
    [SerializeField] float healValue = 100;
    private void OnTriggerExit(Collider other) {
        // var unit = 
        if (allyUnits.Find(x => x != null && x.gameObject == other.transform.gameObject) != null) {
            Unit unit = allyUnits.Find(x => x.transform.gameObject == other.transform.gameObject).GetComponent<Unit>();
            allyUnits.Remove(unit);
        }
    }
    private void Update() {
        foreach (var unit in allyUnits) {
            if (unit != null) {
                unit.Health += healValue * Time.deltaTime;
            }
        }
    }
}