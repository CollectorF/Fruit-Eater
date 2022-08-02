using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleActionElement : BaseActionElement
{
    [Header("Additional Elements")]
    [SerializeField]
    private GameObject proxyBody1;
    [SerializeField]
    private GameObject proxyBody2;
    [SerializeField]
    private Transform basePoint1;
    [SerializeField]
    private Transform basePoint2;

    protected override void DetectTilesBelow()
    {
        if (heads[0].DetectSingleTileBelow(out RaycastHit hitHead1) && heads[1].DetectSingleTileBelow(out RaycastHit hitHead2) && DetectSingleTileBelow(out RaycastHit hitBody))
        {
            FixElement(hitBody.collider.gameObject);
            DisableTargetObject(hitBody.collider.gameObject);
            DisableTargetObject(hitHead1.collider.gameObject);
            DisableTargetObject(hitHead2.collider.gameObject);
        }
    }

    protected override void DetectTilesInfront()
    {
        StartCoroutine(heads[0].MoveHead(heads[0].transform.right, parameters.GrowSpeed));
        StartCoroutine(heads[1].MoveHead(heads[1].transform.right, parameters.GrowSpeed));
    }

    protected override void DetectHeadsPositionChange()
    {
        if (heads[0].transform.hasChanged)
        {
            float distance = Vector3.Distance(heads[0].transform.position, basePoint1.position);
            proxyBody1.transform.localScale = new Vector3(initialScale.x, initialScale.y, distance);

            Vector3 middlePoint = (heads[0].transform.position + basePoint1.position) / 2f;
            proxyBody1.transform.position = middlePoint;
        }
        if (heads[1].transform.hasChanged)
        {
            float distance = Vector3.Distance(heads[1].transform.position, basePoint2.position);
            proxyBody2.transform.localScale = new Vector3(initialScale.x, initialScale.y, distance);

            Vector3 middlePoint = (heads[1].transform.position + basePoint2.position) / 2f;
            proxyBody2.transform.position = middlePoint;
        }
    }
}
