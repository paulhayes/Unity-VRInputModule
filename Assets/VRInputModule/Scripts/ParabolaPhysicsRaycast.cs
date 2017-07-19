using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ParabolaPhysicsRaycast : BaseRaycaster
{
    public override Camera eventCamera
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        throw new NotImplementedException();
    }
}
