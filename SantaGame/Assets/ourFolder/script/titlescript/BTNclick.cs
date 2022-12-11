using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTNclick : MonoBehaviour
{
    public Image window;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void next()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToWindowClose()
    {
        window.gameObject.SetActive(false);
    }

    public void HowToWindowOpen()
    {
        window.gameObject.SetActive(true);
    }
}
