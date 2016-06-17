using UnityEngine;
using System.Collections;
using System;

public enum CreeperState
{
    Alive,
    Dead
}

public class CreeperController : MonoBehaviour, Shootable {

    public float gravity = 20f;
    public float speed = 2f;
    public float jumpSpeed = 5f;
    public float health = 100f;
    public GameObject yukari;

    private CreeperState m_state;
    private CharacterController m_controller;
    private Animator m_animator;
    private Vector3 moveDirection = Vector3.zero;
    private bool m_knockBack = false;
    private MonsterColorBlend m_blender = null;
    private float m_hitAnimationTimer = 0.5f;
    private float m_destroyTimer = 0f;

    // Use this for initialization
    void Start () {
        m_controller = GetComponent<CharacterController>();
        m_blender = GetComponent<MonsterColorBlend>();
        m_animator = GetComponent<Animator>();

        m_state = CreeperState.Alive;
    }
	
	// Update is called once per frame
	void Update () {
        if(yukari != null)
        {
            if(m_controller.isGrounded)
            {
                if(m_state == CreeperState.Alive)
                {
                    Vector3 yukariPos = yukari.transform.position;
                    Vector3 direction = (yukariPos - transform.position).normalized;
                    moveDirection = transform.forward = new Vector3(direction.x, 0, direction.z);
                    moveDirection *= speed;
                }
                else if(m_state == CreeperState.Dead)
                {
                    moveDirection.x = 0;
                    moveDirection.z = 0;
                }

                if(m_knockBack)
                {
                    moveDirection *= -0.5f;
                    moveDirection.y = jumpSpeed;
                    m_knockBack = false;
                }
            }
        }

        // Say I'm dead!
        if (m_state == CreeperState.Dead)
        {
            m_destroyTimer += Time.deltaTime;
            if (m_destroyTimer > 1f) Spawner.Instance.MessageDead(this.gameObject);
        }

        // Control hit animation
        if (m_hitAnimationTimer < 0.5f)
        {
            m_hitAnimationTimer += Time.deltaTime;
            m_animator.speed = 5f;
        }
        else if(m_hitAnimationTimer >= 0.5f)
        {
            m_hitAnimationTimer = 0.5f;
            m_animator.speed = 1f;
        }

        // Check whether it is dead
        if(health <= 0)
        {
            m_hitAnimationTimer = 0.5f;
            m_animator.speed = 1f;
            m_animator.SetBool("Dead", true);
            m_state = CreeperState.Dead;
        }

        // This code must be in bottom
        moveDirection.y -= gravity * Time.deltaTime;
        m_controller.Move(moveDirection * Time.deltaTime);
    }

    public void OnHit(GameObject yukari)
    {
        if(m_state == CreeperState.Alive)
        {
            m_knockBack = true;
            if (m_blender != null) m_blender.Blend();
            m_hitAnimationTimer = 0;
            health -= yukari.GetComponent<YukariController>().damage;
        }
    }
}
