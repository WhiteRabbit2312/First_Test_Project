using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
