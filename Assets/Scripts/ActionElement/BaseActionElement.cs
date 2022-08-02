using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionElement : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]
    protected List<ActionElementHead> heads;
    [SerializeField]
    protected GameObject body;

    [Header("Parameters")]
    [SerializeField]
    protected ActionElementParameters parameters;
    [SerializeField]
    internal LayerMask layerMask;

    public bool isDragged { get; internal set; } = false;
    public bool isFixed { get; internal set; } = false;

    protected Vector3 initialScale;
    protected Coroutine MoveHeadCoroutine;

    protected virtual void Start()
    {
        initialScale = body.transform.localScale;
    }

    private void Update()
    {
        if (!isDragged)
        {
            DetectTilesBelow();
        }
        if (isFixed)
        {
            DetectHeadsPositionChange();
        }
    }

    // ------------- Virtual Methods ------------- 

    protected virtual void DetectTilesBelow()
    {

    }

    protected virtual void DetectTilesInfront()
    {

    }

    protected virtual void DetectHeadsPositionChange()
    {

    }

    // ------------- Base Methods ------------- 

    protected bool DetectSingleTileBelow(out RaycastHit hitOut)
    {
        if (Physics.Raycast(body.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hit, 2f, layerMask))
        {
            hitOut = hit;
            if (hit.collider.CompareTag("Target"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            hitOut = hit;
            return false;
        }
    }

    protected void FixElement(GameObject tile)
    {
        gameObject.transform.position = new Vector3
            (
                tile.transform.position.x,
                gameObject.transform.position.y,
                tile.transform.position.z
            );
        gameObject.tag = "Untagged";
        isFixed = true;
        foreach (var item in heads)
        {
            item.SetColliderState(false);
        }
        DetectTilesInfront();
    }

    internal void DisableTargetObject(GameObject target)
    {
        TileController controller = target.GetComponentInParent<TileController>();
        controller.RemoveTargetObject();
    }
}
