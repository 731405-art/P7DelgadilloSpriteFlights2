using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float thrustForce = 1f;

    public GameObject boosterFlame;
    
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiplier = 10f;

    public UIDocument uiDocument;
    private Label scoreText;

    Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        
        MovePlayer();

        elapsedTime += Time.deltaTime;
        Debug.Log("Score: " + score);
        scoreText.text = "Score: " + score;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);

        if (Mouse.current.leftButton.isPressed)
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                boosterFlame.SetActive(true);
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                boosterFlame.SetActive(false);
            }

        {
            // Calculate mouse direction
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            // Move player in direction of mouse
            transform.up = direction;
            rb.AddForce(direction * thrustForce);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colllision)
    {
        Destroy(gameObject);
    }
}

