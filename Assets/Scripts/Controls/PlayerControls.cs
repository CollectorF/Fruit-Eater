using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private InputAction clickInputAction;
    [SerializeField]
    private LayerMask layerMask;

    private Camera mainCamera;
    private Coroutine dragCoroutine;

    private void Awake()
    {
        mainCamera = Camera.main;
        clickInputAction.Enable();
        clickInputAction.performed += OnClick;
    }

    private void OnDisable()
    {
        clickInputAction.Disable();
        clickInputAction.performed -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext callback)
    {
        //if (!levelFinished)
        {
#if UNITY_ANDROID || UNITY_IOS
            Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
#elif UNITY_STANDALONE
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
#endif
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                if (hit.collider.CompareTag("Dragable"))
                {
                    dragCoroutine = StartCoroutine(DragUpdate(hit.collider.gameObject));
                }
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        BaseActionElement currentElement = clickedObject.GetComponent<BaseActionElement>();
        currentElement.isDragged = true;
        while (clickInputAction.ReadValue<float>() != 0)
        {
#if UNITY_ANDROID || UNITY_IOS
            Vector3 point = mainCamera.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
#elif UNITY_STANDALONE
            Vector3 point = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
#endif
            clickedObject.transform.position = new Vector3(point.x, clickedObject.transform.position.y, point.z);
            yield return null;
        }
        currentElement.isDragged = false;
    }
}
