using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer baseMeshRenderer;

    private TriggerController triggerController;
    private bool isActive;

    private void Start()
    {
        triggerController = GetComponentInChildren<TriggerController>();
        triggerController.OnTriggerEnterEvent += HighlightBase;
        triggerController.OnTriggerStayEvent += HighlightBase;
        triggerController.OnTriggerExitEvent += DehighlightBase;
    }

    private void HighlightBase()
    {
        baseMeshRenderer.material.color = Color.gray;
    }

    private void DehighlightBase()
    {
        baseMeshRenderer.material.color = Color.white;
    }
}
