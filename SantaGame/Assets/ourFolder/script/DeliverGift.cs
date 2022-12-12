using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverGift: MonoBehaviour
{
    [SerializeField] private GameObject sock;
    //[SerializeField] private GameObject effect;
    //[SerializeField] private AudioSource sound;

    public player player;

    private void Start()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Gift")
        {
            sock.gameObject.SetActive(false);
            player.delivedGift++;
        }
    }

   

}

