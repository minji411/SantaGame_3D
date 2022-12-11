using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    int santaHP = 100;
    public Image HPgauge;
    public Text HPtext;

    Animator anim;
    bool IsPause;
    public Image pause;

    private void Awake()
    {
        HPgauge = GameObject.Find("HPgauge").GetComponent<Image>();
        HPtext = GameObject.Find("HPtext").GetComponent<Text>();
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        IsPause = false;
    }

    void Update()
    {
        Shooting();
        HPbar();
        escDown();
    }

    void escDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPause)   //°ÔÀÓ ÁøÇàÁßÀÌ¸é
            {
                Time.timeScale = 0; //¸ØÃß±â
                pause.gameObject.SetActive(true);   //¸ØÃèÀ» ¶§ Ã¢ Ç¥½Ã
                IsPause = true;     //°ÔÀÓ ¸ØÃã Ç¥½Ã
                return;
            }
            if (IsPause)   //°ÔÀÓ ¸ØÃãÀÌ¸é
            {
                Time.timeScale = 1; //ÁøÇàÇÏ±â
                pause.gameObject.SetActive(false);   //¸ØÃèÀ» ¶§ Ã¢ ´Ý±â
                IsPause = false;     //°ÔÀÓ ÁøÇàÁß Ç¥½Ã
                return;
            }
        }
    }
    
    void Shooting()
    {
        //transform.Rotate(0f, rotAxis * bspeed, 0f, Space.Self);
        //transform.Rotate(Input.GetAxis("Mouse Y") * bspeed, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("DoShot");
        }
    }

    public void Damaged()
    {
        santaHP = santaHP - 10;
    }

    public void HPbar()
    {
        HPgauge.fillAmount = santaHP / 100f;
        HPtext.text = string.Format("HP {0}/100", santaHP);
    }
}

