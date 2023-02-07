using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField]
    int energy;

    void Start()
    {
        energy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // energy += 1;
        // Debug.Log("Player's energy = " + energy);
    }
}
