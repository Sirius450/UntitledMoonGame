using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulCounter : MonoBehaviour
{
    private TMP_Text SoulText;
    [SerializeField] SoulCatcher PlayerSouls;


    // Start is called before the first frame update
    void Start()
    {
        SoulText = GetComponent<TMP_Text>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        SoulText.text = PlayerSouls.CurrentSouls.ToString();

    }


}
