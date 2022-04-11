using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //�ж������˶�״̬�Լ���ײ
    Transform m_FloorCheck;//���������ж�,transform�������Ϊ�����������ת�ƶ������ŵ�
    public float m_FloorRadius = 0.035f;//�ж�����뾶���Ϊ0.5f
    bool m_onFloor;//�ж��Ƿ��ڵ���
    public LayerMask m_whereFloor;//�ذ�㣬���ڼ���ɫ��ǽ�����ײ,layer������Ϊint32�����ڱ�ʾlayer�㼶������ɫ�Լ���Ϊһ���㼶
                                  //�ذ���Ϊ��һ���㼶�������Ϳ��Լ�⵽��ײ���ˣ��������ڱ�����������ʵû����ײ�����Կ��Կ���һ���㼶

    Animator m_anim;//����animator���ͣ����ڸı�����˶��Ķ���״̬
    Rigidbody2D m_body;//��ȡ������������ڸ����ٶ�����

    bool m_FacingRight = true;

    public float m_Speed = 200f;
    public float m_jumpaltitude = 300f;

    bool m_jump;
    int m_jumps;

    // Start is called before the first frame update
    void Start()
    {
        m_jump = false;
        m_FloorCheck = transform.Find("FloorCheck");//��ʼ��Ϸʱ��ȡ����λ��

        m_anim = GetComponent<Animator>();//��ȡ��ҵĶ���״̬
        m_body = GetComponent<Rigidbody2D>();//��ȡ��ҵĸ���λ��״̬
        m_jumps = 0;

    }

    private void FixedUpdate()//ÿ֡�̶�����
    {
        m_onFloor = false;//��ʼ���û����ײ�����������׹
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_FloorCheck.position, m_FloorRadius, m_whereFloor);//Բ�����ģ��뾶����ײ���󣬹���2D������Ϣ
        //���ĳ����ײ���ǲ�����ĳ��Բ��������,������ײ�壬����ж��������z����С����ײ�壬���û�з���false
        foreach (Collider2D collider in colliders)//foreach��䣬�������ֵΪfalse������ѭ��
        {
            m_onFloor = true;
            m_jumps = 0;
        }
        if (m_anim.GetBool("floor") != m_onFloor)//�ı�bool���ͣ�ˢ���ж�
        {
            m_anim.SetBool("floor", m_onFloor);
        }
        if (m_body.velocity.y == 0)//���ڶ���ת��
        {
            m_anim.SetFloat("hSpeed", 0);
        }
        else
        {
            m_anim.SetFloat("hSpeed", m_body.velocity.y / Mathf.Abs(m_body.velocity.y));//�������Ŀǰ�����ٶȵ�����ֵ�����Ϊ1�Ǿ�����Ծ���������Ϊ-1�������䶯��
                                                                                        //��Ϊ����ֵ�����������ֻ��1��-1
        }


        //�����Ұ���״̬
        float h = Input.GetAxis("Horizontal");

        m_jump = Input.GetButtonDown("Jump");



        Move(h, m_jump);

    }

    private void Move(float h, bool jump)//�˶�������hΪ�жϷ���boolΪ�ж���Ծ
    {
        Vector2 v = m_body.velocity;

        if (h > 0)
        {
            v.x = h / Mathf.Abs(h) * m_Speed * Time.deltaTime;
            if (!m_FacingRight)//��ת����
            {
                Flip();
            }

            if (!m_anim.GetBool("run"))//�ı䶯��Ϊ�ܲ�����
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

            if (m_anim.GetBool("run"))//�ı䶯��Ϊ�ܲ�����
            {
                m_anim.SetBool("run", false);
            }
        }

        if (jump)//��Ծ���ĸ���
        {
            if (m_jumps <= 1)//������Ծ�Լ��������Ĳ���
            {
                v.y = m_jumpaltitude * Time.deltaTime;
            }
            m_jumps++;
        }

        m_body.velocity = v;//velocity��õ�ǰ�����ٶ� 
    }

    private void Flip()//ˮƽ��ת������������Է���
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
