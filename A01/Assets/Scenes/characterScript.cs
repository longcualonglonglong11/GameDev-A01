using UnityEngine;

public class characterScript : MonoBehaviour
{

    public CharacterController characterController;
    public Animator animator;

    Rigidbody2D rigid;

    float curMoveSpeed = 0;
    float curJumpSpeed = 0;
    public float mvSpeed = 10f;
    public float mvJmp = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("jump");
            curJumpSpeed = mvJmp;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.Play("atk");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curMoveSpeed = -mvSpeed;
            animator.Play("run");
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                curMoveSpeed = mvSpeed;
                animator.Play("run");
                GetComponent<SpriteRenderer>().flipX = true;
            }
            

        }

        rigid.AddForce(new Vector2(curMoveSpeed, curJumpSpeed));
        if (curMoveSpeed > 0)
        {
            curMoveSpeed -= 0.02f;

        }
        else if (curMoveSpeed < 0)
        {
            curMoveSpeed += 0.02f;

        }
        if (curJumpSpeed > 0)
        {
            curJumpSpeed -= 0.04f;

        }
        /*        rigid.velocity = new Vector2(curMoveSpeed, curJumpSpeed);
        curMoveSpeed = 0;
*/
    }
}
