using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    AudioSource Giftsound;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.6f);
        Giftsound = GameObject.Find("Giftsound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * 45f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "House")
        {
            if (collision.transform.GetComponent<DeliverGift>().getgift == false)
            {
                collision.transform.GetComponent<DeliverGift>().getgift = true;
                Giftsound.Play();
                Destroy(this.gameObject);
            }
        }          
    }
}
