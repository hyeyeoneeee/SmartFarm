using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //��Ʈ��ũ�� �����ϱ� ���� ���ӽ����̽� �� ��
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks //���̽� Ŭ������ ����� �� �ֵ��� �ݹ��Լ����� ����ϰڴ� API����
{
    private readonly string version = "1.0";//������ ������� ���� ������ �ǵ��̸� �ȵǴϱ� ��ĥ�� ����
    private string userId = "HYeon";

    private void Awake()
    {
        //�� ����ȭ
        PhotonNetwork.AutomaticallySyncScene = true;
        // �����Ҵ�
        PhotonNetwork.GameVersion = version;
        // AppID
        PhotonNetwork.NickName = userId;
        //���� �������� ��� Ƚ�� (�⺻��: 30)
        Debug.Log($"PhotonNetwork.SendRate = {PhotonNetwork.SendRate}");

        if (PhotonNetwork.IsConnected == false)
        {
            // ���� ���� ���� (Name Server)
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //������ ������ �����ߴٸ� ȣ��
    public override void OnConnectedToMaster() //�ݹ��Լ� �߿����� �� ó���� ȣ���
    {
        Debug.Log("Connected to Master!");
        //Debug.Log($"In Lobby = {PhotonNetwork.InLobby}"); //������ False�� �κ� ���� ���� ȣ������� ��
        //�κ� ����
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"In Lobby = {PhotonNetwork.InLobby}");

        //������ �����ϴ� �Լ� ȣ�� 1) ���� ��ġ����ŷ, 2) ���õ� ��
        PhotonNetwork.JoinRandomRoom(); //���� ã�� ���ӽ�Ŵ
    }

    // ���� �������� �ʾ����� ���� �ݹ� �Լ� ����
    public override void OnJoinRandomFailed(short returnCode, string message) //�ݹ��Լ����� ������ ȣ���
    {
        Debug.Log($"JoinRandomFailed  {returnCode}: {message}");

        //�� �Ӽ� ����
        RoomOptions ro = new RoomOptions();

        //�뿡 ������ �� �ִ� �ִ� ������ ��
        ro.MaxPlayers = 4;

        //�� ���� ����
        ro.IsOpen = true;

        // �κ񿡼� �� ��Ͽ� �����ų�� ����
        ro.IsVisible = true; //true�� ������ false�� �������

        //------------------------------------------------------
        // �� ����
        PhotonNetwork.CreateRoom("HY Room", ro); //���� �̸��� �Ӽ� ���߿� �κ񿡼� ������� ���� �̸��� ����
    }
    //���� ����� �����ƴ��� Ȯ��
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room!");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //�濡 ���Դ�
    public override void OnJoinedRoom()
    {
        Debug.Log($"In Room = {PhotonNetwork.InRoom}");
        //������ ��
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        //������ ����� �г��� Ȯ��
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            //�÷��̾� �г���, ���� ��(���� �� �ٸ� ��)�� ������
            //1.������ Ȯ�� (�������� ���� ������� �־���)
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }

        //������ ���ļ� ������ ����
        // ����Ʈ �׷� �迭�� �޾ƿ� (����Ʈ �׷���, �ڽĵ���, Ʈ������ ������Ʈ�� �޾ƿ�)
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();
        Transform[] Farmpoints = GameObject.Find("FarmPoint").GetComponentsInChildren<Transform>();

        // 1 ���� �迭�� ���̱��� ���� �� ������ �� ����
        //int idx = Random.Range(0, points.Length);
        int idx = PhotonNetwork.CurrentRoom.PlayerCount;

        // �÷��̾� �������� ������ idx��ġ�� ȸ�� ���� ����
        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation,0);
        Debug.Log($"idx: {idx} / position: {Farmpoints[idx].position}");
        PhotonNetwork.Instantiate("Greenhouse", Farmpoints[idx].position, Farmpoints[idx].rotation, 0);
        //������ ���ؼ� �������� �����ϴ� ��쿡�� ������Ʈ ���� ���ν���� �ҽ����� ������
    }

}
