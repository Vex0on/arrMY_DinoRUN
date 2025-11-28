using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public float JumpForce = 12f;

    [Header("Score")]
    public TMP_Text ScoreTxt;
    public float scoreMultiplier = 4f;

    private float score = 0f;
    private float nextScoreUpdate = 0f;

    bool isGrounded = false;
    bool isAlive = true; 

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (!isAlive)
            return;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
                JumpRequested = true;
        }

        score += Time.deltaTime * scoreMultiplier;
        if (Time.time >= nextScoreUpdate)
        {
            ScoreTxt.text = $"SCORE : {score:F0}";
            nextScoreUpdate = Time.time + 0.1f;
        }
    }

    private bool JumpRequested = false;

    private void FixedUpdate()
    {
        if (JumpRequested)
        {
            JumpRequested = false;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive)
            return;

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
    }
}
