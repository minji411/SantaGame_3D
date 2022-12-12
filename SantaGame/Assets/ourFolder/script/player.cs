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
        Cursor.visible = false; //Ŀ�� �� ���̰�
        Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ�� ��ġ ����
        anim = GetComponentInChildren<Animator>();
        IsPause = false;
    }

    void Update()
    {
        HPbar();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsPause)   //���� �������̸�
            {
                Time.timeScale = 0; //���߱�
                pause.gameObject.SetActive(true);   //������ �� â ǥ��
                Cursor.visible = true; //Ŀ�� ���̰�
                Cursor.lockState = CursorLockMode.Confined; //���콺 Ŀ�� ���� ������
                IsPause = true;     //���� ���� ǥ��
                return;
            }
            if (IsPause)   //���� �����̸�
            {
                Time.timeScale = 1; //�����ϱ�
                pause.gameObject.SetActive(false);   //������ �� â �ݱ�
                Cursor.visible = false; //Ŀ�� �� ���̰�
                Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ�� ��ġ ����
                IsPause = false;     //���� ������ ǥ��
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

