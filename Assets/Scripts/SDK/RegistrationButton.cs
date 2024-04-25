using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using Firebase.Database;


public class RegistrationButton : MonoBehaviour
{
    [SerializeField] private GameObject _nameExistingPanel;
    [SerializeField] private GameObject _wrongSymbolNumberPanel;
    [SerializeField] private GameObject _wrongEmailPanel;

    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _passwordField;

    [SerializeField] private RegistrationUIFlow _registrationFlow;
    [SerializeField] private Button _registrationButton;

    public static Action OnUserRegistered;
    public static Action<string> OnWriteNewUser;

    private Coroutine _registrationCoroutine;

    public const string EMAIL_PATTERN = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
   + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
   + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
   + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    private bool _nameExist;

    private void Reset()
    {
        _registrationFlow = FindObjectOfType<RegistrationUIFlow>();
        _registrationButton = GetComponent<Button>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _registrationButton.onClick.AddListener(HandRegistrationStateClicked);
    }

    private void OnDestroy()
    {
        _registrationButton.onClick.RemoveListener(HandRegistrationStateClicked);
    }

  

    private bool IsEmailValid(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            return Regex.IsMatch(email, EMAIL_PATTERN);
        }
        else
        {
            return false;
        }
    }

    private void HandRegistrationStateClicked()
    {
        StartCoroutine(FindName());
        //_registrationCoroutine = StartCoroutine(routine: RegisterUser(_emailField.text, _passwordField.text));
    }

    bool isRegistered;
    private IEnumerator RegisterUser(string email, string password)
    {
        
        bool validEmail = IsEmailValid(email);

        var auth = FireManager.Instance.Auth;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {

            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                isRegistered = false;
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                isRegistered = false;
                return;
            }

            isRegistered = true;
        });

        yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
        _registrationCoroutine = null;

        if (!validEmail)
        {
            _wrongEmailPanel.SetActive(true);
        }

        else
        {
            

            if (isRegistered)
            {
                
                OnUserRegistered?.Invoke();
                OnWriteNewUser?.Invoke(_nameField.text);
            }

            else
            {
                _wrongSymbolNumberPanel.SetActive(true);
            }
        }
        

    }

    private IEnumerator FindName()
    {
        _nameExist = false;
        var task = DatabaseReference.Instance.Reference.Child("User").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error when getting highscore");
        }
        else
        {
            DataSnapshot snapshot = task.Result;

            foreach (var item in snapshot.Children)
            {
                if (_nameField.text == item.Child("name").Value.ToString())
                {
                    Debug.Log("Find name");
                    _nameExistingPanel.SetActive(true);
                    _nameExist = true;
                    yield return null;
                }

            }

            if (!_nameExist)
            {
                StartCoroutine(RegisterUser(_emailField.text, _passwordField.text));
                
            }
        }
    }
}
