using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D _rb;

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

  // DOUBLE JUMP
  private bool canDoubleJump; 

  void Start()
  {
    _rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    HandleJump();
  }

  private void FixedUpdate()
  {
    HandleRun(); 
  }

    private void HandleRun()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); 
        _rb.linearVelocity = new Vector2(moveInput * speed, _rb.linearVelocity.y); 

        // Ajustar rotación del personaje según dirección.
        if (moveInput != 0)
        {
            transform.eulerAngles = moveInput > 0 ? new Vector3(0, 180f, 0) : Vector3.zero;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    private void HandleJump()
    {
        // Si el jugador está en el suelo, puede reiniciar el salto y el doble salto.
        if (IsGrounded())
        {
            canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.W))
            {
                StartJump();
            }
        }

        // Control del salto inicial o doble salto prolongado.
        if (Input.GetKey(KeyCode.W) && isJumping)
        {
            ContinueJump();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        // Si no está en el suelo y puede hacer doble salto, lo realiza.
        if (canDoubleJump && !IsGrounded() && Input.GetKeyDown(KeyCode.W))
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



