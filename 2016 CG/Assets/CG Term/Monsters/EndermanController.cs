using UnityEngine;
using System.Collections;
using System;

public enum EndermanState
{
    Alive,
    Dead
}

public class EndermanController : MonoBehaviour, Shootable
{

    public float gravity = 20f;
    public float speed = 2f;
    public float jumpSpeed = 5f;
    public float health = 100f;
    public GameObject yukari;

    private EndermanState m_state;

    //private Endermannodes m_nodes;

    private CharacterController m_controller;
    private Animator m_animator;
    private Vector3 moveDirection = Vector3.zero;
    private bool m_knockBack = false;
    private MonsterColorBlend m_blender = null;
    private float m_hitAnimationTimer = 0.5f;

    // Use this for initialization
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        m_blender = GetComponent<MonsterColorBlend>();
        m_animator = GetComponent<Animator>();

        m_state = EndermanState.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (yukari != null)
        {
            if (m_controller.isGrounded)
            {

                
                if (m_knockBack)
                {
                    
                }
            }
        }

        // Control hit animation
        if (m_hitAnimationTimer < 0.5f)
        {
            m_hitAnimationTimer += Time.deltaTime;
            m_animator.speed = 5f;
        }
        else if (m_hitAnimationTimer >= 0.5f)
        {
            m_hitAnimationTimer = 0.5f;
            m_animator.speed = 1f;
        }

        // Check whether it is dead
        if (health <= 0)
        {
            m_hitAnimationTimer = 0.5f;
            m_animator.speed = 1f;
            m_animator.SetBool("Dead", true);
            m_state = EndermanState.Dead;
        }

        // 이 코드는 가장 아래에 있어야 한다
        moveDirection.y -= gravity * Time.deltaTime;
        m_controller.Move(moveDirection * Time.deltaTime);
    }

    public void OnHit(GameObject yukari)
    {
        m_knockBack = true;
        if (m_blender != null) m_blender.Blend();
        m_hitAnimationTimer = 0;
        health -= yukari.GetComponent<YukariController>().damage;
    }
}
