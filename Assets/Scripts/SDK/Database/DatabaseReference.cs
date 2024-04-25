using UnityEngine;
using Firebase.Database;

public class DatabaseReference : MonoBehaviour
{
    private static DatabaseReference _instance;

    public static DatabaseReference Instance
    {
        get
        {
            return _instance;
        }
    }

    private Firebase.Database.DatabaseReference reference;

    public Firebase.Database.DatabaseReference Reference
    {
        get;
        set;
        
    }

    void Awake()
    {

        DontDestroyOnLoad(this);

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Reference = reference;

        if (_instance == null)
        {
            
            _instance = this;
            
        }
    }
}
