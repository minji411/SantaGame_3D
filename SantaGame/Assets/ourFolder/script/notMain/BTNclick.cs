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
        Time.timeScale = 1;     //진행하기
        Cursor.visible = false; //커서 안 보이게
        Cursor.lockState = CursorLockMode.Locked; //마우스 커서 위치 고정
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("title");
        Time.timeScale = 1; //진행하기
    }


    public void next()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToWindowClose()
    {
        window.gameObject.SetActive(false);
        Time.timeScale = 1; //진행하기
        //Cursor.visible = false; //커서 안 보이게
        //Cursor.lockState = CursorLockMode.Locked; //마우스 커서 위치 고정
        player.IsPause = false;     //게임 진행중 표시
    }

    public void HowToWindowOpen()
    {
        window.gameObject.SetActive(true);
    }
}
