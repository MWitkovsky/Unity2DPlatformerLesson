using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private float width;
    private float height;

    //animation stuff
    private Animator anim;
    private Vector3 scale;
    private float xScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x * 2.0f;
        height = GetComponent<Collider2D>().bounds.extents.y * 2.0f;
        anim = GetComponent<Animator>();
        scale = transform.localScale;
		xScale = scale.x;
    }

    public void ProcessInput(float horizontal, bool jumpKeyPressed)
    {
        //Horizontal movement
        velocity = rb.velocity;
        velocity.x = horizontal * moveSpeed;
        rb.velocity = velocity;

        //Jump
        if (jumpKeyPressed && CanJump())
        {
            velocity.y = jumpForce;
            rb.velocity = velocity;
            anim.SetBool("land", false);
            anim.SetTrigger("jump");
            anim.SetFloat("jumpSpeed", Mathf.Abs(velocity.x) + 1.0f);
        }
		
        //Anim stuff
        //Movement
		anim.SetFloat("moveSpeed", Mathf.Abs(velocity.x));
        //Flipping facing depending on movement
		if (velocity.x < 0.0f){
            scale.x = -xScale;
			transform.localScale = scale;
		}
		else if (velocity.x > 0.0f){
            scale.x = xScale;
			transform.localScale = scale;
		}
    }

    private bool CanJump()
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(width, height), 0.0f, Vector2.down, 0.1f))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("land", true);
        }
    }
}
