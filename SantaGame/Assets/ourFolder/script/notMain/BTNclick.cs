using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTNclick : MonoBehaviour
{
    public Image window;
    public player2 player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1;     //�����ϱ�
        Cursor.visible = false; //Ŀ�� �� ���̰�
        Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ�� ��ġ ����
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("title");
        Time.timeScale = 1; //�����ϱ�
    }


    public void next()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToWindowClose()
    {
        window.gameObject.SetActive(false);
        Time.timeScale = 1; //�����ϱ�
        //Cursor.visible = false; //Ŀ�� �� ���̰�
        //Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ�� ��ġ ����
        player.IsPause = false;     //���� ������ ǥ��
    }

    public void HowToWindowOpen()
    {
        window.gameObject.SetActive(true);
    }
}
