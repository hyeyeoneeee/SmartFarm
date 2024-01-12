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

    //�α��� �� ��ȯ���� �Ұ�
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
                Debug.Log("���� ���� ����");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("���� ���� ���");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.Log("���� ���� ����");
        });
    }
    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("�α��� ����");
                return;
            }
            if (task.IsCanceled)
            {
                Debug.Log("�α��� ���");
                return;
            }
            FirebaseUser newUser = task.Result.User;
            Debug.Log("�α��� ����");
            isLogin = true;
        });
    }
    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
    private void Update()
    {
        if (isLogin == true)
        {
            SceneManager.LoadScene("SmartFarm");
        }
    }
}