using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 얘가 이제
// UI 매니저 변수들을 얘걸로 갈아끼면 되는데
// 프리팹 내에서는 참조할 수 있으니까 미리 public으로 담아놓고
// UI 매니저에 밀어넣어주면 될듯
public class Greenhouse : MonoBehaviour
{
    // 포톤 뷰
    PhotonView photonView;

    //파티클 변수
    public ParticleSystem heaterParticle;   // 할당 필요

    //라이트 오브젝트
    public GameObject farmLight;    // 할당 필요

    //애니메이터 컴포넌트
    public Animator waterAnimator;  // 할당 필요

    //식물 그룹 오브젝트
    public GameObject grassGroup;   // 할당 필요
    public GameObject tomatoGroup;  // 할당 필요
    public GameObject tomato;       // 할당 필요
    
    //블라인드 오브젝트
    public GameObject blindOJ;      // 할당 필요

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
