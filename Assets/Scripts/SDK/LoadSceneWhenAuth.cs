using UnityEngine;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LoadSceneWhenAuth : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        RegistrationButton.OnUserRegistered += HandleAuthStateChanged;
        LogInButton.OnUserLogedIn += HandleAuthStateChanged;
    }

    private void HandleAuthStateChanged()
    {
        CheckUser();
    }

    private void CheckUser()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
        
    }

    private void OnDestroy()
    {
        RegistrationButton.OnUserRegistered -= HandleAuthStateChanged;
        LogInButton.OnUserLogedIn -= HandleAuthStateChanged;
    }
}
