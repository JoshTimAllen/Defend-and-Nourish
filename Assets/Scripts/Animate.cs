using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Animate : MonoBehaviour {
    [SerializeField] Animator animator = null;
    void Start() {

    }
    void Update() {

    }
    public void RunAnimation(bool start) {
        if (start) {
            SetBool("Run", start);
            SetLayerWeight(2, 1);
        }
        else {
            SetBool("Run", start);
            SetLayerWeight(2, 0);
        }
    }
    public void Play(string animationName, int layer = 0) {
        animator.Play(animationName, layer);
    }
    public void SetBool(string param, bool value) {
        animator.SetBool(param, value);
    }
    public void SetLayerWeight(int layerIndex, float weight) {
        animator.SetLayerWeight(layerIndex, weight);
    }
}