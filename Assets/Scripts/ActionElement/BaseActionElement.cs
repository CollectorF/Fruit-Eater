using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum ElementType
//{
//    Horizontal,
//    Vertical,
//    Angle,
//    Triple
//}

public class BaseActionElement : MonoBehaviour
{
    //[SerializeField]
    //private ElementType type;
    [SerializeField]
    private List<GameObject> heads;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private float growSpeed;
    [SerializeField]
    private LayerMask layerMask;

    protected virtual void Update()
    {
        DetectTile();
    }

    protected virtual void GrowElement()
    {

    }

    protected virtual void DetectTile()
    {
        //Physics.Raycast(body.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hitBody, 2f, layerMask);
        //Debug.DrawRay(body.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, Color.red, 2f);
        //Debug.Log(hitBody.collider.tag);
        //foreach (var item in heads)
        //{
        //    Physics.Raycast(item.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out RaycastHit hitHead, 2f, layerMask);
        //    Debug.Log(hitHead.collider.tag);
        //}
    }
}
