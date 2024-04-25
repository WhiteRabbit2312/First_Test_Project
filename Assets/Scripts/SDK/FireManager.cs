using UnityEngine;
using Firebase.Auth;
using System;


public class FireManager : MonoBehaviour
{
    private static FireManager _instance;

    public static FireManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private FirebaseAuth _auth;

    public FirebaseAuth Auth
    {
        get;
        set;
    }
 

    public static Action OnFirebaseInitialized;

    private void Awake()
    {
        _auth = FirebaseAuth.DefaultInstance;

        Auth = _auth;

        if (_instance == null)
        {
            _instance = this;
 
        }

    }

   

    private void OnDestroy()
    {
        _auth = null;

        if(_instance == this)
        {
            _instance = null;
        }
    }
}
