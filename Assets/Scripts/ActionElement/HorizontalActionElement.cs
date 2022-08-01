using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalActionElement : BaseActionElement
{
    protected override void DetectTileBelow()
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
}
