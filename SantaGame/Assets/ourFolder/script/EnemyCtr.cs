using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtr : MonoBehaviour
{
    [SerializeField]
     private int enemyHP = 30;

    public player2 player;

    NavMeshAgent agent;

    [SerializeField]
    Transform target;

    public AudioClip clipDamage;
    public AudioClip clipAttack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<player2>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 disvec = target.position - this.gameObject.transform.position;
        float mag = Vector3.Magnitude(disvec);
        
        if (Mathf.Abs(mag) <= 45.0)
        {
            agent.destination = target.position;
        }

        if(enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SoundManager.instance.SFXPlay("damage", clipDamage);
            player.Damaged();
        }
        else if(collision.gameObject.tag == "Weapon")
        {
            SoundManager.instance.SFXPlay("attack", clipAttack);
            enemyHP = enemyHP - 10;
        }
    }
}
