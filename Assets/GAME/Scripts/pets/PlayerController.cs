using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spr;
    Animator anim;
    //con fricción hará que el Circle collider del jugador no se deslice para nada
    public PhysicsMaterial2D withFriction;
    //sin fricción hará que el Circle collider del jugador se pueda deslizar
    public PhysicsMaterial2D noFriction;

    //Mirando a la derecha
    bool facingRight = false;
    //velocidad
    public float maxSpeed;
    //Suelo
    public bool isGround;
    public LayerMask groundLayer;
    //Salto
    public float jumpPower;
    //Joystick
    public Joystick_move jm;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        //ejecutamos el movimiento
        playerMove();
        //ejecutamos el salto
        playerJump();

    }

    void Flip()
    {
        //hacemos que el sprite y la variable facingRight cambien a true o false
        facingRight = !facingRight;
        spr.flipX = !spr.flipX;
    }
    void playerMove()
    {
        //PC
        //float move = Input.GetAxis("Horizontal");
        //Android
        //el movimiento se igualara al movimiento que generará el script joystick_move
        float move = jm.Horizontal();

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        rb2d.sharedMaterial = (move != 0) ? noFriction : withFriction;

        //la velocidad del pet se igualara al movimiento del joystick x la variable velocidad maxima.
        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
        //convierte el flotante en un numero absoluto(no dara numeros negativos, solo positivos) para que se lo devuelva a el parametro "moveSpeed" de la animación
        anim.SetFloat("moveSpeed", Mathf.Abs(move));
    }
    void playerJump()
    {
        //El Raycast sirve para detectar el suelo

        //posicion del pet para el Raycast
        Vector2 position = transform.position;
        //la dirección hacia donde se dibujara el Raycast
        Vector2 direction = new Vector2(0, -0.4f);
        float distance = 0.4f;

        //Se dibuja el Raycast
        Debug.DrawRay(position, direction, Color.green);
        //Se crea el Raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        //si el collider del Raycast es null isGround será verdadero de lo contrario será falso
        isGround = (hit.collider != null) ? true : false;

        if (isGround && jm.Vertical() > 0.5f)
        {
            //le quita la "velocidad" a la y por si la tiene
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            //le agrega la fuerza al pet
            rb2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        else
        {
            return;
        }
    }
}
