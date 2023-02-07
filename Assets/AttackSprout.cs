using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSprout : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }
    [SerializeField] HitBox bulletHitbox = null;
    //Create variable to hold particle fx
    [SerializeField] ParticleSystem MuzzleParticleFx;
    [SerializeField] Unit myUnit = null;
    public List<Unit> UnitsHit = new List<Unit>();
    event System.Action<List<Unit>> unitsHit_Event;
    private void Awake() {
        // unitsHit_Event += bot.OnUnitHitsUpdate;
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
    [SerializeField] float shootRate = 0.3f;
    float timer;
    private void Update() {
        var enemyUnits = UnitsHit.FindAll(x => x.isEnemy != myUnit.isEnemy);
        timer += Time.deltaTime;
        if (enemyUnits.Count > 0) {
            if (timer > shootRate) {
                timer = 0;
                var bullet = Instantiate(bulletHitbox, transform.position, transform.rotation);
                //Instatiate a particle effects at the hitbox Pos for visual flare
                var bulletPartcle = Instantiate(MuzzleParticleFx, transform.position, transform.rotation);

                bullet.transform.GetComponent<Rigidbody>().velocity = ((enemyUnits[0].transform.position - bullet.transform.position).normalized * 13f);
                bullet.init(false, myUnit.damage, myUnit);
                bullet.gameObject.SetActive(true);
                Destroy(bullet, 30);
            }
        }
        else {
            timer = 0;
        }
    }
}