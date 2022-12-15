using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public GameObject[] Bullet;
    public Transform FirePos;
    GameObject SBullet;
    AudioSource audioSource;
    [SerializeField] GameObject[] GBullet;
    bool changebullet = false;
    public Image snowMode;
    public Image giftMode;

    public player2 player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (changebullet == false)
            {
                changebullet = true;
                giftMode.gameObject.SetActive(true);
                snowMode.gameObject.SetActive(false);
            }
            else if (changebullet == true)
            {
                changebullet = false;
                snowMode.gameObject.SetActive(true);
                giftMode.gameObject.SetActive(false);
            }

            //Debug.Log(changebullet);
        }
        if (Input.GetMouseButtonDown(0) && !player.IsPause)
        {
            if (changebullet == false)
            {
                int i = Random.Range(0, 3);
                SBullet = Bullet[i];
                Instantiate(SBullet, transform.position, transform.rotation);
                audioSource.Play();
            }
            else if (changebullet==true)
            {
                int i = Random.Range(0, 3);
                SBullet = GBullet[i];
                Instantiate(SBullet, transform.position, transform.rotation);
                audioSource.Play();
            }
        }
    }
}
