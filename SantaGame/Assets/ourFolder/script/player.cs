using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    int santaHP = 100;
    public Image HPgauge;
    public Text HPtext;

    Animator anim;
    public bool IsPause;
    public Image pause;

    public int delivedGift;

    private void Awake()
    {
        HPgauge = GameObject.Find("HPgauge").GetComponent<Image>();
        HPtext = GameObject.Find("HPtext").GetComponent<Text>();
    }

    void Start()
    {
        Cursor.visible = false; //커서 안 보이게
        Cursor.lockState = CursorLockMode.Locked; //마우스 커서 위치 고정
        anim = GetComponentInChildren<Animator>();
        IsPause = false;
    }

    void Update()
    {
        HPbar();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsPause)   //게임 진행중이면
            {
                Time.timeScale = 0; //멈추기
                pause.gameObject.SetActive(true);   //멈췄을 때 창 표시
                Cursor.visible = true; //커서 보이게
                Cursor.lockState = CursorLockMode.Confined; //마우스 커서 게임 위에만
                IsPause = true;     //게임 멈춤 표시
                return;
            }
            if (IsPause)   //게임 멈춤이면
            {
                Time.timeScale = 1; //진행하기
                pause.gameObject.SetActive(false);   //멈췄을 때 창 닫기
                Cursor.visible = false; //커서 안 보이게
                Cursor.lockState = CursorLockMode.Locked; //마우스 커서 위치 고정
                IsPause = false;     //게임 진행중 표시
                return;
            }
        }
        else if (!IsPause)
        {
            Shooting();
        }

        if (santaHP <= 0)
        {
            SceneManager.LoadScene("failure");
        }

        if (delivedGift == 7)
        {
            SceneManager.LoadScene("success");
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

