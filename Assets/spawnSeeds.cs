using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSeeds : MonoBehaviour {
    public static spawnSeeds Instance { get; private set; }
    public GameObject seed;
    public GameObject tree;
    public List<GameObject> mySeeds;

    IEnumerator coroutine;
    [SerializeField]
    float spawnSeedTime;
    int spawnCounter;

    void Start() {
        mySeeds = new List<GameObject>();
        spawnSeedTime = 60f;
        spawnCounter = 0;
        startTheCoroutine();
    }

    //Waits 10 seconds before spawning a seed 3 units away from the tree in the x or z direction
    IEnumerator SpawnSeed(float spawnSeedTime) {
        while (true) {
            if (mySeeds.Count < 30) {
                yield return new WaitForSeconds(spawnSeedTime);
                mySeeds.Add(Instantiate(seed, new Vector3(Random.Range(tree.transform.position.x - 10, tree.transform.position.x + 10), 1, Random.Range(tree.transform.position.z - 10, tree.transform.position.z + 10)), Quaternion.identity));
            }
            yield return null;
        }
    }

    public void startTheCoroutine() {
        coroutine = SpawnSeed(spawnSeedTime);
        StartCoroutine(coroutine);
    }
}
