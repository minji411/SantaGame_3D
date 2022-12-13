
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MovementState
{
    walk,
    sprint,
    jump,
    air,
    //crouch,
    slide,
    airdash,
    blowed,
    dead,
    zoom,
    slope,

}

public class player2 : MonoBehaviour
{
    #region Keybinds
    [Header("Keybinds")]

    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode crouchKey = KeyCode.LeftControl;
    #endregion

    public MovementState state;

    public bool IsPause;

    bool isjump = false;
    int santaHP = 100;
    public Image HPgauge;
    public Text HPtext;
    public int delivedGift;
    public Image pause;

    int isJ = 0;
    Animator anim;
    //private MovementState laststate;

    Vector3 playerCenter = new Vector3(0, 0.5f, 0);
    #region MoveSpeed Private Variables
    [Header("Movement")]
    public float WalkSpeed = 20.0f; //걸음 속도
    public float SprintSpeed = 30.0f; //달리기 속도
    public float moveSpeed;  //움직임 속도
    public float jumpForce = 10f; //점프 정도
    [HideInInspector] public float airMultiplier = 0.4f; //공중 속도 제어
    [HideInInspector] public float slideSpeed = 10f; //슬라이딩 스피드
    [HideInInspector] public float airDashSpeed = 20f; //공중 대시 스피드
    [HideInInspector] public float airDashSpeedChangeFactor = 50; //공중 상태일때 빠르게 속도 전환
    [HideInInspector] public float crouchSpeed = 5f;
    [HideInInspector] public float desiredMoveSpeed;
    [HideInInspector] public float lastDesiredMoveSpeed;
    // [HideInInspector] public float speedChangeFactor;
    #endregion
    #region Float property
    [HideInInspector] public float gravityForce = 6f; //중력 정도
    [HideInInspector] public float hAxis; //키입력 horizontal
    [HideInInspector] public float vAxis; //키입력 vertical
    [HideInInspector] public float groundDrag = 2f; //바닥마찰
                                                    //private float onAirTime = 0f; //공중에 있었던 시간
    #endregion
    #region Bool property
    public bool grounded = false; //바닥과 닿아있는 상태
    [HideInInspector] public bool isJump; //점프상태
    [HideInInspector] public bool isDash; //대시상태
    [HideInInspector] public bool isEffect; //이펙트 상태
    [HideInInspector] public bool controllCan; //플레이어 컨트롤 가능상태
    [HideInInspector] public bool isSlidng; //슬라이딩 상태
    [HideInInspector] public bool keepMoment;
    [HideInInspector] public bool isBlow; //내려찍기 상태
    [HideInInspector] public bool arrowLayer; //화살 레이어 체크
    [HideInInspector] public bool downhillroad = false; //내리막길 체크
    [HideInInspector] public bool lookArrow = false;
    #endregion
    #region Int property
    public int jumpCnt = 2; //점프 수 카운트
    [HideInInspector] public int dashCnt = 1; //대시 수 카운트
    [HideInInspector] public int landingCnt = 1; //착지 애니메이션 작동 제어
    #endregion
    #region Sliding Setting 
    [Header("Slope Handling")]
    [HideInInspector] public float slideForce = 200f;


    #endregion
    #region Vector3

    public Vector3 moveDir;
    Vector3 moveVec;
    #endregion

    //private float maxSlopeAngle = 90f; //최대 인식 기울기
    Rigidbody prigidbody;
    [HideInInspector] public Transform orientation;


