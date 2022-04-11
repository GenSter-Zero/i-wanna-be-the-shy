using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    public Text healthText;//血量文字
    public static int HealthCurrent;//剩余血量
    public static int HealthMax;//总血量

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
        healthBar.fillAmount = (float)HealthCurrent / (float)HealthMax;//血量条显示
        healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();//血量显示
    }
}
