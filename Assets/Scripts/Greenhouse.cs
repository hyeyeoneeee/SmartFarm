using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// �갡 ����
// UI �Ŵ��� �������� ��ɷ� ���Ƴ��� �Ǵµ�
// ������ �������� ������ �� �����ϱ� �̸� public���� ��Ƴ���
// UI �Ŵ����� �о�־��ָ� �ɵ�
public class Greenhouse : MonoBehaviour
{
    // ���� ��
    PhotonView photonView;

    //��ƼŬ ����
    public ParticleSystem heaterParticle;   // �Ҵ� �ʿ�

    //����Ʈ ������Ʈ
    public GameObject farmLight;    // �Ҵ� �ʿ�

    //�ִϸ����� ������Ʈ
    public Animator waterAnimator;  // �Ҵ� �ʿ�

    //�Ĺ� �׷� ������Ʈ
    public GameObject grassGroup;   // �Ҵ� �ʿ�
    public GameObject tomatoGroup;  // �Ҵ� �ʿ�
    public GameObject tomato;       // �Ҵ� �ʿ�
    
    //����ε� ������Ʈ
    public GameObject blindOJ;      // �Ҵ� �ʿ�

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().OverrideMembers(heaterParticle, farmLight, waterAnimator, grassGroup, tomatoGroup, tomato, blindOJ);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
            return;
    }

}
