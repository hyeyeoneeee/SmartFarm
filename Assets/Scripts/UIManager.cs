using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel; // 띄울 UI 패널
    public GameObject exitPanel; // 종료버튼 클릭시 띄울 패널
    private int temCount; //온도숫자
    private int huCount; //습도숫자
    public TextMeshProUGUI temText; //온도 텍스트
    public TextMeshProUGUI huText; //습도 텍스트

    //생육상태 텍스트
    public TextMeshProUGUI plantText;

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

    //식물 성장에 필요한 필수 버튼 부울변수
    bool temClick = false;
    bool lightClick = false;
    bool nuClick = false;
    bool huClick = false;
    bool venClick = false;

    // 변수 덮어쓰는 함수
    public void OverrideMembers(ParticleSystem heaterParticle, GameObject farmLight, Animator waterAnimator, GameObject grassGroup, GameObject tomatoGroup, GameObject tomato, GameObject blindOJ)
    {
        this.heaterParticle = heaterParticle;
        this.farmLight = farmLight;
        this.waterAnimator = waterAnimator;
        this.grassGroup = grassGroup;
        this.tomatoGroup = tomatoGroup;
        this.tomato = tomato;
        this.blindOJ = blindOJ;

        // 초기에는 UI를 비활성화
        uiPanel.SetActive(false);
        exitPanel.SetActive(false);

        // 온습도 카운트 초기화
        temCount = 10;
        huCount = 60;
        PlusTemCount();
        PlusHuCount();

        //초기에는 grass 그룹 오브젝트만 활성화
        grassGroup.SetActive(true);
        tomatoGroup.SetActive(false);
        tomato.SetActive(false);

        //초기에 LED 오브젝트 비활성화
        farmLight.SetActive(false);

        //초기에 블라인드 오브젝트 비활성화
        blindOJ.SetActive(false);

        plantText.text = "<color=#87CEFA>" + "생육초기" + "</color>";
    }

    void Start()
    {
        // 초기에는 UI를 비활성화
        uiPanel.SetActive(false);
        exitPanel.SetActive(false);

        // 온습도 카운트 초기화
        temCount = 10;
        huCount = 60;
        PlusTemCount();
        PlusHuCount();

        //초기에는 grass 그룹 오브젝트만 활성화
        grassGroup.SetActive(true);
        tomatoGroup.SetActive(false);
        tomato.SetActive(false);

        //초기에 LED 오브젝트 비활성화
        farmLight.SetActive(false);

        //초기에 블라인드 오브젝트 비활성화
        blindOJ.SetActive(false);

        plantText.text = "<color=#87CEFA>" + "생육초기" + "</color>";

    }

    public void ShowUI()//스마트폰 아이콘 누르면 호출되는 메서드
    {
        uiPanel.SetActive(true);
    }
    public void CloseUI() // door아이콘 누를 때 호출되는 메서드
    {
        uiPanel.SetActive(false);
    }
    public void ExitUI() //exit아이콘 클릭시 호출
    {
        exitPanel.SetActive(true);
    }
    public void ReplayUI() { exitPanel.SetActive(false); }    //replay버튼 클릭시

    public void CloseGame() //quit클릭시 게임종료
    {
        Application.Quit();
    }

    #region -----------------Count------------------------
    public void PlusTemCount() //온도 증가 카운트 메서드
    {
        temCount += 5;
        temText.text = temCount.ToString() + "°C";
        temClick = true;
        CheckThreeOptions();
    }
    public void PlusHuCount() //습도 증가 카운트 메서드
    {
        huCount += 5;
        huText.text = huCount.ToString() + "%";
        huClick = true;
        CheckThreeOptions();
    }
    public void MinusTemCount()
    {
        temCount -= 5;
        temText.text = temCount.ToString() + "°C";
        temClick = true;
        CheckThreeOptions();
    }
    public void MinusHuCount()
    {
        huCount -= 5;
        huText.text = huCount.ToString() + "%";
        huClick = true;
        CheckThreeOptions();
    }
    #endregion

    #region ---------------Particle--------------------
    
    public void HeParticle() //히터 아이콘 클릭시
    {
        heaterParticle.Play(); // 파티클 호출
    }

    public void OnLight() //LED 아이콘 클릭시 
    {
        farmLight.SetActive(true); //라이트 오브젝트 활성화
        lightClick = true; 
        CheckThreeOptions();
    }
    // 환기 아이콘 클릭시
    public void OnVentilation()
    {
        venClick = true;
        CheckThreeOptions();
        blindOJ.SetActive(false);
    }
    // 블라인드 애니메이션 실행 메서드
    public void OnBlind()
    {
        blindOJ.SetActive(true);
    }
    // 영양제 아이콘 클릭시 애니메이션 실행 메서드
    public void OnNutrients()
    {
        waterAnimator.SetTrigger("Watering");
        nuClick = true;
        CheckThreeOptions();
    }

    void CheckThreeOptions()
    {
        if(temClick == true && huClick == true && lightClick == true && nuClick == true)
        {

            //현재 grass 오브젝트 비활성화
            grassGroup.SetActive(false);
            tomatoGroup.SetActive(true);

            temClick = false;
            huClick = false;
            lightClick = false;
            nuClick = false;

            plantText.text = "<color=#FFA500>" + "생육중기" + "</color>";

            if (venClick == true)
            {
                tomato.SetActive(true);
                venClick =false;
                plantText.text = "<color=#FF4500>" + "생육말기" + "</color>";
            }
        }
    }
    #endregion
}
