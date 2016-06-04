using UnityEngine;
using System.Collections;
using System;

public class CreeperController : MonoBehaviour, Shootable {

    public float gravity = 20f;
    public float speed = 2f;
    public GameObject yukari;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if(yukari != null)
        {
            Vector3 yukariPos = yukari.transform.position;
            Vector3 direction = (yukariPos - transform.position).normalized;
            transform.forward = new Vector3(direction.x, 0, direction.z);
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    public void OnHit(GameObject yukari)
    {
        moveDirection.y += 5f;
        Vector3 yukariPos = yukari.transform.position;
        Vector3 direction = -(yukariPos - transform.position).normalized;
        moveDirection += direction * 5f;
    }
}
