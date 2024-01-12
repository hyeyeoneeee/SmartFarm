using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    //�̵��ӵ�
    public float moveSpeed = 7f;

    //ĳ���� ��Ʈ�ѷ� ������Ʈ ���� ������
    CharacterController cc;

    //�߷� ����
    float gravity = -20f;

    ////���� �ӷ� ����
    float yVelocity = 0;

    //ȸ�� �ӵ�
    public float rotSpeed = 200f;
    float mx = 0;

    //�ִϸ�����
    Animator animator;

    // ī�޶� ��ġ
    [SerializeField] Transform camPos;

    // ���� ��
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
        //Ű���� �Է�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�̵� ���� ����
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

        // ī�޶� �������� ���� ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);


        //���� �ӵ��� �߷� �� ����
        yVelocity += gravity * Time.deltaTime; //������
        dir.y = yVelocity;

        //�̵�
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //�÷��̾� ȸ��
        float mouse_X = Input.GetAxis("Mouse X");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0); //��������

    }
}
