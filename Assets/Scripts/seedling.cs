using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedling : MonoBehaviour {
    [Header("Seed Types")]
    [SerializeField] GameObject attckSeed;
    [SerializeField] GameObject dfndSeed;
    [SerializeField] GameObject thornSeed;//The defensive seed with thorns
    [SerializeField] GameObject cloneSeed;//The defensive seed with thorns
    [SerializeField] soilNourishment nourishScript;

    Unit unit;
    private void Awake() {
        unit = GetComponent<Unit>();
        unit.Death_Event += OnDeath;
    }
    private void OnDeath(Unit unit) {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (nourishScript != null) {
            transform.position = nourishScript.transform.position + nourishScript.transform.up;
        }
    }

    //This class will choose which seed to spawn
    public void SpawnDescider() {
        if (nourishScript.nourishCount == 4) {
            Instantiate(cloneSeed, transform.position, transform.rotation);
        }
        if (nourishScript.nourishCount == 3) {
            Instantiate(thornSeed, transform.position, transform.rotation);
        }
        else if (nourishScript.nourishCount == 2) {
            Instantiate(dfndSeed, transform.position, transform.rotation);
        }
        else if (nourishScript.nourishCount == 1) {
            Instantiate(attckSeed, transform.position, transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "nourishment") { return; }
        if (Player.Instance.playerUnit.objectInHand == this.transform) {
            Player.Instance.playerUnit.dropObject();
        }
        soilNourishment nourishment = other.GetComponent<soilNourishment>();
        if (nourishment.seedlings.Count > 0) { return; }
        nourishScript = nourishment;
        nourishment.addSeed(this);
        Destroy(unit);
        StartCoroutine(nourishCounter((4 - nourishment.nourishCount) * 4f));
    }
    IEnumerator nourishCounter(float waitTIme) {
        yield return new WaitForSeconds(waitTIme);
        SpawnDescider();
        Destroy(nourishScript.gameObject);
    }
}
