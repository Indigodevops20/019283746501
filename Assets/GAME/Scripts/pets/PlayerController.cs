using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer spr;
    Animator anim;
    public PhysicsMaterial2D withFriction;
    public PhysicsMaterial2D noFriction;
    //
    bool facingRight = false;
    //velocidad
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        if (move > 0 && !facingRight)
        {
            Flip();
        }else if (move < 0 && facingRight)
        {
            Flip();
        }

        rb2d.sharedMaterial = (move != 0) ? noFriction : withFriction;

        rb2d.velocity = new Vector2(move * maxSpeed, rb2d.velocity.y);
        anim.SetFloat("moveSpeed", Mathf.Abs(move));
    }

    void Flip()
    {
        facingRight = !facingRight;
        spr.flipX = !spr.flipX;
    }
}
