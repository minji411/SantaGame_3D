using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverGift: MonoBehaviour
{
    [SerializeField] private GameObject sock;
    //[SerializeField] private GameObject effect;
    //[SerializeField] private AudioSource sound;
    public bool getgift = false;
    private Material house_matl;
    public GameObject particle;
    public player2 player;
    public AudioClip clip;
    
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<player2>();
        house_matl = GetComponent<MeshRenderer>().material;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Gift" && !player.delHouse.Contains(gameObject))
        {
            SoundManager.instance.SFXPlay("delivered", clip);
            sock.gameObject.SetActive(false);
            particle.SetActive(true);
            house_matl.SetColor("_EmissionColor", new Color(255, 220, 108) * 0.01f);

            player.delivedGift++;
            player.delHouse.Add(gameObject);
        }
    }

   

}

