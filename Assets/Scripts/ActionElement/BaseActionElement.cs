using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionElement : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> heads;
    [SerializeField]
    protected GameObject body;
    [SerializeField]
    private float growSpeed;
    [SerializeField]
    protected float returnMoveDuration;
    [SerializeField]
    protected LayerMask layerMask;

    public bool isDragged { get; internal set; } = false;

    protected Vector3 initialPosition;

    protected virtual void Start()
    {
        initialPosition = gameObject.transform.position;
    }


    protected virtual void Update()
    {
        if (!isDragged)
        {
            DetectTileBelow();
        }
    }

    protected virtual void GrowElement()
    {

    }

    protected virtual void DetectTileBelow()
    {

    }

    protected virtual void FixElement(GameObject tile)
    {
        gameObject.transform.position = new Vector3
            (
                tile.transform.position.x,
                gameObject.transform.position.y,
                tile.transform.position.z
            );
        gameObject.tag = "Untagged";
        isDragged = false;
    }

    protected void DisableTargetObject(GameObject target)
    {
        TileController controller = target.GetComponentInParent<TileController>();
        controller.RemoveTargetObject();
    }

    //protected IEnumerator MoveToInitialPosition(GameObject gameObject, Vector3 endPos, float moveDuration)
    //{
    //    Vector3 startPos = gameObject.transform.position;
    //    float timeElapsed = 0;
    //    while (timeElapsed < moveDuration)
    //    {
    //        transform.position = Vector3.Lerp(startPos, endPos, timeElapsed / moveDuration);
    //        timeElapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = endPos;
    //}
}
