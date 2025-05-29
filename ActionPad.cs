using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPad : MonoBehaviour, IPointerDownHandler {

    PlayerController player;
    PlayerRayCast playerRayCast;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerRayCast = Camera.main.GetComponent<PlayerRayCast>();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        if(ped.selectedObject.name == "JumpPad")
        {
            player.Jump();
        }
        if (ped.selectedObject.name == "LiftPad")
        {
            playerRayCast.liftObject();
        }
        if (ped.selectedObject.name == "ThrowPad")
        {
            playerRayCast.throwObject();
        }
    }
}
