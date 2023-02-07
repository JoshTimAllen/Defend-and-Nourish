using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhizome : MonoBehaviour {
    Unit rootUnit = null;
    private void Awake() {
        rootUnit = GameObject.FindGameObjectWithTag("Root Tree").GetComponent<Unit>();
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}