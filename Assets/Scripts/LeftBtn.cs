using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LeftBtn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool pointerDown;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            playerMovement.MoveLeft();
        }
    }

    public void OnPointerDown(PointerEventData evenetData)
    {
        pointerDown = true;
    }
    public void OnPointerUp(PointerEventData evenetData)
    {
        pointerDown = false;
    }

}
