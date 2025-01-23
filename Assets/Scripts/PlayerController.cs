using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; // Componente Rigidbody2D del jugador.

    // RUN
    public float moveSpeed = 5f; // Velocidad de movimiento lateral del jugador.
    private float horizontalInput; // Entrada del usuario para el movimiento horizontal.

    // JUMP
    public float jumpForce = 10f; // Fuerza del salto.
    public int maxJumps = 2; // Máximo número de saltos permitidos.
    public float jumpHoldTime = 0.2f; // Tiempo que puede mantener el salto.
    private int currentJumpCount; // Contador de saltos realizados.
    private float jumpTimer; // Temporizador para mantener el salto.
    private bool isJumping; // Indica si el jugador está en medio de un salto.

    // Settings
    public Transform groundCheck; // Transform para detectar el suelo.
    public float groundCheckRadius = 0.2f; // Radio para detectar el suelo.
    public LayerMask groundLayer; // Capa que define qué es considerado suelo.
    private bool isGrounded; // Indica si el jugador está tocando el suelo.

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround(); // Verifica si el jugador está en el suelo.
        HandleJumpInput(); // Maneja la lógica del salto.
    }

    void FixedUpdate()
    {
        Move(); // Maneja el movimiento horizontal.
    }

    /// Maneja el movimiento horizontal del jugador.
    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Cambiar la dirección del sprite según la entrada.
        if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    /// Detecta si el jugador está tocando el suelo.
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reinicia el contador de saltos al tocar el suelo.
        if (isGrounded)
        {
            currentJumpCount = 0;
            isJumping = false;
        }
    }

    /// Maneja la lógica de entrada y el salto del jugador.
    private void HandleJumpInput()
    {
        // Inicia un salto si el jugador está en el suelo o tiene saltos restantes.
        if (Input.GetKeyDown(KeyCode.W) && (isGrounded || currentJumpCount < maxJumps))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            jumpTimer = jumpHoldTime;
            currentJumpCount++;
        }

        // Mantiene el salto mientras la tecla esté presionada y haya tiempo restante.
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            if (jumpTimer > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Finaliza el salto cuando se suelta la tecla.
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }

    // Dibuja un gizmo para visualizar el área de detección del suelo en la escena.
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}




