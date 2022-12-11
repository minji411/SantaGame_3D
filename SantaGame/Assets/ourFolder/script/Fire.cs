using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject[] Bullet;
    public Transform FirePos;
    GameObject SBullet;
    AudioSource audioSource;
    [SerializeField] GameObject[] GBullet;
    bool changebullet = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (changebullet == false)
                changebullet = true;
            else if (changebullet == true)
                changebullet = false;
            Debug.Log(changebullet);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (changebullet == false)
            {
                int i = Random.Range(0, 3);
                SBullet = Bullet[i];
                Instantiate(SBullet, FirePos.transform.position, FirePos.transform.rotation);
                audioSource.Play();
            }
            else if (changebullet==true)
            {
                int i = Random.Range(0, 3);
                SBullet = GBullet[i];
                Instantiate(SBullet, FirePos.transform.position, FirePos.transform.rotation);
                audioSource.Play();
            }
        }
    }
}
