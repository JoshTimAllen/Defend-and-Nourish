using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour {
    // [SerializeField] CharacterController characterController = null;
    Unit unit;
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] NavMeshAgent navMeshAgent = null;
    private void Awake() {
        unit = GetComponent<Unit>();
    }
    private void Start() {
        // characterController = GetComponent<CharacterController>();
    }

    public Vector3 rotateDirection { get; set; }
    public Vector3 moveDirection { get; set; }
    private void Update() {
        rigidbody.MovePosition(transform.position + moveDirection * Time.deltaTime * unit.speed);
        // characterController.Move(moveDirection * Time.deltaTime * 4f);
        // if()
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotateDirection), Time.deltaTime * 40f);

        // if (Input.GetKey("Z")) {
        //     BasicAttack();
        // }
    }
    public void Rotate(Vector3 rotateDirection) {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotateDirection), Time.deltaTime * 40f);
    }
    public void Move(Vector3 vector) {
        moveDirection = vector;
    }
    public void AI_Move_To_Destination(Vector3 destination) {
        navMeshAgent.SetDestination(destination);
    }
    // [SerializeField]
    public void BasicAttack() {

    }
}
