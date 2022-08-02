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
    protected float growSpeed = 0.3f;
    [SerializeField]
    internal LayerMask layerMask;

    public bool isDragged { get; internal set; } = false;
    public bool isFixed { get; internal set; } = false;

    protected Vector3 initialPosition;
    protected Coroutine MoveHeadCoroutine;

    private void Start()
    {
        initialPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (!isDragged)
        {
            DetectTilesBelow();
        }
    }

    // ------------- Virtual Methods ------------- 

    protected virtual void DetectTilesBelow()
    {

    }

    protected virtual void DetectTilesInfront()
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
