using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scans and triggers if unit is in radius of the collider
/// </summary>

public class RadiusTrigger : MonoBehaviour {
    [SerializeField] Unit myUnit = null;
    [SerializeField] Bot bot = null;
    public List<Unit> UnitsHit = new List<Unit>();
    event System.Action<List<Unit>> unitsHit_Event;
    private void Awake() {
        unitsHit_Event += bot.OnUnitHitsUpdate;
    }
    private void OnTriggerEnter(Collider other) {
        if (UnitsHit.Find(x => x != null && x.gameObject == other.gameObject)) { return; }
        Unit unit = null;
        unit = other.GetComponent<Unit>();
        if (unit == null) { return; }
        UnitsHit.Add(unit);
        unitsHit_Event?.Invoke(UnitsHit);
        if (unit.isEnemy != myUnit) {

        }
    }
    private void OnTriggerExit(Collider other) {
        if (UnitsHit.Find(x => x != null && x.gameObject == other.gameObject)) {
            UnitsHit.Remove(UnitsHit.Find(x => x != null && x.gameObject == other.gameObject));
        }
    }
    private void OnDestroy() {
        unitsHit_Event -= bot.OnUnitHitsUpdate;
    }
}