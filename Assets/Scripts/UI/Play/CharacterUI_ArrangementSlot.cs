using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CharacterUI_ArrangementSlot : BaseBehaviour
{
    private EventTrigger eventTrigger = null;
    private GameObject slot = null;
    private RectTransform rectTransform = null;
    private Vector3 enterPoint = Vector3.zero;

    private Vector2 initAnchoredPos = Vector2.zero;

    private RectTransform uiFormRectTransform = null;

    public RectTransform UiFormRectTransform { get => uiFormRectTransform; set => uiFormRectTransform = value; }

    private void Awake()
    {
        slot = transform.Find("Slot").gameObject;

        eventTrigger = slot.GetComponent<EventTrigger>();

        rectTransform = GetComponent<RectTransform>();

        // 포인터 다운 직후
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();

        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener(SlotPointerDown);
        eventTrigger.triggers.Add(pointerDownEntry);

        // 드래그시
        EventTrigger.Entry dragEntry = new EventTrigger.Entry();

        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener(SlotDrag);
        eventTrigger.triggers.Add(dragEntry);

        // 포인터 뗀 직후
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();

        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener(SlotPointerUp);
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    // Start is called before the first frame update
    void Start()
    {
        initAnchoredPos = rectTransform.anchoredPosition;
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

    }

    private void SlotPointerDown(BaseEventData data)
    {
        Debug.Log("SlotPointerEnter");

        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        enterPoint = rectTransform.anchoredPosition;

        IngameScene.Instance.PlaceObjforCard(true);
    }

    private void SlotDrag(BaseEventData data)
    {
        Vector2 mousePos = Vector2.zero;

        RectTransform tempUiFormRectTransform = 
            (uiFormRectTransform == null) ? transform.parent.GetComponent<RectTransform>() : uiFormRectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tempUiFormRectTransform, 
            Input.mousePosition, 
            UIManager.Instance.CurrentCamera, 
            out mousePos);

        rectTransform.localPosition = mousePos;
    }

    private void SlotPointerUp(BaseEventData data)
    {
        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f).SetEase(Ease.InSine);

        rectTransform.DOAnchorPos(initAnchoredPos, 0.4f).SetEase(Ease.InSine);

        IngameScene.Instance.PlaceObjforCard(false);
    }
}
