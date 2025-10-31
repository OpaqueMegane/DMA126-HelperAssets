/*
 This is the version modified in class to play animations
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimal2DPlatformerController : MonoBehaviour
{
    public float moveHorizontal;
    private float moveVertical;
    private Vector2 currentVelocity;
    [SerializeField]
    private float movementSpeed = 3f;
    private Rigidbody2D rb;
    private bool isJumping;
    [SerializeField]
    private float jumpSpeed = 15f;
    private bool haveJumped = false;

    public bool grounded = false;

    public LayerMask standableLayers = ~(1 << 6);

    public KeyCode jumpKey = KeyCode.W;

    public float horizontalStopStrength = .5f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.moveHorizontal = Input.GetAxis("Horizontal");
        this.moveVertical = Input.GetAxis("Vertical");
    
        Vector2 finalVelo = rb.velocity;
        
        if (grounded && Input.GetKeyDown(jumpKey)) //<-- this can be tweaked for easier jumping
        {

            finalVelo.y = jumpSpeed;
        }

        if (moveHorizontal == 0)
        {
            finalVelo.x = Mathf.MoveTowards(rb.velocity.x, 0, Time.deltaTime * horizontalStopStrength);
        }

        animator.SetBool("grounded", grounded);

        if (Mathf.Abs(moveHorizontal) > .1f && grounded)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        rb.velocity = finalVelo;
        this.currentVelocity = this.rb.velocity;
    }

    private void FixedUpdate()
    {
        if (this.moveHorizontal != 0)
        {
            this.rb.velocity = new Vector2(this.moveHorizontal * this.movementSpeed, this.currentVelocity.y);
        }

        if (isJumping && !haveJumped)
        {
            this.rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
            this.haveJumped = true;
        }

        GroundCheck();
    }

    float maxSlope = 45;
    void GroundCheck()
    {
        var hit = Physics2D.CapsuleCast(this.transform.position, groundCheckSize, CapsuleDirection2D.Horizontal, 0, Vector2.down,  groundCastDistance, standableLayers);
        if (hit.collider != null)
        {
            grounded = true;
            grounded = Vector2.Angle(Vector2.up, hit.normal) <=maxSlope;
        }
        else
        {
            grounded = false;
        }
    }

    public float groundCastDistance = .5f;
    public Vector2 groundCheckSize = new Vector2(.1f, .1f);
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position + new Vector3((groundCheckSize.x -groundCheckSize.y) * .5f, -groundCastDistance, 0), groundCheckSize.y);
        Gizmos.DrawWireSphere(this.transform.position + new Vector3(-(groundCheckSize.x - groundCheckSize.y) * .5f, -groundCastDistance, 0), groundCheckSize.y);
        Gizmos.DrawLine(
            this.transform.position + new Vector3(-(groundCheckSize.x - groundCheckSize.y) * .5f, -groundCastDistance - groundCheckSize.y, 0), 
            this.transform.position + new Vector3((groundCheckSize.x - groundCheckSize.y) * .5f, -groundCastDistance - groundCheckSize.y, 0));
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    this.isJumping = false;
    //    currentJumpTimer = 0;
    //    //if (collision.gameObject.tag.Equals("Background"))
    //    //{
    //    //    this.isJumping = false;
    //    //}

    //}


}