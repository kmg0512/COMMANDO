using UnityEngine;
using System.Collections;
using System;

public class CreeperController : MonoBehaviour, Shootable {

    public float gravity = 20f;
    public float speed = 2f;
    public float jumpSpeed = 5f;
    public float health = 100f;
    public GameObject yukari;

    private CharacterController m_controller;
    private Animator m_animator;
    private Vector3 moveDirection = Vector3.zero;
    private bool knockBack = false;
    private MonsterColorBlend blender = null;
    private float hitAnimationTimer = 0.5f;

    // Use this for initialization
    void Start () {
        m_controller = GetComponent<CharacterController>();
        blender = GetComponent<MonsterColorBlend>();
        m_animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(yukari != null)
        {
            if(m_controller.isGrounded)
            {
                Vector3 yukariPos = yukari.transform.position;
                Vector3 direction = (yukariPos - transform.position).normalized;
                moveDirection = transform.forward = new Vector3(direction.x, 0, direction.z);
                moveDirection *= speed;

                if(knockBack)
                {
                    moveDirection *= -0.5f;
                    moveDirection.y = jumpSpeed;
                    knockBack = false;
                }
            }
        }

        if(hitAnimationTimer < 0.5f)
        {
            hitAnimationTimer += Time.deltaTime;
            m_animator.speed = 5f;
        }
        else if(hitAnimationTimer >= 0.5f)
        {
            hitAnimationTimer = 0.5f;
            m_animator.speed = 1f;
        }

        if(health <= 0)
        {
            hitAnimationTimer = 0.5f;
            m_animator.speed = 1f;
            m_animator.SetBool("Dead", true);
        }

        // 이 코드는 가장 아래에 있어야 한다
        moveDirection.y -= gravity * Time.deltaTime;
        m_controller.Move(moveDirection * Time.deltaTime);
    }

    public void OnHit(GameObject yukari)
    {
        knockBack = true;
        if (blender != null) blender.Blend();
        hitAnimationTimer = 0;
        health -= yukari.GetComponent<YukariController>().damage;
    }
}
