using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool IsPause;
    public Image pause;

    // Start is called before the first frame update
    void Start()
    {
        IsPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        escDown();
    }

    void escDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPause)   //���� �������̸�
            {
                Time.timeScale = 0; //���߱�
                pause.gameObject.SetActive(true);   //������ �� â ǥ��
                IsPause = true;     //���� ���� ǥ��
                return;
            }
            if (IsPause)   //���� �����̸�
            {
                Time.timeScale = 1; //�����ϱ�
                pause.gameObject.SetActive(false);   //������ �� â �ݱ�
                IsPause = false;     //���� ������ ǥ��
                return;
            }
        }
    }
}
