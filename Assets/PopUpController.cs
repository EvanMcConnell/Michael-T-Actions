using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PopUpController : MonoBehaviour
{
    [SerializeField] UnityEvent PopupEvents;

    private void OnEnable()
    {
        PlatformerPlayerController.Instance.isPaused = true;
    }
    public void InteractWithPopUp()
    {
        PlatformerPlayerController.Instance.isPaused = false;
        PopupEvents.Invoke();
        Destroy(this.gameObject);

    }
}
