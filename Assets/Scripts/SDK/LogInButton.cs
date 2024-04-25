using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LogInButton : MonoBehaviour
{
    [SerializeField] private GameObject _wrongPasswordPanel;
    [SerializeField] private GameObject _wrongEmailPanel;


    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _passwordField;

    public static Action OnUserLogedIn;
    private Coroutine _registrationCoroutine;
    [SerializeField] private Button _logInButton;

    private void Reset()
    {

        _logInButton = GetComponent<Button>();
    }

    void Start()
    {
        RegistrationUIFlow.OnStateChanged += HandleRegistrationStateChanged;
        _logInButton.onClick.AddListener(HandRegistrationStateClicked);
    }

    private void OnDestroy()
    {
        RegistrationUIFlow.OnStateChanged -= HandleRegistrationStateChanged;
        _logInButton.onClick.RemoveListener(HandRegistrationStateClicked);
    }

    private void HandleRegistrationStateChanged(RegistrationUIFlow.State registrationState)
    {
    }

    private void HandRegistrationStateClicked()
    {
        _registrationCoroutine = StartCoroutine(routine: LogInUser(_emailField.text, _passwordField.text));
    }

    private IEnumerator LogInUser(string email, string password)
    {
        var auth = FireManager.Instance.Auth;

        bool isLogedIn = false;//TODO

        var logOutTask = auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                isLogedIn = false;

                
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                isLogedIn = false;
               
                return;
            }
            /*
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);*/
            isLogedIn = true;

        });

        yield return new WaitUntil(predicate: () => logOutTask.IsCompleted);

        if (isLogedIn)
        {
            _registrationCoroutine = null;
            OnUserLogedIn?.Invoke();
            PlayerPrefs.SetInt("LogedIn", 1);
        }

        else
        {
            _wrongPasswordPanel.SetActive(true);
        }
        
    }
}
