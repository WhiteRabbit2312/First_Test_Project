using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RegistrationUIFlow : MonoBehaviour
{
    [SerializeField] private Button regisrtationButton;

    [HideInInspector] public string Email;
    [HideInInspector] public string Password;
    [HideInInspector] public string Name;
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private TMP_InputField _verifyPasswordField;

    public State CurrentState
    {
        get;
        private set;
    }

    public static Action<State> OnStateChanged;//

    private void Awake()
    {
        _emailField.onValueChanged.AddListener(ValueChanged);
        _passwordField.onValueChanged.AddListener(ValueChanged);
        _verifyPasswordField.onValueChanged.AddListener(ValueChanged);

        ComputeState();
        regisrtationButton.gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
        regisrtationButton.enabled = false;
    }

    private void ValueChanged(string _)
    {
        ComputeState();
    }

    private void ComputeState()
    {
        if (string.IsNullOrEmpty(_emailField.text))
        {
            Email = _emailField.text;
            SetState(State.EnterEmail);
        }

        else if (string.IsNullOrEmpty(_nameField.text))
        {
            Name = _nameField.text;
            SetState(State.EnterName);
        }

        else if (string.IsNullOrEmpty(_passwordField.text))
        {
            Password = _passwordField.text;
            SetState(State.EnterPassword);
            regisrtationButton.gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
            regisrtationButton.enabled = false;
        }

        else if (_passwordField.text != _verifyPasswordField.text)
        {
            regisrtationButton.gameObject.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
            regisrtationButton.enabled = false;
            SetState(State.PasswordDontMatch);
        }

        else
        {
            regisrtationButton.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            regisrtationButton.enabled = true;
            SetState(State.Ok);
        }
    }

    private void SetState(State state)
    {
        CurrentState = state;
        OnStateChanged?.Invoke(state);
    }

    public enum State
    {
        EnterEmail,
        EnterName,
        EnterPassword,
        PasswordDontMatch,
        Ok
    }
}
