using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    public Text healthText;//Ѫ������
    public static int HealthCurrent;//ʣ��Ѫ��
    public static int HealthMax;//��Ѫ��

    private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;//Ѫ������ʾ
        healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();//Ѫ����ʾ
    }
}
