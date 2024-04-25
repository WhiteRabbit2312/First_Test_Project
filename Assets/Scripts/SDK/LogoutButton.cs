using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class LogoutButton : MonoBehaviour//, IPointerClickHandler
{
    public void OnPointerClick()//(PointerEventData eventData)
    {
        FirebaseAuth.DefaultInstance.SignOut();
        PlayerPrefs.SetInt("LogedIn", 0);
        SceneManager.LoadScene(0);
    }
}
