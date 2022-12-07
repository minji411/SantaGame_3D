
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class player2 : MonoBehaviour
{
    #region Keybinds
    [Header("Keybinds")]

    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode crouchKey = KeyCode.LeftControl;
    #endregion



    //private MovementState laststate;

    Vector3 playerCenter = new Vector3(0, 0.5f, 0);
    #region MoveSpeed Private Variables
    [Header("Movement")]
    public float WalkSpeed = 5.0f; //걸음 속도
    public float SprintSpeed = 7.0f; //달리기 속도
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
    [HideInInspector] public float groundDrag = 5f; //바닥마찰
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
        Cursor.visible = false; //마우스 커서를 보이지 않게
        Cursor.lockState = CursorLockMode.Locked; //마우스 커서 위치 고정
        capsuleCollider = GetComponent<CapsuleCollider>();
        prigidbody = GetComponent<Rigidbody>();
        prigidbody.freezeRotation = true; //회전 못하게 고정
        orientation = transform.Find("Orientation");

    }
    private void Update()
    {
        InputKey();
    }
    private void FixedUpdate()
    {
        MoveMent3D();
    }
    public void InputKey()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

    }


    public void MoveMent3D()
    {
 
 
        Quaternion q;
        Vector3 vec;

        if (hAxis != 0 || vAxis != 0)
        {

            vec = Camera.main.transform.TransformDirection(new Vector3(hAxis, 0, vAxis));
            vec.y = 0;
            q = Quaternion.LookRotation(vec);

            moveDir = orientation.forward * vAxis + orientation.right * hAxis;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 15f * Time.deltaTime);


            transform.Translate(moveDir.normalized * 20 * 1f * Time.deltaTime);





        }
      
    }
 
  
  

  



}