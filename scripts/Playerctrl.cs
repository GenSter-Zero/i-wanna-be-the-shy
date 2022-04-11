using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Playerctrl : MonoBehaviour
{
    //角色代码

    bool m_onFloor;//判断是否处于地面
    bool m_isWall;//判断是否属于墙面

    public LayerMask m_floorLayer;//地板层，用于检测角色与墙体的碰撞,layer的类型为int32，用于表示layer层级，将角色自己设为一个层级
                                  //地板设为另一个层级，这样就可以检测到碰撞体了，并且由于背景与人物其实没有碰撞，所以可以看作一个层级
    public float m_FloorCheckDistance = 0.4f;//地板判断距离

    SoundManager m_audio;
    //public AudioClip deathmusic;
    //private AudioSource death_audio;
    //public AudioClip fjumpmusic;
    //private AudioSource fjump_audio;
    //public AudioClip sjumpmusic;
    //private AudioSource sjump_audio;

    public Transform m_footCheck;//创建脚部判定,transform类的作用为用于物体的旋转移动和缩放等
    public Transform m_headCheck;//创建头部判定

    public float m_wallCheckDistance = 0.4f;//判定距离大概为0.5f

    private Animator m_anim;//创建animator类型，用于改变玩家运动的动画状态
    private Rigidbody2D m_body;//获取刚体组件，便于给予速度与力
    private Renderer m_render;//获取显示组件，为了实现无敌与闪烁效果

    bool m_FacingRight = true;//面向右边

    public float m_Speed=5f;//移动速度
    public float m_jumpaltitude = 8.1f;//跳跃速度

    public float m_CanJumpTime = 0.2f;//跳跃时间限定（大跳）
    private float m_JumpTimer;//跳跃时间
    private bool m_isJumping;//判断跳跃

    public float blinkTime;//闪烁时间
    public int blinkSeconds;//闪烁次数
    private bool isInvincible=false;//无敌状态

    private Vector2 m_vec;//玩家向量
    private float m_input_h;//move所需变量

    private int m_jumpTimes;//二段跳

    public GameObject pfb_bullet;// 子弹构造
    protected Vector2 bulletSpeed = new Vector2(15, 0);//子弹动能

    private void Awake()
    {
        //death_audio = ui_GameOverImage.gameObject.AddComponent<AudioSource>();
        //death_audio.clip = deathmusic;
        //fjump_audio = gameObject.gameObject.AddComponent<AudioSource>();
        //fjump_audio.clip = fjumpmusic;
        //sjump_audio = gameObject.gameObject.AddComponent<AudioSource>();
        //sjump_audio.clip = sjumpmusic;
        m_audio = gameObject.GetComponent<SoundManager>();
        m_anim = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();
        m_render = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        m_JumpTimer = 0f;
        m_isJumping = false;
        m_vec =new Vector2(0,m_jumpaltitude);
        m_jumpTimes = 0;
    }

    void FixedUpdate()//每帧固定更新（可设置最大值，会导致移动代码跳过）
    {
        m_onFloor = IsFloored();
        

    }
    private void LateUpdate()//更新速度最慢
    {
        
    }
    private void Update()//最大效率执行更新
    {
        
        
        if(m_anim.GetBool("floor")!=m_onFloor)//改变bool类型，刷新判断
        {
            m_anim.SetBool("floor", m_onFloor);
        }
        jump();
        #region 跳跃

        //跳跃
        if (m_isJumping && Input.GetButton("Jump"))//（大跳的实现）
        {
            if (m_JumpTimer <= m_CanJumpTime)
            {
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;//固定跳跃速度
                m_JumpTimer += Time.deltaTime;
            }
            else
            {
                m_isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            m_isJumping = false;
        }

        m_anim.SetFloat("hSpeed", m_body.velocity.y);

        #endregion

        //检测玩家按键状态
        m_input_h = Input.GetAxisRaw("Horizontal");//与以往不同，没有加raw时移动过是加速的，类似0.1-0.3-0.1，加了raw就只会返回1和-1

        Move(m_input_h);

        if(Input.GetButtonDown("shoot"))
        {
            GameObject obj = Instantiate(pfb_bullet, transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody2D>().velocity = m_FacingRight ? bulletSpeed : -1 * bulletSpeed;
        }
    }

    void jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (m_onFloor)
            {
                //fjump_audio.Play();
                SoundManager.instance.fJumpAudio();
                m_jumpTimes = 1;
                m_isJumping = true;//跳跃中
                m_JumpTimer = 0f;//跳跃时间
                m_onFloor = false;
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;//固定跳跃速度
            }
            else if (m_jumpTimes == 1)
            {
                //sjump_audio.Play();
                SoundManager.instance.sJumpAudio();
                m_jumpTimes = 2;
                m_isJumping = true;
                m_JumpTimer = 0f;
                m_onFloor = false;
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;//固定跳跃速度
            }
            else if (m_jumpTimes == 0 && !m_onFloor)
            {
                //sjump_audio.Play();
                SoundManager.instance.sJumpAudio();
                m_jumpTimes = 2;
                m_isJumping = true;
                m_JumpTimer = 0f;
                m_onFloor = false;
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;//固定跳跃速度
            }
        }
    }

    private void Move(float h)//运动函数
    {
        m_isWall = IsWalled(m_FacingRight?1:-1);//如果是1，那就true，如果是-1，那就false

        if (m_FacingRight)
        {
            if (h < 0)
            {
                Flip();
            }
            else if (m_isWall)//改变动画为跑步动画
            {
                m_anim.SetBool("run", false);
                return;
            }
        }
        else
        {
            if (h > 0)
            {
                Flip(); 
            }
            else if (m_isWall)//改变动画为跑步动画
            {
                m_anim.SetBool("run", false);
                return;
            }
        }
        Vector2 v = m_body.velocity;
        v.x = h * m_Speed;
        m_body.velocity = v;//赋值返回

        m_anim.SetBool("run", !(h == 0));//意思即为，h=0时，那么就说明我没在跑，h！=0时，说明我在跑
    }

    private void Flip()//水平翻转函数，人物面对方向
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsFloored()//地面判断函数
    {
        Vector3 leftVec = transform.position;//左侧射线探测
        leftVec.x = transform.position.x-0.25f;
        Debug.DrawRay(leftVec, Vector2.down, Color.green);//射线检测功能，从start起始位置绘制一条color颜色的线，持续时间为duration，如果为0线会出现一帧，方向为2D的下方
        RaycastHit2D lefthit = Physics2D.Raycast(leftVec, Vector2.down, m_FloorCheckDistance, m_floorLayer);

        Vector3 rightVec = transform.position;//右侧射线探测
        rightVec.x = transform.position.x + 0.25f;
        Debug.DrawRay(rightVec, Vector2.down, Color.green);//射线检测功能，从start起始位置绘制一条color颜色的线，持续时间为duration，如果为0线会出现一帧，方向为2D的下方
        RaycastHit2D righthit = Physics2D.Raycast(rightVec, Vector2.down, m_FloorCheckDistance, m_floorLayer);

        if (lefthit.collider!=null|| righthit.collider != null)
        {
            m_jumpTimes = 0;
            return true;
        }
        return false;
    }

    private bool IsWalled(float dir)//墙体判断函数
    {                                        //    起始点位置            方向向量             光线距离        探测指定图层
        RaycastHit2D hit1 = Physics2D.Raycast(m_headCheck.position, dir * Vector2.right, m_wallCheckDistance, m_floorLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(m_footCheck.position, dir * Vector2.right, m_wallCheckDistance, m_floorLayer);
        if((hit1.collider==null)&&(hit2.collider==null))
        {
           
            return false;
        }
        return true;
    }

    public int m_HP = 30;
    public GameObject ui_GameOverImage;

    void BeDamaged(int damage)//受到伤害
    {
        if(isInvincible==false)
        {
            m_HP -= damage;
        }

        if(m_HP<=0)
        {
            //玩家死亡
            Destroy(gameObject);
            ui_GameOverImage.SetActive(true);
            SoundManager.instance.pauseLevelAudio();
            SoundManager.instance.DeathAudio();
            //death_audio.Play();
        }
        BlinkPlayer(blinkSeconds,blinkTime);
    }

    void BlinkPlayer(int numBlinks,float seconds)//闪烁操作
    {
        
        StartCoroutine(DoBlinks(numBlinks,seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)//无敌时间
    {
        isInvincible = true; 
        for (int i =0; i< numBlinks * 2; i++)
        {
            m_render.enabled = !m_render.enabled;
            yield return new WaitForSeconds(seconds);
        }
        m_render.enabled = true;
        isInvincible = false;
    }
}
