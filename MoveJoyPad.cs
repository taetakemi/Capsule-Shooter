using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveJoyPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Image bgJoy;
    Image joy;
    Vector3 joyPos;
    public PlayerController player;

    void Start()
    {
        bgJoy = GetComponent<Image>();
        joy = transform.GetChild(0).GetComponent<Image>();
    }

    void FixedUpdate()
    {
#if UNITY_ANDROID
        player.androidMove(joyPos);
#endif
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgJoy.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / bgJoy.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgJoy.rectTransform.sizeDelta.y);

            joyPos = new Vector3(pos.x * 2f - 1f, 0f, pos.y * 2f - 1f);
            joyPos = (joyPos.magnitude > 1f) ? joyPos.normalized*player.velocity : joyPos* player.velocity;

            joy.rectTransform.anchoredPosition = new Vector3(
                joyPos.x * (bgJoy.rectTransform.sizeDelta.x / 2),
                joyPos.z * (bgJoy.rectTransform.sizeDelta.y / 2));
            //Debug.Log(inputVector);
        }
    }
    public void OnPointerUp(PointerEventData ped)
    {
        joyPos = Vector3.zero;
        //Debug.Log(inputVector);
        joy.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
}
