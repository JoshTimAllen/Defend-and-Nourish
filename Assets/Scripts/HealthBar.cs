using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] Unit myUnit = null;
    [SerializeField] Image bar = null;
    void Update() {
        bar.fillAmount = myUnit.Health / myUnit.MaxHealth;
    }
    private void LateUpdate() {
        transform.LookAt(Camera.main.transform.position);
    }
}