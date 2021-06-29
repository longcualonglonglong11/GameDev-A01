using UnityEngine;

public class characterScript : MonoBehaviour
{
    [SerializeField] Transform groundCollider;
    [SerializeField] LayerMask groundLayer;

    bool SpaceKeyPressed = false;
    bool TabKeyPressed = false;
    bool isGrounded;
    public Vector2 movement;

    public CharacterController characterController;
    public Animator animator;

    Rigidbody2D rigid;

    float curMoveSpeed = 0;
    float curJumpSpeed = 0;
    public float mvSpeed = 20f;
    public float mvJmp = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else { animator.SetBool("isJumping", true); }
        animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")* mvSpeed * Time.deltaTime));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceKeyPressed = true;
            //Debug.Log("Jump");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.Play("atk");
            TabKeyPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curMoveSpeed = -mvSpeed;
            //animator.Play("run");
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                curMoveSpeed = mvSpeed;
                //animator.Play("run");
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        if (SpaceKeyPressed && isGrounded)
        {
            curJumpSpeed = mvJmp;
            rigid.AddForce(new Vector2(Input.GetAxis("Horizontal"), mvJmp));
            curJumpSpeed = 0;
            SpaceKeyPressed = false;
        }
        if (TabKeyPressed)
        {
            //empty
            TabKeyPressed = false;
        }
        else
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Debug.Log("XForce:" + Input.GetAxis("Horizontal") * mvSpeed* Time.deltaTime + " Velocity=" + rigid.velocity.x
                + " Time:" + Time.fixedDeltaTime);
            moveCharacter(movement);
        }
    }
    bool GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCollider.position, 
            new Vector2(0.1f, 0.1f), 0f, groundLayer);
        if (colliders.Length == 0)
        {
            return false;
        }
        else { return true; }
    }
    void moveCharacter(Vector2 direction) //for making character move left and right
    {
        Vector2 Force = direction * mvSpeed * Time.fixedDeltaTime;
        rigid.AddForce(Force);
    }
}
