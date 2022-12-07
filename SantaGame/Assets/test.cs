using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    float hAxis;
    float vAxis;
    Transform orientation;
    // Start is called before the first frame update
    void Start()
    {
        orientation = GameObject.Find("Orientation").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        InputKey();

    }
    void InputKey()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }
    void Move()
    {
        Quaternion q;
        Vector3 vec;
        vec = Camera.main.transform.TransformDirection(new Vector3(hAxis, 0, vAxis));
        vec.y = 0;
        q = Quaternion.LookRotation(vec);

        //moveDir = orientation.forward * vAxis + orientation.right * hAxis;
        //rigid.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
        Vector3 moveDir = orientation.forward * vAxis + orientation.right * hAxis;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 15f * Time.deltaTime);
        transform.Translate(moveDir.normalized * 10 * 1f * Time.deltaTime);
    }
}
