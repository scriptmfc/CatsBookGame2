using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChracterSelectPopup : CBasicPopup
{
    public Action callback;
    public void onUpdateData(Action _callback = null)
    {
        callback = _callback;
    }
}
