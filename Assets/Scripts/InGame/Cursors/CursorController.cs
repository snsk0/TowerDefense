using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Cursors
{
    public class CursorController
    {
        public void SetVisibleCursoe(bool value)
        {
            if (value)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = value;
        }
    }
}

