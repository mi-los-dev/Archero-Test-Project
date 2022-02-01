using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystickFix : FloatingJoystick
{
    protected override void Start()
    {
        base.Start();
        OnPointerDown(new PointerEventData(null));
        OnPointerUp(new PointerEventData(null));
    }
}
