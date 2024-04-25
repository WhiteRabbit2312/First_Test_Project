using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIRegister : MonoBehaviour
{
    [SerializeField] private GameObject _registerPanel;
    [SerializeField] private GameObject _loginPanel;

    [SerializeField] private Button _logInButton;
    [SerializeField] private Button _registerButton;

    public static Action OnSilentAuthorization;

    private int _logedIn;

    // Start is called before the first frame update
    void Awake()
    {
        _logedIn = PlayerPrefs.GetInt("LogedIn");

        if (_logedIn == 1)
        {
            SceneManager.LoadScene(1);
            OnSilentAuthorization?.Invoke();
        }
        else
        {
            _loginPanel.SetActive(true);
            _registerPanel.SetActive(false);
        }

        _registerButton.onClick.AddListener(OpenRegisterPanel);
        _logInButton.onClick.AddListener(OpenLogInPanel);
    }

    private void OpenRegisterPanel()
    {
        _registerPanel.SetActive(true);
        _loginPanel.SetActive(false);
    }

    private void OpenLogInPanel()
    {
        _registerPanel.SetActive(false);
        _loginPanel.SetActive(true);
    }
}
