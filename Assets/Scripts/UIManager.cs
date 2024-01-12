using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel; // ��� UI �г�
    public GameObject exitPanel; // �����ư Ŭ���� ��� �г�
    private int temCount; //�µ�����
    private int huCount; //��������
    public TextMeshProUGUI temText; //�µ� �ؽ�Ʈ
    public TextMeshProUGUI huText; //���� �ؽ�Ʈ

    //�������� �ؽ�Ʈ
    public TextMeshProUGUI plantText;

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

    //�Ĺ� ���忡 �ʿ��� �ʼ� ��ư �οﺯ��
    bool temClick = false;
    bool lightClick = false;
    bool nuClick = false;
    bool huClick = false;
    bool venClick = false;

    // ���� ����� �Լ�
    public void OverrideMembers(ParticleSystem heaterParticle, GameObject farmLight, Animator waterAnimator, GameObject grassGroup, GameObject tomatoGroup, GameObject tomato, GameObject blindOJ)
    {
        this.heaterParticle = heaterParticle;
        this.farmLight = farmLight;
        this.waterAnimator = waterAnimator;
        this.grassGroup = grassGroup;
        this.tomatoGroup = tomatoGroup;
        this.tomato = tomato;
        this.blindOJ = blindOJ;

        // �ʱ⿡�� UI�� ��Ȱ��ȭ
        uiPanel.SetActive(false);
        exitPanel.SetActive(false);

        // �½��� ī��Ʈ �ʱ�ȭ
        temCount = 10;
        huCount = 60;
        PlusTemCount();
        PlusHuCount();

        //�ʱ⿡�� grass �׷� ������Ʈ�� Ȱ��ȭ
        grassGroup.SetActive(true);
        tomatoGroup.SetActive(false);
        tomato.SetActive(false);

        //�ʱ⿡ LED ������Ʈ ��Ȱ��ȭ
        farmLight.SetActive(false);

        //�ʱ⿡ ����ε� ������Ʈ ��Ȱ��ȭ
        blindOJ.SetActive(false);

        plantText.text = "<color=#87CEFA>" + "�����ʱ�" + "</color>";
    }

    void Start()
    {
        // �ʱ⿡�� UI�� ��Ȱ��ȭ
        uiPanel.SetActive(false);
        exitPanel.SetActive(false);

        // �½��� ī��Ʈ �ʱ�ȭ
        temCount = 10;
        huCount = 60;
        PlusTemCount();
        PlusHuCount();

        //�ʱ⿡�� grass �׷� ������Ʈ�� Ȱ��ȭ
        grassGroup.SetActive(true);
        tomatoGroup.SetActive(false);
        tomato.SetActive(false);

        //�ʱ⿡ LED ������Ʈ ��Ȱ��ȭ
        farmLight.SetActive(false);

        //�ʱ⿡ ����ε� ������Ʈ ��Ȱ��ȭ
        blindOJ.SetActive(false);

        plantText.text = "<color=#87CEFA>" + "�����ʱ�" + "</color>";

    }

    public void ShowUI()//����Ʈ�� ������ ������ ȣ��Ǵ� �޼���
    {
        uiPanel.SetActive(true);
    }
    public void CloseUI() // door������ ���� �� ȣ��Ǵ� �޼���
    {
        uiPanel.SetActive(false);
    }
    public void ExitUI() //exit������ Ŭ���� ȣ��
    {
        exitPanel.SetActive(true);
    }
    public void ReplayUI() { exitPanel.SetActive(false); }    //replay��ư Ŭ����

    public void CloseGame() //quitŬ���� ��������
    {
        Application.Quit();
    }

    #region -----------------Count------------------------
    public void PlusTemCount() //�µ� ���� ī��Ʈ �޼���
    {
        temCount += 5;
        temText.text = temCount.ToString() + "��C";
        temClick = true;
        CheckThreeOptions();
    }
    public void PlusHuCount() //���� ���� ī��Ʈ �޼���
    {
        huCount += 5;
        huText.text = huCount.ToString() + "%";
        huClick = true;
        CheckThreeOptions();
    }
    public void MinusTemCount()
    {
        temCount -= 5;
        temText.text = temCount.ToString() + "��C";
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
    
    public void HeParticle() //���� ������ Ŭ����
    {
        heaterParticle.Play(); // ��ƼŬ ȣ��
    }

    public void OnLight() //LED ������ Ŭ���� 
    {
        farmLight.SetActive(true); //����Ʈ ������Ʈ Ȱ��ȭ
        lightClick = true; 
        CheckThreeOptions();
    }
    // ȯ�� ������ Ŭ����
    public void OnVentilation()
    {
        venClick = true;
        CheckThreeOptions();
        blindOJ.SetActive(false);
    }
    // ����ε� �ִϸ��̼� ���� �޼���
    public void OnBlind()
    {
        blindOJ.SetActive(true);
    }
    // ������ ������ Ŭ���� �ִϸ��̼� ���� �޼���
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

            //���� grass ������Ʈ ��Ȱ��ȭ
            grassGroup.SetActive(false);
            tomatoGroup.SetActive(true);

            temClick = false;
            huClick = false;
            lightClick = false;
            nuClick = false;

            plantText.text = "<color=#FFA500>" + "�����߱�" + "</color>";

            if (venClick == true)
            {
                tomato.SetActive(true);
                venClick =false;
                plantText.text = "<color=#FF4500>" + "��������" + "</color>";
            }
        }
    }
    #endregion
}
