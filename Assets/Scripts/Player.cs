using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }
    public Unit playerUnit = null;
    [SerializeField] Controller controller = null;
    [SerializeField] Animate animate = null;

    public KeyCode attackBtn;
    public KeyCode pickUpBtn;
    private void Awake() {
        Instance = this;
        // animate = GetComponent<Animate>();
    }
    void Start() {

    }
    Vector3 worldPos;
    [SerializeField] LayerMask layerMask = new LayerMask();
    Camera cam { get { return Camera.main; } }
    [SerializeField] Transform cursor = null;
    private object collisionMask;
    [SerializeField] Transform characterDisplay = null;
    [SerializeField] new Rigidbody rigidbody;
    void Update() {
        if (spinAttacking) {
            BasicAttack();
            characterDisplay.transform.Rotate(new Vector3(0, 360f * Time.deltaTime * 2, 0), Space.Self);
        }
        // Vector3 moveDriection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") != 0) {
            rigidbody.velocity = playerUnit.transform.forward * Input.GetAxis("Vertical") * playerUnit.speed;
        }
        playerUnit.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 180 * Time.deltaTime, 0), Space.Self);

        // controller.Move(moveDriection);

        if (Input.GetKeyDown(attackBtn)) {
            BasicAttack();
        }
        if (Input.GetKeyDown("space")) {
            // rigidbody.AddForce(Vector3.up * playerUnit.speed * 20f * 0.7f);
            // pickUp();
        }
        if (Input.GetKeyDown(pickUpBtn)) {
            pickUp();
        }
        if (Input.GetKeyDown("1")) {
            spinAttacking = !spinAttacking;
            characterDisplay.localEulerAngles = new Vector3();
        }
        RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)) {
            Transform objectHit = hit.transform;
            worldPos = hit.point;
            // Debug.Log(hit.transform);
            // Do something with the object that was hit by the raycast.
            // Debug.DrawRay(cam.transform.position, (cam.transform.position - hit.transform.position).normalized * hit.distance, Color.yellow);
            // Debug.Log("Did Hit");
        }
        else {
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            // Debug.Log("Did not Hit");
        }
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward * 100000, Color.red);
        worldPos.y = playerUnit.transform.position.y;
        playerUnit.transform.LookAt(worldPos);

        // playerUnit.transform.eulerAngles = new Vector3(playerUnit.transform.eulerAngles.x, 0, playerUnit.transform.eulerAngles.z);
        cursor.transform.position = worldPos;

        // Vector3 dir = (playerUnit.transform.position - worldPos).normalized;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            if (!spinAttacking) {
                animate.RunAnimation(true);
            }
            // controller.rotateDirection = dir;
        }
        else {
            if (!spinAttacking) {
                animate.RunAnimation(false);
            }
        }
    }
    bool spinAttacking;
    private void spinAttack() {
        spinAttacking = true;
    }

    public void BasicAttack() {
        playerUnit.BasicAttack();
        animate.Play("Basic Attack");
        // playerUnit.pickUp():
    }

    public void pickUp() {
        if (playerUnit.objectInHand != null) {
            playerUnit.dropObject();
            return;
        }
        print("pick");
        RaycastHit[] hits = new RaycastHit[0];
        Vector3 p1 = playerUnit.transform.position;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.

        hits = Physics.SphereCastAll(p1, 2, playerUnit.transform.forward);
        // Physics.SphereCastNonAlloc(p1, 2f, playerUnit.transform.forward, hits);
        print(hits.Length);

        if (hits.Length > 0) {
            foreach (var hit in hits) {
                if (hit.transform.tag == "Seed") {
                    Unit seedUnit = hit.transform.gameObject.GetComponent<Unit>();
                    playerUnit.pickUp(seedUnit);
                    break;
                }
            }
        }

    }
}