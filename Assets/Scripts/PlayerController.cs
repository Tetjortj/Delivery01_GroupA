using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody2D _rb; // Componente Rigidbody2D del jugador para controlar su física.

    // RUN 
    public float speed; // Velocidad de movimiento lateral del jugador.
    private float moveInput; // Entrada del usuario para el movimiento horizontal.

    // JUMP 
    public float jumpForce; // Fuerza del salto del jugador.
    private bool isJumping; // Indica si el jugador está en medio de un salto.
    public float jumpTime; // Duración máxima del salto continuo.
    private float jumpTimeCounter; // Temporizador para medir el tiempo restante del salto.
    public float checkRadius; // Radio para verificar si el jugador está tocando el suelo.
    public Transform feetPos; // Posición de los pies del jugador para la verificación de suelo.
    public LayerMask whatIsGround; // Capa que define qué se considera suelo.
    public int maxJumps = 3; // Número máximo de saltos (puedes ajustarlo según lo que necesites).
    private int currentJumps; // Contador de saltos realizados actualmente.
    public int jumpCount;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // Asigna el componente Rigidbody2D.
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Run(); 
    }

    // Método para manejar el movimiento lateral del jugador.
    private void Run()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); // Obtiene la entrada horizontal del usuario.
        _rb.linearVelocity = new Vector2(moveInput * speed, _rb.linearVelocity.y); // Aplica la velocidad en la dirección horizontal.

        // Cambia la dirección del sprite según la dirección del movimiento.
        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
            // Debug.Log("Izquierda");
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0f, 0);
            // Debug.Log("Derecha");
        }
    }


    // Método para verificar si el jugador está tocando el suelo.
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    // Restablecer el contador de saltos cuando el jugador toca el suelo.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("isGround") || collision.collider.CompareTag("Ground"))
        {
            jumpCount = 0; // Permite el salto normal nuevamente.
            // Debug.Log("El jugador está en el suelo, puede saltar de nuevo.");
        }
    }


  private void Jump()
    {
        // Saltar si el jugador está en el suelo y presiona la tecla de salto (Primer salto).
        if (isGrounded() && Input.GetKeyDown(KeyCode.W) && currentJumps < maxJumps)
        {
            Debug.Log("Está saltando");
            isJumping = true;
            _rb.linearVelocity = Vector2.up * jumpForce; // Aplica la fuerza hacia arriba para el salto
            jumpTimeCounter = jumpTime; // Reinicia el temporizador de salto
            currentJumps++; // Incrementa el contador de saltos
        }

        // Saltar mientras está en el aire (Doble salto, Triple salto, etc.)
        else if (Input.GetKeyDown(KeyCode.W) && currentJumps > 0 && currentJumps < maxJumps)
        {
            Debug.Log("Está realizando un salto adicional");
            _rb.linearVelocity = Vector2.up * jumpForce; // Aplica la fuerza para el salto adicional
            currentJumps++; // Incrementa el contador de saltos
        }

        // Mantiene el salto si la tecla se mantiene presionada y hay tiempo de salto restante.
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            Debug.Log("Está saltando");
            if (jumpTimeCounter > 0)
            {
                _rb.linearVelocity = Vector2.up * jumpForce; // Aplica fuerza continua hacia arriba.
                jumpTimeCounter -= Time.deltaTime; // Reduce el tiempo restante del salto.
            }
            else
            {
                isJumping = false; // Finaliza el salto si se agota el tiempo.
            }
        }

        // Finaliza el salto si el jugador suelta la tecla de salto.
        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
            // Debug.Log("Ya no salta");
        }
    }
}



