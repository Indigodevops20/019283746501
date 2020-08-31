using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Joystick_move : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joystickImg;
    private Vector2 InputVector;
    // Start is called before the first frame update
    void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
            InputVector = new Vector2(pos.x,pos.y);
            InputVector = (InputVector.magnitude > 1.0f)?InputVector.normalized:InputVector;
            
            //move dot of Joystick
            joystickImg.rectTransform.anchoredPosition = new Vector3(InputVector.x* (bgImg.rectTransform.sizeDelta.x/2), InputVector.y* (bgImg.rectTransform.sizeDelta.y/2));
        }
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputVector = Vector2.zero;
        joystickImg.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public float Horizontal()
    {
        if(InputVector.x != 0)
        {
            return InputVector.x;
        }else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float Vertical()
    {
        if(InputVector.y != 0)
        {
            Debug.Log(InputVector.y);
            return InputVector.y;
        }else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
