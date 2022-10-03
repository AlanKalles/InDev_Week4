using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveMult;
    Rigidbody2D myBody;
    float horizontalMove;
    float verticalMove;

    private bool isLadder;
    private bool isClimbing;
    public float climbSpeed = 2f;

    bool grounded = false;
    public float castDist = 1f;

    public float jumpLimit = 10f;
    public float gravityScale = 2f;
    public float gravityFall = 5f;

    bool jump = false;
    bool jumping = false;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        //Debug.Log(GetComponent<SpriteRenderer>().size);
        if (Input.GetKeyDown("space") && grounded)
        {
            jump = true;
            jumping = true;
            Debug.Log(jump);
        }

        if (isLadder && Mathf.Abs(verticalMove) > 0f)
        {
            isClimbing = true;
            myAnim.SetBool("isClimb", true);
        }

    }

    private void FixedUpdate()
    {
        HorizontalMove(horizontalMove);

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
            Debug.Log(jump);
        }

        if(myBody.velocity.y >= 0 && jumping == true)
        {
            myBody.gravityScale = gravityScale;
        }else if(myBody.velocity.y < 0 && jumping == true)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        if (hit.collider != null && hit.transform.tag == "platform")
        {
            grounded = true;
            jumping = false;
        }
        else
        {
            grounded = false;
        }

        RaycastHit2D hitLadder = Physics2D.Raycast(transform.position, Vector2.up, castDist);
        Debug.DrawRay(transform.position, Vector2.up * castDist, new Color(255, 0, 0));
        if (hitLadder.collider != null && hitLadder.transform.tag == "Ladder")
        {
            isLadder = true;
        }
        else
        {
            isLadder = false;
            isClimbing = false;
        }

        if (isClimbing)
        {
            myBody.gravityScale = 0f;
            myBody.velocity = new Vector2(myBody.velocity.x, verticalMove * climbSpeed);
            if(Mathf.Abs(verticalMove) == 0f)
            {
                myAnim.speed = 0f;
            }
            else
            {
                myAnim.speed = 1f;
            }
        }
        else if (isClimbing == false)
        {
            myBody.gravityScale = gravityScale;
            myAnim.SetBool("isClimb", false);
        }


    }

    void HorizontalMove(float toMove)
    {
        float moveX = toMove * Time.fixedDeltaTime * moveMult;
        myBody.velocity = new Vector3(moveX, myBody.velocity.y);
        if(myBody.velocity.x > 0 || myBody.velocity.x < 0)
        {
            myAnim.SetBool("isWalking", true);
            if(myBody.velocity.x > 0)
            {
                myAnim.SetBool("isRight", true);
                myAnim.SetBool("isLeft", false);
            }
            else
            {
                myAnim.SetBool("isLeft", true);
                myAnim.SetBool("isRight", false);
            }
        }
        else
        {
            myAnim.SetBool("isWalking", false);
        }
    }

}
