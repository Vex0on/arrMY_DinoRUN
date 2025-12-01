using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float JumpForce = 12f;
    public float slideDuration = 0.4f;

    [Header("Score")]
    public TMP_Text ScoreTxt;
    public float scoreMultiplier = 4f;

    private float score = 0f;
    private float nextScoreUpdate = 0f;

    private bool isGrounded = false;
    private bool isAlive = true;
    private bool JumpRequested = false;
    private bool SlideRequested = false;
    private bool isSliding = false;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Vector2 originalColliderSize;
    private Vector2 slideColliderSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<CapsuleCollider2D>();

        originalColliderSize = col.size;
        slideColliderSize = new Vector2(col.size.x, col.size.y * 0.45f);
    }

    private void Update()
    {

        if (!isAlive)
            return;
        // ----------------- SKOK -----------------
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
                JumpRequested = true;
        }

        // ----------------- WŒLIZG -----------------
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
        {
            SlideRequested = true;
        }

        // ----------------- WYNIK -----------------
        score += Time.deltaTime * scoreMultiplier;
        if (Time.time >= nextScoreUpdate)
        {
            ScoreTxt.text = $"SCORE: {score:F0}";
            nextScoreUpdate = Time.time + 0.1f;
        }
    }

    private void FixedUpdate()
    {
        if (JumpRequested)
        {
            JumpRequested = false;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            if (isSliding)
            {
                stopSlide();
            }
        }

        if (SlideRequested)
        {
            SlideRequested = false;
            startSlide();
        }
    }

    private void startSlide()
    {
        isSliding = true;
        col.size = slideColliderSize;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -4f);
        Invoke(nameof(stopSlide), slideDuration);
    }

    private void stopSlide()
    {
        if (!isSliding) return;

        isSliding = false;
        col.size = originalColliderSize;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) return;

        if (collision.gameObject.CompareTag("ground"))
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Time.timeScale = 0f;

        MainMenu.Instance?.ShowTryAgain();
        MainMenu.Instance?.ShowFinalScore(score);
    }
}
