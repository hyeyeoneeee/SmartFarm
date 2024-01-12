using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    //이동속도
    public float moveSpeed = 7f;

    //캐릭터 컨트롤러 컴포넌트 변수 가져옴
    CharacterController cc;

    //중력 변수
    float gravity = -20f;

    ////수직 속력 변수
    float yVelocity = 0;

    //회전 속도
    public float rotSpeed = 200f;
    float mx = 0;

    //애니메이터
    Animator animator;

    // 카메라 위치
    [SerializeField] Transform camPos;

    // 포톤 뷰
    PhotonView photonView;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();    
        if(photonView.IsMine)
            Camera.main.GetComponent<CamFollow>().target = camPos;
    }

    void Update()
    {
        if (photonView.IsMine == false)
            return;
        //키보드 입력
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);

        dir = dir.normalized;

        if(dir.sqrMagnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // 카메라를 기준으로 방향 전환
        dir = Camera.main.transform.TransformDirection(dir);


        //수직 속도에 중력 값 적용
        yVelocity += gravity * Time.deltaTime; //누적됨
        dir.y = yVelocity;

        //이동
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //플레이어 회전
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0); //도리도리

    }
}
