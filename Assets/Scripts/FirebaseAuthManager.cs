using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    public TMP_InputField email;
    public TMP_InputField password;

    //로그인 씬 전환위한 불값
    bool isLogin = false;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Create()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log(task.Exception.ToString());
                Debug.Log("계정 생성 실패");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("계정 생성 취소");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("계정 생성 성공");
        });
    }
    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("로그인 실패");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("로그인 취소");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.Log("로그인 성공");
            isLogin = true;
        });
    }
    public void Logout()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }
    private void Update()
    {
        if (isLogin == true)
        {
            SceneManager.LoadScene("SmartFarm");
        }
    }
}