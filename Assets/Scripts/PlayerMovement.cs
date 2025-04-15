using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Pobierz input z klawiatury
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalizuj wektor, aby ruch po skosie nie by� szybszy
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Ustaw pr�dko�� Rigidbody
        rb.linearVelocity = movement * moveSpeed;
    }
}