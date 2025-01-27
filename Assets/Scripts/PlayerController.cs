using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    
    // RUN 
    public float speed; 
    private float moveInput; 

    // JUMP 
    public float jumpForce; 
    private bool isJumping; 
    public float jumpTime; 
    private float jumpTimeCounter;
    public float checkRadius; 
    public Transform feetPos; 
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;


    // DOUBLE JUMP
    private bool canDoubleJump;

   
    //CODE
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
       
    }

    void Update()
    {
        HandleJump();
        
    }

    private void FixedUpdate()
    {
        HandleRun(); 
    }

    // METODOS REUTILIZABLES 
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    private bool IsTouchingWall()
    {
        float rayDistance = 0.5f;
        Vector2 origin = transform.position;

        bool isTouchingRight = Physics2D.Raycast(origin, Vector2.right, rayDistance, whatIsWall) ||
                               Physics2D.Raycast(origin + Vector2.up * 0.5f, Vector2.right, rayDistance, whatIsWall) ||
                               Physics2D.Raycast(origin + Vector2.down * 0.5f, Vector2.right, rayDistance, whatIsWall);

        bool isTouchingLeft = Physics2D.Raycast(origin, Vector2.left, rayDistance, whatIsWall) ||
                              Physics2D.Raycast(origin + Vector2.up * 0.5f, Vector2.left, rayDistance, whatIsWall) ||
                              Physics2D.Raycast(origin + Vector2.down * 0.5f, Vector2.left, rayDistance, whatIsWall);

        // Dibuja los rayos para depuración
        Debug.DrawRay(origin, Vector2.right * rayDistance, Color.red);
        Debug.DrawRay(origin + Vector2.up * 0.5f, Vector2.right * rayDistance, Color.red);
        Debug.DrawRay(origin + Vector2.down * 0.5f, Vector2.right * rayDistance, Color.red);

        Debug.DrawRay(origin, Vector2.left * rayDistance, Color.blue);
        Debug.DrawRay(origin + Vector2.up * 0.5f, Vector2.left * rayDistance, Color.blue);
        Debug.DrawRay(origin + Vector2.down * 0.5f, Vector2.left * rayDistance, Color.blue);

        return isTouchingRight || isTouchingLeft;
    }


    private bool IsTouchingWallOnRight()
    {
        // Lanza un Raycast hacia la derecha
        Vector2 origin = transform.position;
        float rayDistance = 0.5f;
        return Physics2D.Raycast(origin, Vector2.right, rayDistance, whatIsWall);
    }

    private bool IsTouchingWallOnLeft()
    {
        // Lanza un Raycast hacia la izquierda
        Vector2 origin = transform.position;
        float rayDistance = 0.5f;
        return Physics2D.Raycast(origin, Vector2.left, rayDistance, whatIsWall);
    }

    private void HandleRun()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); 
        _rb.linearVelocity = new Vector2(moveInput * speed, _rb.linearVelocity.y);

        // Detecta si el personaje está tocando una pared
        if (IsTouchingWall() && !IsGrounded())
        {
            // Si el personaje está tocando una pared y moviéndose hacia ella, cancela el movimiento
            if ((moveInput > 0 && IsTouchingWallOnRight()) || (moveInput < 0 && IsTouchingWallOnLeft()))
            {
                moveInput = 0; // Anula el movimiento hacia la pared
            }
        }

        // Aplica la velocidad al personaje
        _rb.linearVelocity = new Vector2(moveInput * speed, _rb.linearVelocity.y);

        // Ajustar rotación del personaje según dirección.
        if (moveInput != 0)
        {
            transform.eulerAngles = moveInput > 0 ? Vector3.zero : new Vector3(0, 180f, 0);
        }
        if (moveInput != 0 && IsGrounded()) {
            _anim.SetBool("isRunning", true);
        } else {
            _anim.SetBool("isRunning", false);
        }
    }

    private void HandleJump()
    {
        _anim.SetBool("isJump", !IsGrounded());
        _anim.SetFloat("YVelocity", _rb.linearVelocity.y);

        // Si el jugador está en el suelo, puede reiniciar el salto y el doble salto.
        if (IsGrounded())
        {
            canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartJump();
            }
           
        }

        // Control del salto inicial o doble salto prolongado.
        if ((Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) && isJumping)
        {
            ContinueJump();
        }

        if (Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = false;
        }

        // Si no está en el suelo y puede hacer doble salto, lo realiza.
        if (canDoubleJump && !IsGrounded() && (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow)))
        {
            canDoubleJump = false;
            StartJump();
        }
    }

    private void StartJump()
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        _rb.linearVelocity = Vector2.up * jumpForce;
    }

    private void ContinueJump()
    {
        if (jumpTimeCounter > 0)
        {
            _rb.linearVelocity = Vector2.up * jumpForce;
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
    }
}



