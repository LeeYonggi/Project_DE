using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterUI_ArrangementSlot : BaseBehaviour
{
    private EventTrigger eventTrigger = null;
    private GameObject slot = null;
    private RectTransform rectTransform = null;
    private Vector3 enterPoint = Vector3.zero;


    private void Awake()
    {
        slot = transform.Find("Slot").gameObject;

        eventTrigger = slot.GetComponent<EventTrigger>();

        rectTransform = GetComponent<RectTransform>();

        // 포인터 다운 직후
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();

        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener(SlotPointerEnter);
        eventTrigger.triggers.Add(pointerEnterEntry);

        // 드래그시
        EventTrigger.Entry dragEntry = new EventTrigger.Entry();

        dragEntry.eventID = EventTriggerType.Drag;
        dragEntry.callback.AddListener(SlotDrag);
        eventTrigger.triggers.Add(dragEntry);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

    }

    private void SlotPointerEnter(BaseEventData data)
    {
        Debug.Log("SlotPointerEnter");

        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        enterPoint = rectTransform.anchoredPosition;
    }

    private void SlotDrag(BaseEventData data)
    {
        Vector2 mousePos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), // I will Fix it because cost too high - Lee Yonggi 2021/03/10
            Input.mousePosition, 
            UIManager.Instance.CurrentCamera, 
            out mousePos);

        rectTransform.localPosition = mousePos;
    }
}
