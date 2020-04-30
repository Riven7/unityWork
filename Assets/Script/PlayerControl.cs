using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveForce = 400f;
    public float maxSpeed = 5f;

    [HideInInspector]   //隐藏接下来定义的第一个变量
    public bool faceRight = true;

    public float jumpForce = 500;
    [HideInInspector]
    public bool jump = false; //为false代表当前不能跳跃
    [HideInInspector]
    public bool grounded = false; //判断人物是否在地上

    private Rigidbody2D heroBody;
    private Transform groundCheck;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");//找到GroundCheck赋给变量
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");//获取水平方向的输入（左-1或右1）

        //判断是否超过最大速度
        if (h * heroBody.velocity.x < maxSpeed)
            heroBody.AddForce(Vector2.right * h * moveForce);//施加向右×h方向的力，大小为moveForce
        if (Mathf.Abs(heroBody.velocity.x) > maxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * maxSpeed, heroBody.velocity.y);

        anim.SetFloat("speed", Mathf.Abs(h));   //speed大于1则会进行run的动画

        //改变人物朝向
        if (h > 0 && !faceRight)
            flip();
        if (h < 0 && faceRight)
            flip();
        if (jump)
        {
            anim.SetTrigger("Jump");
            heroBody.AddForce(new Vector2(0, jumpForce));//加力二维向量，水平0，垂直jumpForce（正值向上）
            jump = false; //不能再跳了   (力应该系统设置的有持续时间吧提供一小段就不提供了，可以维持一下运动)
        }
    }
    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position
                                        , 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    void flip()
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
