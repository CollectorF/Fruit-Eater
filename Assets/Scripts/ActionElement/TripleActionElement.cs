using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleActionElement : BaseActionElement
{
    [Header("Additional elements")]
    [SerializeField]
    private GameObject ghostBody;

    private Vector3 initialGhostScale;


    protected override void Start()
    {
        base.Start();
        initialGhostScale = ghostBody.transform.localScale;
    }

    protected override void DetectTilesBelow()
    {
        if (heads[0].DetectSingleTileBelow(out RaycastHit hitHead1) && heads[1].DetectSingleTileBelow(out RaycastHit hitHead2) && heads[2].DetectSingleTileBelow(out RaycastHit hitHead3) && DetectSingleTileBelow(out RaycastHit hitBody))
        {
            FixElement(hitBody.collider.gameObject);
            DisableTargetObject(hitBody.collider.gameObject);
            DisableTargetObject(hitHead1.collider.gameObject);
            DisableTargetObject(hitHead2.collider.gameObject);
            DisableTargetObject(hitHead3.collider.gameObject);
        }
    }

    protected override void DetectTilesInfront()
    {
        StartCoroutine(heads[0].MoveHead(heads[0].transform.right, growSpeed));
        StartCoroutine(heads[1].MoveHead(heads[1].transform.right, growSpeed));
        StartCoroutine(heads[2].MoveHead(heads[2].transform.right, growSpeed));
    }

    protected override void DetectHeadsPositionChange()
    {
        if (heads[0].transform.hasChanged || heads[1].transform.hasChanged)
        {
            float distance = Vector3.Distance(heads[0].transform.position, heads[1].transform.position);
            body.transform.localScale = new Vector3(distance, initialScale.y, initialScale.z);

            Vector3 middlePoint = (heads[0].transform.position + heads[1].transform.position) / 2f;
            body.transform.position = middlePoint;
        }

        if (heads[2].transform.hasChanged)
        {
            float distance = Vector3.Distance(body.transform.position, heads[2].transform.position);
            ghostBody.transform.localScale = new Vector3(initialGhostScale.x, initialGhostScale.y, distance);

            float middlePoint = (body.transform.position.x + heads[2].transform.position.x) / 2f;
            ghostBody.transform.position = new Vector3(middlePoint, ghostBody.transform.position.y, ghostBody.transform.position.z);
        }
    }
}
