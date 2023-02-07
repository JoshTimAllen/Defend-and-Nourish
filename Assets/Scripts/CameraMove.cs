using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public static CameraMove Instance { get; private set; }
    [SerializeField] new Camera camera = null;
    [SerializeField] Transform Target = null;
    [SerializeField] Vector3 Offset = new Vector3(4.434425f, 4.809996f, -5.71656f);
    Vector3 TargetPosition;
    private void Awake() {
    }
    void Start() {

    }
    void LateUpdate() {
        Vector3 direction = (TargetPosition - transform.position).normalized;
        // TargetPosition = Target.position + Offset;
        TargetPosition = Target.position + Offset + (Target.forward * 1);

        float smoothSpeed = 1f + Mathf.Pow(Vector3.Distance(camera.transform.position, TargetPosition), 3f);
        transform.position = TargetPosition;// Vector3.SmoothDamp(transform.position, TargetPosition, ref refVelocity, smoothTime, smoothSpeed, Time.deltaTime);

        Vector3 smoothedVector = Vector3.SmoothDamp(camera.transform.position, TargetPosition + (Target.forward * 3), ref camRefVelocity, smoothTime, smoothSpeed, Time.deltaTime);
        camera.transform.position = transform.position;// smoothedVector;// Vector3.LerpUnclamped(transform.position, smoothedVector, 20 * Time.deltaTime);

        if (Vector3.Distance(transform.position, TargetPosition) > 100f) {
            transform.position = TargetPosition;
            camera.transform.position = TargetPosition;
        }
    }
    [SerializeField] float smoothTime = 0.01f;
    Vector3 refVelocity;
    Vector3 camRefVelocity;
}