using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootJoyPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Image bgJoy;
    Image joy;
    Vector3 joyPos;
    Vector3 cameraRotation;
    Vector3 playerRotation;
    Camera camera;
    public PlayerController player;
    public float sensitivity;
    bool joyUp;
    public Fire fire;

    void Start()
    {
        bgJoy = GetComponent<Image>();
        joy = transform.GetChild(0).GetComponent<Image>();
        camera = Camera.main;
    }

    void FixedUpdate()
    {
        if (joyUp) { return; }
        cameraRotation = new Vector3(joyPos.z * -1f + cameraRotation.x, 0f, 0f);
        playerRotation = new Vector3(0f, joyPos.x + playerRotation.y, 0f);
        camera.transform.localRotation = Quaternion.Euler(cameraRotation);
        player.transform.rotation = Quaternion.Euler(playerRotation);
        //FIRE
        fire.shoot();
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        joyUp = false;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgJoy.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / bgJoy.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgJoy.rectTransform.sizeDelta.y);

            joyPos = new Vector3(pos.x * 2f + 1f, 0f, pos.y * 2f - 1f);
            joyPos = (joyPos.magnitude > 1f) ? joyPos.normalized : joyPos;

            joy.rectTransform.anchoredPosition = new Vector3(
                joyPos.x * (bgJoy.rectTransform.sizeDelta.x / 2),
                joyPos.z * (bgJoy.rectTransform.sizeDelta.y / 2));
            //Debug.Log(inputVector);
        }
    }
    public void OnPointerUp(PointerEventData ped)
    {
        joyPos = Vector3.zero;
        joyUp = true;
        //Debug.Log(inputVector);
        joy.rectTransform.anchoredPosition = Vector3.zero;
    }
    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
}