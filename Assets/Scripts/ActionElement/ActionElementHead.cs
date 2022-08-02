using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionElementHead : MonoBehaviour
{
    private BaseActionElement mainActionController;
    private Collider collider;

    private void Awake()
    {
        mainActionController = GetComponentInParent<BaseActionElement>();
        collider = GetComponent<Collider>();
    }

    internal bool DetectSingleTileBelow(out RaycastHit hitOut)
    {
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hit, 2f, mainActionController.layerMask))
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

    internal void SetColliderState(bool state)
    {
        collider.enabled = state;
    }

    internal IEnumerator MoveHead(Vector3 direction, float moveDuration)
    {
        while(Physics.Raycast(gameObject.transform.position, direction, out RaycastHit hit, 1.5f, mainActionController.layerMask))
        {
            Vector3 startPoint = gameObject.transform.position;
            Vector3 targetPoint = new Vector3
                (
                    hit.collider.gameObject.transform.position.x,
                    gameObject.transform.position.y,
                    hit.collider.gameObject.transform.position.z
                );
            float timeElapsed = 0;
            while (timeElapsed < moveDuration)
            {
                gameObject.transform.position = Vector3.Lerp(startPoint, targetPoint, timeElapsed / moveDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            gameObject.transform.position = targetPoint;
            mainActionController.DisableTargetObject(hit.collider.gameObject);
        }
    }
}
