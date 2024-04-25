using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    public void CloseNotificationPanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    
}
