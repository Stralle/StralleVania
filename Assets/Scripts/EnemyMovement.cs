using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Serialized fields
    [SerializeField]
    float m_walkingSpeed = 2.0f;

    // Cached references
    Rigidbody2D m_rigidBody;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (IsFacingRight())
        {
            Vector2 playerVelocity = new Vector2(m_walkingSpeed, m_rigidBody.velocity.y);
            m_rigidBody.velocity = playerVelocity;
        }
        else
        {
            Vector2 playerVelocity = new Vector2(-m_walkingSpeed, m_rigidBody.velocity.y);
            m_rigidBody.velocity = playerVelocity;
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(-Mathf.Sign(m_rigidBody.velocity.x), 1f); //Flipping enemy to the oposite side
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
    }
}
