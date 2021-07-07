using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpQueenScript : MonoBehaviour
{

    public CharacterController characterController;
    public Animator animator;
    Rigidbody2D rigid;

    float curMoveSpeed = 0;
    //float curJumpSpeed = 0;
    public float mvSpeed = 10f;
    public float jmpSpeed = 10f;
    bool atk = false, canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y + jmpSpeed);
            canJump = false;
        }

        if (Input.GetKey(KeyCode.Tab))
            atk = true;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            curMoveSpeed = -mvSpeed;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                curMoveSpeed = mvSpeed;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else curMoveSpeed = 0;
        }

        // Can't jumping and attacking
        if (atk && rigid.velocity.y == 0)
        {
            animator.Play("Attack");
            atk = false;
        }
        else
        {
            if ((rigid.velocity.x != 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && rigid.velocity.y == 0)
                animator.Play("Run");
            else if (rigid.velocity.y != 0 || Input.GetKey(KeyCode.Space))
                animator.Play("Jump");
            else
            {
                    animator.Play("Idle");
            }
        }
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.name == "Tilemap")
            canJump = true;
        if (target.collider.name == "Monster")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                Destroy(target.gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D target)
    {
        if (target.collider.name == "Monster")
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("atk"))
                Destroy(target.gameObject);
        }
    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(curMoveSpeed, rigid.velocity.y);
    }
}

