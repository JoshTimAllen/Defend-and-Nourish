using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soilNourishment : MonoBehaviour {
    [SerializeField] public int nourishCount;
    [SerializeField] GameObject WhatIsSeed;

    [Space(25)]
    [Header("Child Components")]
    [Space(5)]

    [SerializeField] Light light1;
    [SerializeField] Light light2;

    //Spawn nourishment for the soil
    // Start is called before the first frame update
    void Start() {
        nourishCount = UnityEngine.Random.Range(1, 1);
        //Spawn nourishment for the soil
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("seed")) {
            //Set seed position
            WhatIsSeed.transform.position = this.transform.position;

            WhatIsSeed.GetComponentInChildren<Rigidbody>().velocity = new Vector3(0, .5f, 0);
            //Turn Gameobject mesh off
            Destroy(this.gameObject);

        }//if count is less than 3, increase the count once more
        if (other.CompareTag("nourishment")) {
            if (nourishCount <= 3) {
                //if this is called, increase the count
                nourishingCount();
                //Check for the Y position value of the other gameobject and remove it's lights if higher than next object!
                if (other.gameObject.transform.position.y <= this.gameObject.transform.position.y) {
                    Destroy(light1);
                    Destroy(light2);
                }
            }
        }


    }
    public List<seedling> seedlings = new List<seedling>();
    internal void addSeed(seedling seedling) {
        seedlings.Add(seedling);
    }

    public void nourishingCount() {
        nourishCount += 1;
    }
}
