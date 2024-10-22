using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class healthUI : MonoBehaviour
{
    private TMP_Text Hptext;
    private Image image;
    [SerializeField] Health PlayerHealth;
    private float maxHp;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        Hptext = GetComponent<TMP_Text>();
        image = GetComponent<Image>();

        UpdateUI();
    }

    public void UpdateUI()
    {
        //set les variable pour les point de vie
        maxHp = PlayerHealth.maxHealth;
        hp = PlayerHealth.CurrentHelath;

        //set la bar de pv
        image.fillAmount = hp / maxHp;
    }
}
