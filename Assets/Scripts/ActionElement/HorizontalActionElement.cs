using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalActionElement : BaseActionElement
{
    protected override void DetectTilesBelow()
    {
        if (Physics.Raycast(body.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hitBody, 2f, layerMask))
        {
            if (Physics.Raycast(heads[0].transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hitHead1, 2f, layerMask))
            {
                if (Physics.Raycast(heads[1].transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hitHead2, 2f, layerMask))
                {
                    if (hitBody.collider.CompareTag("Target") && hitHead1.collider.CompareTag("Target") && hitHead2.collider.CompareTag("Target"))
                    {
                        FixElement(hitBody.collider.gameObject);
                        DisableTargetObject(hitBody.collider.gameObject);
                        DisableTargetObject(hitHead1.collider.gameObject);
                        DisableTargetObject(hitHead2.collider.gameObject);
                    }
                }
            }
        }
    }

    protected override void DetectTilesInfront()
    {
        bool hasTarget = Physics.Raycast(heads[0].transform.position, -Vector3.right, out RaycastHit hit, 1.5f, layerMask);
        if (hasTarget)
        {
            Vector3 targetPoint = new Vector3
                (
                    hit.collider.gameObject.transform.position.x,
                    heads[0].transform.position.y,
                    hit.collider.gameObject.transform.position.z
                );
            if (MoveHeadCoroutine == null)
            {
                MoveHeadCoroutine = StartCoroutine(MoveHead(heads[0], targetPoint, growSpeed, hit.collider.gameObject));
            }
        }
    }
}
