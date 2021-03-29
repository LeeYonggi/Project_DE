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
    private GameObject imageUI = null;
    private RectTransform rectTransform = null;
    private Vector3 enterPoint = Vector3.zero;

    private Vector2 initAnchoredPos = Vector2.zero;

    private RectTransform uiFormRectTransform = null;

    private float placePointDistance = 160.0f;

    private CharacterSlotData slotData = null;


    public static CharacterUI_ArrangementSlot Create(Transform parent, RectTransform uiRectTransform, Vector2 anchoredPos, CharacterSlotData slotData)
    {
        GameObject char_ArrSlotPrefab = AssetBundleManager.Instance.GetAsset<GameObject>("prefab/ui/ingame", "Character_ArrangementSlot");

        var tempObj = GameObject.Instantiate(char_ArrSlotPrefab, parent);
        var char_ArrSlot = tempObj.GetComponent<CharacterUI_ArrangementSlot>();

        tempObj.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        char_ArrSlot.UiFormRectTransform = uiRectTransform;
        char_ArrSlot.slotData = slotData;

        return char_ArrSlot;
    }

    public RectTransform UiFormRectTransform { get => uiFormRectTransform; set => uiFormRectTransform = value; }

    private void Awake()
    {
        slot = transform.Find("Slot").gameObject;
        imageUI = transform.Find("Slot/ImageUI").gameObject;

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
        transform.SetAsLastSibling();

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

        // 드래그 시 UI가 작아지는 연출을 위한 수식
        float increasedScreenRatio = (float)uiFormRectTransform.sizeDelta.y / (float)MainManager.Instance.FixSreenHeight;
        float ratio = 1 - (rectTransform.anchoredPosition.y - initAnchoredPos.y) / (placePointDistance * increasedScreenRatio);

        ratio = Mathf.Min(Mathf.Max(ratio, 0.3f), 1.0f);
        transform.localScale = new Vector3(ratio, ratio, ratio);

        // Raycast로 놓을 수 있는 곳인지 판단
        RaycastHit raycastHit = GetPlaceableColliderRayhit(out bool isHit);

        if(isHit)
        {
            imageUI.SetActive(false);

            IngameScene.Instance.SetDeployEffectActive("BraveKnight", true);
            IngameScene.Instance.SetDeployEffectPos("BraveKnight", raycastHit.point);
        }
        else
        {
            IngameScene.Instance.SetDeployEffectActive("BraveKnight", false);
            imageUI.SetActive(true);
        }
    }

    private void SlotPointerUp(BaseEventData data)
    {
        transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f).SetEase(Ease.InSine);

        rectTransform.DOAnchorPos(initAnchoredPos, 0.4f).SetEase(Ease.InSine);

        IngameScene.Instance.PlaceObjforCard(false);
        imageUI.SetActive(true);
    }

    private RaycastHit GetPlaceableColliderRayhit(out bool isHit)
    {
        RaycastHit[] raycastHit = IngameScene.Instance.CameraMousePointRaycast();

        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].transform.tag == "PlaceableCollider")
            {
                isHit = true;
                return raycastHit[i];
            }
        }
        isHit = false;
        return default;
    }

}
