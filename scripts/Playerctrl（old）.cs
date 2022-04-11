using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //判定人物运动状态以及碰撞
    Transform m_FloorCheck;//创建地面判定,transform类的作用为用于物体的旋转移动和缩放等
    public float m_FloorRadius = 0.035f;//判定距离半径大概为0.5f
    bool m_onFloor;//判断是否处于地面
    public LayerMask m_whereFloor;//地板层，用于检测角色与墙体的碰撞,layer的类型为int32，用于表示layer层级，将角色自己设为一个层级
                                  //地板设为另一个层级，这样就可以检测到碰撞体了，并且由于背景与人物其实没有碰撞，所以可以看作一个层级

    Animator m_anim;//创建animator类型，用于改变玩家运动的动画状态
    Rigidbody2D m_body;//获取刚体组件，便于给予速度与力

    bool m_FacingRight = true;

    public float m_Speed = 200f;
    public float m_jumpaltitude = 300f;

    bool m_jump;
    int m_jumps;

    // Start is called before the first frame update
    void Start()
    {
        m_jump = false;
        m_FloorCheck = transform.Find("FloorCheck");//开始游戏时获取地面位置

        m_anim = GetComponent<Animator>();//获取玩家的动画状态
        m_body = GetComponent<Rigidbody2D>();//获取玩家的刚体位置状态
        m_jumps = 0;

    }

    private void FixedUpdate()//每帧固定更新
    {
        m_onFloor = false;//开始检测没有碰撞到地面进行下坠
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_FloorCheck.position, m_FloorRadius, m_whereFloor);//圆的中心，半径，碰撞对象，构造2D数组信息
        //检测某个碰撞体是不是在某个圆形区域内,返回碰撞体，如果有多个，返回z轴最小的碰撞体，如果没有返回false
        foreach (Collider2D collider in colliders)//foreach语句，如果返回值为false就跳过循环
        {
            m_onFloor = true;
            m_jumps = 0;
        }
        if (m_anim.GetBool("floor") != m_onFloor)//改变bool类型，刷新判断
        {
            m_anim.SetBool("floor", m_onFloor);
        }
        if (m_body.velocity.y == 0)//用于动画转变
        {
            m_anim.SetFloat("hSpeed", 0);
        }
        else
        {
            m_anim.SetFloat("hSpeed", m_body.velocity.y / Mathf.Abs(m_body.velocity.y));//赋予玩家目前刚体速度的纵向值，如果为1那就是跳跃动画，如果为-1就是下落动画
                                                                                        //意为绝对值，这样输出数只有1或-1
        }


        //检测玩家按键状态
        float h = Input.GetAxis("Horizontal");

        m_jump = Input.GetButtonDown("Jump");



        Move(h, m_jump);

    }

    private void Move(float h, bool jump)//运动函数，h为判断方向，bool为判断跳跃
    {
        Vector2 v = m_body.velocity;

        if (h > 0)
        {
            v.x = h / Mathf.Abs(h) * m_Speed * Time.deltaTime;
            if (!m_FacingRight)//翻转动画
            {
                Flip();
            }

            if (!m_anim.GetBool("run"))//改变动画为跑步动画
            {
                m_anim.SetBool("run", true);
            }
        }

        else if (h < 0)
        {
            v.x = h / Mathf.Abs(h) * m_Speed * Time.deltaTime;
            if (m_FacingRight)
            {
                Flip();
            }


            if (!m_anim.GetBool("run"))
            {
                m_anim.SetBool("run", true);
            }
        }

        else
        {
            v.x = 0;

            if (m_anim.GetBool("run"))//改变动画为跑步动画
            {
                m_anim.SetBool("run", false);
            }
        }

        if (jump)//跳跃力的给予
        {
            if (m_jumps <= 1)//接受跳跃以及二段跳的操作
            {
                v.y = m_jumpaltitude * Time.deltaTime;
            }
            m_jumps++;
        }

        m_body.velocity = v;//velocity获得当前刚体速度 
    }

    private void Flip()//水平翻转函数，人物面对方向
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