    CapsuleCollider capsuleCollider;
    RaycastHit hit;
    // private RaycastHit slopeHit; //기울기 raycast
    // Start is called before the first frame update
    void Start()
    {
        HPgauge = GameObject.Find("HPgauge").GetComponent<Image>();
        HPtext = GameObject.Find("HPtext").GetComponent<Text>();
        //Cursor.visible = false; //마우스 커서를 보이지 않게
        //Cursor.lockState = CursorLockMode.Locked; //마우스 커서 위치 고정
        capsuleCollider = GetComponent<CapsuleCollider>();
        prigidbody = GetComponent<Rigidbody>();
        prigidbody.freezeRotation = true; //회전 못하게 고정
        orientation = transform.Find("Orientation");
        anim = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        SpeedControl();
        StateHandler();
        InputKey();
        GroundCheck();
        DragCtrl();
        Jump();
        Shooting();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!IsPause)   //???? ??????????
            {
                Time.timeScale = 0; //??????
                pause.gameObject.SetActive(true);   //?????? ?? ?? ????
                Cursor.visible = true; //???? ??????
                Cursor.lockState = CursorLockMode.Confined; //?????? ???? ???? ??????
                IsPause = true;     //???? ???? ????
                return;
            }
            if (IsPause)   //???? ????????
            {
                Time.timeScale = 1; //????????
                pause.gameObject.SetActive(false);   //?????? ?? ?? ????
                Cursor.visible = false; //???? ?? ??????
                Cursor.lockState = CursorLockMode.Locked; //?????? ???? ???? ????
                IsPause = false;     //???? ?????? ????
                return;
            }
        }
        else if (!IsPause)
        {
            Shooting();
        }

        if (santaHP <= 0)
        {
            SceneManager.LoadScene("failure");
        }

        if (delivedGift == 7)
        {
            SceneManager.LoadScene("success");
        }
        HPbar();
    }
    private void FixedUpdate()
    {
        MoveMent3D();
    }
    public void HPbar()
    {
        HPgauge.fillAmount = santaHP / 100f;
        HPtext.text = string.Format("HP {0}/100", santaHP);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //prigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("DoJump", true);
            isjump = true;
            isJ++;
        }
    }
    public void Damaged()
    {
        santaHP = santaHP - 10;
    }
    public void InputKey()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        anim.SetBool("IsRun", moveVec != Vector3.zero);
    }

    void Shooting()
    {
        //transform.Rotate(0f, rotAxis * bspeed, 0f, Space.Self);
        //transform.Rotate(Input.GetAxis("Mouse Y") * bspeed, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("DoShot");
        }
    }

    public void DragCtrl()
    {
        if (grounded)
            prigidbody.drag = groundDrag;
        else
        {
            prigidbody.drag = 0f;
        }
    }

    public void MoveMent3D()
    {
        DragCtrl();
   
        prigidbody.AddForce(Vector3.down * gravityForce);
        Quaternion q;
        Vector3 vec;

        if (hAxis != 0 || vAxis != 0)
        {
            vec = Camera.main.transform.TransformDirection(new Vector3(hAxis, 0, vAxis));
            vec.y = 0;
            q = Quaternion.LookRotation(vec);

            moveDir = orientation.forward * vAxis + orientation.right * hAxis;


            if (grounded)
            {
                prigidbody.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
     
            }

            else if (!grounded)
            {
                prigidbody.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
  
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 15f * Time.deltaTime);
        }
    }


    public void SpeedControl()
    {
        if (prigidbody.velocity.magnitude < 0.2f) //가만히 있을때 작은 값으로 speed가 생기는 것을 방지
        {
            prigidbody.velocity = Vector3.zero;
        }
        if (downhillroad) //다운힐에서 지정한 속도로만움직이게 하기위함
        {
            if (prigidbody.velocity.magnitude > moveSpeed)
                prigidbody.velocity = prigidbody.velocity.normalized * moveSpeed;
        }

        else //속도 제한 시켜주기위함 현재 상태에 따른 속도를 초과했을때 속도를 제한함
        {
            Vector3 flatVel = new Vector3(prigidbody.velocity.x, 0f, prigidbody.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                prigidbody.velocity = new Vector3(limitedVel.x, prigidbody.velocity.y, limitedVel.z);
            }
        }

    }

    public void StateHandler()
    {
         if (grounded)
        {
            capsuleCollider.material.dynamicFriction = 1f;

            gravityForce = 6f;
            state = MovementState.walk;
     
            desiredMoveSpeed = WalkSpeed;
        }
        else
        {
            state = MovementState.air;
            capsuleCollider.material.dynamicFriction = 0f;
  
            //desiredMoveSpeed = WalkSpeed;
            if (desiredMoveSpeed < SprintSpeed)
                desiredMoveSpeed = WalkSpeed;
            else
                desiredMoveSpeed = SprintSpeed;

        }
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            //StartCoroutine(SmmothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }
        //bool desiredMoveSpeedChange = desiredMoveSpeed != lastDesiredMoveSpeed;

        //if (laststate == MovementState.airdash) keepMoment = true;
        //if (desiredMoveSpeedChange)
        //{
        //    if (keepMoment)
        //    {
        //        StopAllCoroutines();
        //        //StartCoroutine(SmmothlyLerpMoveSpeed());
        //    }
        //    else
        //    {
        //        StopAllCoroutines();
        //        moveSpeed = desiredMoveSpeed;
        //    }
        //}
        lastDesiredMoveSpeed = desiredMoveSpeed;
        //laststate = state;

    }

    public void GroundCheck() //공중에서는 Collision , Trigger 체크를 못하니 함수로 체크
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, 200f))
        {
            //if (hit.distance > 0.5f)
            if (hit.distance > 0.35f)
            {
                grounded = false;
       
            }
        }
    }

    private void OnTriggerExit(Collider other) //CollisionExit로 하면 울퉁불퉁한곳에서 grouned가 false됨 Trigger 박스 긴거 하나 만들어서 체크중
    {
        if (other.transform.tag != null)
        {
            grounded = false;
        }
    }
    private void OnCollisionStay(Collision collision) //플레이어 밑에 오브젝트와 0.5이하 거리라면 바닥이라고 판정 ->lowpoly의 경우 틈이 많아 0.5가 적절
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;

        }
        else if (Physics.Raycast(transform.position, transform.up * -1, out hit, 10f))
        {
            float myAng = Vector3.Angle(Vector3.up, hit.normal);

            if (hit.distance < 0.1f)
            {
                isjump = false;
                isJ = 0;
                grounded = true;
                //playerAnimation.InitAnim();

            }
            if (myAng > 30 && hit.distance < 1f)
            {

                grounded = true;
            }

        }
        if (!grounded)
        {
            prigidbody.AddForce(transform.forward * -1 * 0.5f, ForceMode.Impulse);
            //prigidbody.AddForce(Vector3.down*20f , ForceMode.Force);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, 10f))
        {
            if (hit.distance < 1f)
            {
                controllCan = true;

                jumpCnt = 2;
                isJump = false;
                dashCnt = 1;
                isBlow = false;
    
                if (landingCnt == 1) //점프를 하고 나면 landingCnt 1로 설정 -> 점프를 하고나면 착지를 해야하니 , 내려찍기후에도 1로 설정
                {
                    landingCnt = 0; //착지 가능 횟수 0으로 설정
                }
            }
        }
   
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, 1f))
        {
            prigidbody.velocity = new Vector3(0, -3, 0);
        }
    }
}