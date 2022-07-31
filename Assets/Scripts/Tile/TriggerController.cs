using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    public delegate void TriggerEnterEvent();
    public delegate void TriggerStayEvent();
    public delegate void TriggerExitEvent();

    public event TriggerEnterEvent OnTriggerEnterEvent;
    public event TriggerStayEvent OnTriggerStayEvent;
    public event TriggerExitEvent OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActiveElement"))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ActiveElement"))
        {
            OnTriggerStayEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ActiveElement"))
        {
            OnTriggerExitEvent?.Invoke();
        }
    }
}
