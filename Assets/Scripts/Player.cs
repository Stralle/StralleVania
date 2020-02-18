using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Serialized fields
    [SerializeField]
    float m_runSpeed = 5.0f;
    [SerializeField]
    float m_jumpSpeed = 10.0f;
    [SerializeField]
    float m_climbSpeed = 5.0f;
    [SerializeField]
    Vector2 m_deathKick = new Vector2(10.0f, 20.0f);
    [SerializeField]
    Vector2 m_beginningPosition = new Vector2(-6.84f, 1.56f);

    // Cached components references
    Rigidbody2D m_rigidBody;
    Animator m_animator;
    CapsuleCollider2D m_bodyCollider;
    BoxCollider2D m_feetCollider;
    float m_gravityScaleAtStart;
    
    // States
    private bool m_isRunning = false;
    private bool m_isClimbing = false;
    private bool m_isAlive = true;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_bodyCollider = GetComponent<CapsuleCollider2D>();
        m_feetCollider = GetComponent<BoxCollider2D>();
        m_gravityScaleAtStart = m_rigidBody.gravityScale;
    }

    void Update()
    {
        if (m_isAlive)
        {
            Run();
            Jump();
            ClimbLadder();
            FlipSprite();
            ChangeAnimations();
            Die();
        }
        Reset();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * m_runSpeed, m_rigidBody.velocity.y);
        m_rigidBody.velocity = playerVelocity;
    }
    private void ClimbLadder()
    {
        if (m_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(m_rigidBody.velocity.x, controlThrow * m_climbSpeed);
            m_rigidBody.velocity = playerVelocity;
            m_rigidBody.gravityScale = 0.0f;

            bool playerHasVerticalSpeed = Mathf.Abs(m_rigidBody.velocity.y) > Mathf.Epsilon;
            m_isClimbing = playerHasVerticalSpeed;
        }
        else
        {
            m_isClimbing = false;
            m_rigidBody.gravityScale = m_gravityScaleAtStart;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && m_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, m_jumpSpeed);
            m_rigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(m_rigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(m_rigidBody.velocity.x), 1f); // Changing facing side with correct input
            m_isRunning = true;
        }
        else
        {
            m_isRunning = false;
        }
    }

    private void ChangeAnimations()
    {
        m_animator.SetBool("Climbing", m_isClimbing);
        m_animator.SetBool("Running", m_isRunning);
    }

    private void Die()
    {
        bool canIDie = m_bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes"));

        if (canIDie)
        {
            Debug.Log("Dies");
            m_rigidBody.velocity = m_deathKick;
            transform.localScale = new Vector2(transform.localScale.x * 4, transform.localScale.y * 4);
            m_animator.SetBool("Dead", true);
            m_isAlive = false;
        }
    }

    private void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = m_beginningPosition;
            transform.localScale = new Vector2(1f, 1f);
            m_rigidBody.velocity = new Vector2(0f, 0f);
            m_isAlive = true;
            m_animator.SetBool("Dead", false);
        }
    }
}
