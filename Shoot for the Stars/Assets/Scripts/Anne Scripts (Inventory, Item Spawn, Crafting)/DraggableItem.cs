using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableItem : MonoBehaviour
{
    [SerializeField] private InputActionReference clickAction; 
    [SerializeField] private InputActionReference pointerPosAction;
    [SerializeField] private RectTransform panelTransform;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private RectTransform itemTransform;
    [SerializeField] Camera uiCamera;

    private Vector2 _offset;
    private bool _isDragging;

    private void OnEnable()
    {
        clickAction.action.started += OnClickStarted;
        clickAction.action.canceled += OnClickCanceled;
        pointerPosAction.action.performed += OnPointMoved;
    }

    private void OnDisable()
    {
        clickAction.action.started -= OnClickStarted;
        clickAction.action.canceled -= OnClickCanceled;
        pointerPosAction.action.performed -= OnPointMoved;
        _isDragging = false; 
    }
    private bool IsPointerOverItem(Vector2 pointerPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(itemTransform, pointerPos, uiCamera);
    }
    private void OnClickStarted(InputAction.CallbackContext ctx)
    {
        var pointerPos = pointerPosAction.action.ReadValue<Vector2>();
        if (IsPointerOverItem(pointerPos))
        {
            _isDragging = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasTransform, pointerPos, uiCamera, out var localPos
                );

            _offset = panelTransform.anchoredPosition - localPos;
        }
    }


    private void OnClickCanceled(InputAction.CallbackContext ctx)
    {
        _isDragging = false;
    }

    private void OnPointMoved(InputAction.CallbackContext ctx)
    {
        if (!_isDragging) return;
        var pointerPos = ctx.ReadValue<Vector2>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform, pointerPos, uiCamera, out var localPos);
        panelTransform.anchoredPosition = localPos + _offset;
    }
}


