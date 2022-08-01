using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer baseMeshRenderer;
    [SerializeField]
    private GameObject targetObject;

    private TriggerController triggerController;
    private bool isActive;

    public delegate void TargetRemovedEvent();

    public event TargetRemovedEvent OnTargetRemove;

    private void OnEnable()
    {
        triggerController = GetComponentInChildren<TriggerController>();
        triggerController.OnTriggerEnterEvent += HighlightBase;
        triggerController.OnTriggerStayEvent += HighlightBase;
        triggerController.OnTriggerExitEvent += DehighlightBase;
        isActive = true;
        targetObject.SetActive(true);
    }

    private void OnDisable()
    {
        triggerController.OnTriggerEnterEvent -= HighlightBase;
        triggerController.OnTriggerStayEvent -= HighlightBase;
        triggerController.OnTriggerExitEvent -= DehighlightBase;
    }

    private void HighlightBase()
    {
        if (isActive)
        {
            baseMeshRenderer.material.color = Color.gray;
        }
    }

    private void DehighlightBase()
    {
        baseMeshRenderer.material.color = Color.white;
    }

    internal void RemoveTargetObject()
    {
        targetObject.SetActive(false);
        DehighlightBase();
        OnTargetRemove?.Invoke();
    }
}
