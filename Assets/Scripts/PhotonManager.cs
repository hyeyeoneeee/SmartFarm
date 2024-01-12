using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; //네트워크에 접속하기 위한 네임스페이스 두 개
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks //베이스 클래스에 상속할 수 있도록 콜백함수들을 사용하겠다 API제공
{
    private readonly string version = "1.0";//버전을 만들어줄 거임 유저가 건들이면 안되니까 고칠수 없게
    private string userId = "HYeon";

    private void Awake()
    {
        //씬 동기화
        PhotonNetwork.AutomaticallySyncScene = true;
        // 버전할당
        PhotonNetwork.GameVersion = version;
        // AppID
        PhotonNetwork.NickName = userId;
        //포톤 서버와의 통신 횟수 (기본값: 30)
        Debug.Log($"PhotonNetwork.SendRate = {PhotonNetwork.SendRate}");

        if (PhotonNetwork.IsConnected == false)
        {
            // 포톤 서버 접속 (Name Server)
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //마스터 서버에 접속했다면 호출
    public override void OnConnectedToMaster() //콜백함수 중에서도 맨 처음에 호출됨
    {
        Debug.Log("Connected to Master!");
        //Debug.Log($"In Lobby = {PhotonNetwork.InLobby}"); //아직은 False임 로비를 따로 만들어서 호출해줘야 함
        //로비 접속
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"In Lobby = {PhotonNetwork.InLobby}");

        //방으로 접속하는 함수 호출 1) 랜덤 매치메이킹, 2) 선택된 방
        PhotonNetwork.JoinRandomRoom(); //룸을 찾아 접속시킴
    }

    // 룸이 생성되지 않았으면 오류 콜백 함수 실행
    public override void OnJoinRandomFailed(short returnCode, string message) //콜백함수여서 없으면 호출됨
    {
        Debug.Log($"JoinRandomFailed  {returnCode}: {message}");

        //룸 속성 설정
        RoomOptions ro = new RoomOptions();

        //룸에 접속할 수 있는 최대 접속자 수
        ro.MaxPlayers = 4;

        //룸 오픈 여부
        ro.IsOpen = true;

        // 로비에서 룸 목록에 노출시킬지 여부
        ro.IsVisible = true; //true면 공개방 false면 비공개방

        //------------------------------------------------------
        // 룸 생성
        PhotonNetwork.CreateRoom("HY Room", ro); //룸의 이름과 속성 나중에 로비에서 방생성시 룸의 이름을 설정
    }
    //룸이 제대로 생성됐는지 확인
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room!");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    //방에 들어왔니
    public override void OnJoinedRoom()
    {
        Debug.Log($"In Room = {PhotonNetwork.InRoom}");
        //접속자 수
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        //접속한 사용자 닉네임 확인
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            //플레이어 닉네임, 고유 값(각자 다 다른 값)을 가져옴
            //1.고유값 확인 (고유값은 들어온 순서대로 주어짐)
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
        }

        //서버를 거쳐서 프리팹 생성
        // 포인트 그룹 배열을 받아옴 (포인트 그룹의, 자식들의, 트랜스폼 컴포넌트를 받아옴)
        Transform[] points = GameObject.Find("PointGroup").GetComponentsInChildren<Transform>();
        Transform[] Farmpoints = GameObject.Find("FarmPoint").GetComponentsInChildren<Transform>();

        // 1 부터 배열의 길이까지 숫자 중 랜덤한 값 추출
        //int idx = Random.Range(0, points.Length);
        int idx = PhotonNetwork.CurrentRoom.PlayerCount;

        // 플레이어 프리팹을 추출한 idx위치와 회전 값에 생성
        PhotonNetwork.Instantiate("Player", points[idx].position, points[idx].rotation,0);
        Debug.Log($"idx: {idx} / position: {Farmpoints[idx].position}");
        PhotonNetwork.Instantiate("Greenhouse", Farmpoints[idx].position, Farmpoints[idx].rotation, 0);
        //포톤을 통해서 프리팹을 생성하는 경우에는 프로젝트 뷰의 리로스라는 소스에서 가져옴
    }

}
