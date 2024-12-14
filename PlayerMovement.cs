using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 6;
    private float jumpForce = 15;
    private float movementInput;
    private Vector3 startPosition;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody2D rigid;
    private Animator animator;
    private BoxCollider2D boxcol;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxcol = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        // mover al personaje
        rigid.linearVelocity = new Vector2(movementInput * movementSpeed, rigid.linearVelocity.y);

        // animación de caminar
        animator.SetFloat("walk", Mathf.Abs(movementInput));
        animator.SetBool("touchingwall", onWall() && rigid.linearVelocity.y < -2);
        animator.SetBool("grounded", isGrounded());

        // animación caer
        animator.SetBool("falling", !isGrounded() && rigid.linearVelocity.y < -2);
        animator.SetBool("jumping", !isGrounded() && rigid.linearVelocity.y > 0);

        // voltear el personaje dependiendo de la dirección
        if (movementInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // mira a la derecha
        }
        else if (movementInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // mira a la izquierda
        }

        // caer más lento por la pared
        if (onWall() && !isGrounded() && rigid.linearVelocity.y < -0.1f)
        {
           
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, -4);
        }

        // teleport al inicio
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = startPosition;
        }
    }

    private void OnJump()
    {
        if (isGrounded())
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, jumpForce);
        }
    }

    private void OnMovement(InputValue value)
    {
        movementInput = value.Get<float>();
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.2f, wallLayer);
        return raycastHit.collider != null;
    }

    private bool isFalling()
    {
        return !isGrounded() && rigid.linearVelocity.y <= 0;
    }
}
