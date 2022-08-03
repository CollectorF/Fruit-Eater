using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleActionElement : BaseActionElement
{
    [Header("Additional Elements")]
    [SerializeField]
    private GameObject proxyBody;
    [SerializeField]
    private Transform point;

    private Vector3 initialProxyScale;

    protected override void Start()
    {
        base.Start();
        initialProxyScale = proxyBody.transform.localScale;
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
        StartCoroutine(heads[0].MoveHead(heads[0].transform.right, parameters.GrowSpeed));
        StartCoroutine(heads[1].MoveHead(heads[1].transform.right, parameters.GrowSpeed));
        StartCoroutine(heads[2].MoveHead(heads[2].transform.right, parameters.GrowSpeed));
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
            float distance = Vector3.Distance(point.position, heads[2].transform.position);
            proxyBody.transform.localScale = new Vector3(initialProxyScale.x, initialProxyScale.y, distance);

            Vector3 middlePoint = (heads[2].transform.position + point.position) / 2f;
            proxyBody.transform.position = middlePoint;
        }
    }
}
