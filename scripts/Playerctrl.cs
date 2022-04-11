using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Playerctrl : MonoBehaviour
{
    //��ɫ����

    bool m_onFloor;//�ж��Ƿ��ڵ���
    bool m_isWall;//�ж��Ƿ�����ǽ��

    public LayerMask m_floorLayer;//�ذ�㣬���ڼ���ɫ��ǽ�����ײ,layer������Ϊint32�����ڱ�ʾlayer�㼶������ɫ�Լ���Ϊһ���㼶
                                  //�ذ���Ϊ��һ���㼶�������Ϳ��Լ�⵽��ײ���ˣ��������ڱ�����������ʵû����ײ�����Կ��Կ���һ���㼶
    public float m_FloorCheckDistance = 0.4f;//�ذ��жϾ���

    SoundManager m_audio;
    //public AudioClip deathmusic;
    //private AudioSource death_audio;
    //public AudioClip fjumpmusic;
    //private AudioSource fjump_audio;
    //public AudioClip sjumpmusic;
    //private AudioSource sjump_audio;

    public Transform m_footCheck;//�����Ų��ж�,transform�������Ϊ�����������ת�ƶ������ŵ�
    public Transform m_headCheck;//����ͷ���ж�

    public float m_wallCheckDistance = 0.4f;//�ж�������Ϊ0.5f

    private Animator m_anim;//����animator���ͣ����ڸı�����˶��Ķ���״̬
    private Rigidbody2D m_body;//��ȡ������������ڸ����ٶ�����
    private Renderer m_render;//��ȡ��ʾ�����Ϊ��ʵ���޵�����˸Ч��

    bool m_FacingRight = true;//�����ұ�

    public float m_Speed=5f;//�ƶ��ٶ�
    public float m_jumpaltitude = 8.1f;//��Ծ�ٶ�

    public float m_CanJumpTime = 0.2f;//��Ծʱ���޶���������
    private float m_JumpTimer;//��Ծʱ��
    private bool m_isJumping;//�ж���Ծ

    public float blinkTime;//��˸ʱ��
    public int blinkSeconds;//��˸����
    private bool isInvincible=false;//�޵�״̬

    private Vector2 m_vec;//�������
    private float m_input_h;//move�������

    private int m_jumpTimes;//������

    public GameObject pfb_bullet;// �ӵ�����
    protected Vector2 bulletSpeed = new Vector2(15, 0);//�ӵ�����

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

    void FixedUpdate()//ÿ֡�̶����£����������ֵ���ᵼ���ƶ�����������
    {
        m_onFloor = IsFloored();
        

    }
    private void LateUpdate()//�����ٶ�����
    {
        
    }
    private void Update()//���Ч��ִ�и���
    {
        
        
        if(m_anim.GetBool("floor")!=m_onFloor)//�ı�bool���ͣ�ˢ���ж�
        {
            m_anim.SetBool("floor", m_onFloor);
        }
        jump();
        #region ��Ծ

        //��Ծ
        if (m_isJumping && Input.GetButton("Jump"))//��������ʵ�֣�
        {
            if (m_JumpTimer <= m_CanJumpTime)
            {
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;//�̶���Ծ�ٶ�
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

        //�����Ұ���״̬
        m_input_h = Input.GetAxisRaw("Horizontal");//��������ͬ��û�м�rawʱ�ƶ����Ǽ��ٵģ�����0.1-0.3-0.1������raw��ֻ�᷵��1��-1

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
                m_isJumping = true;//��Ծ��
                m_JumpTimer = 0f;//��Ծʱ��
                m_onFloor = false;
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;//�̶���Ծ�ٶ�
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
                m_body.velocity = m_vec;//�̶���Ծ�ٶ�
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
                m_body.velocity = m_vec;//�̶���Ծ�ٶ�
            }
        }
    }

    private void Move(float h)//�˶�����
    {
        m_isWall = IsWalled(m_FacingRight?1:-1);//�����1���Ǿ�true�������-1���Ǿ�false

        if (m_FacingRight)
        {
            if (h < 0)
            {
                Flip();
            }
            else if (m_isWall)//�ı䶯��Ϊ�ܲ�����
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
            else if (m_isWall)//�ı䶯��Ϊ�ܲ�����
            {
                m_anim.SetBool("run", false);
                return;
            }
        }
        Vector2 v = m_body.velocity;
        v.x = h * m_Speed;
        m_body.velocity = v;//��ֵ����

        m_anim.SetBool("run", !(h == 0));//��˼��Ϊ��h=0ʱ����ô��˵����û���ܣ�h��=0ʱ��˵��������
    }

    private void Flip()//ˮƽ��ת������������Է���
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsFloored()//�����жϺ���
    {
        Vector3 leftVec = transform.position;//�������̽��
        leftVec.x = transform.position.x-0.25f;
        Debug.DrawRay(leftVec, Vector2.down, Color.green);//���߼�⹦�ܣ���start��ʼλ�û���һ��color��ɫ���ߣ�����ʱ��Ϊduration�����Ϊ0�߻����һ֡������Ϊ2D���·�
        RaycastHit2D lefthit = Physics2D.Raycast(leftVec, Vector2.down, m_FloorCheckDistance, m_floorLayer);

        Vector3 rightVec = transform.position;//�Ҳ�����̽��
        rightVec.x = transform.position.x + 0.25f;
        Debug.DrawRay(rightVec, Vector2.down, Color.green);//���߼�⹦�ܣ���start��ʼλ�û���һ��color��ɫ���ߣ�����ʱ��Ϊduration�����Ϊ0�߻����һ֡������Ϊ2D���·�
        RaycastHit2D righthit = Physics2D.Raycast(rightVec, Vector2.down, m_FloorCheckDistance, m_floorLayer);

        if (lefthit.collider!=null|| righthit.collider != null)
        {
            m_jumpTimes = 0;
            return true;
        }
        return false;
    }

    private bool IsWalled(float dir)//ǽ���жϺ���
    {                                        //    ��ʼ��λ��            ��������             ���߾���        ̽��ָ��ͼ��
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

    void BeDamaged(int damage)//�ܵ��˺�
    {
        if(isInvincible==false)
        {
            m_HP -= damage;
        }

        if(m_HP<=0)
        {
            //�������
            Destroy(gameObject);
            ui_GameOverImage.SetActive(true);
            SoundManager.instance.pauseLevelAudio();
            SoundManager.instance.DeathAudio();
            //death_audio.Play();
        }
        BlinkPlayer(blinkSeconds,blinkTime);
    }

    void BlinkPlayer(int numBlinks,float seconds)//��˸����
    {
        
        StartCoroutine(DoBlinks(numBlinks,seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)//�޵�ʱ��
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
