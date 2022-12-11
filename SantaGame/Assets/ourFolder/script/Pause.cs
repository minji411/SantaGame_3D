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
}
