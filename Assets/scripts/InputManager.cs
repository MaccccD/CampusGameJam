using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public class Axes
    {
        public const string VERTICAL_AXIS = "Vertical";
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string RIGHT_VERTICAL_AXIS = "RightVertical";
        public const string RIGHT_HORIZONTAL_AXIS = "RightHorizontal";

        public const string MOUSE_X_AXIS = "Mouse X";
        public const string MOUSE_Y_AXIS = "Mouse Y";

        public const string FIRE_1 = "RightTrigger";
    }

    public class Buttons
    {
        public const string JUMP = "Jump";
        public const string DODGE = "Dodge";
        public const string FIRE_1 = "Fire1";
        public const string RELOAD = "Reload";
        public const string ALT_FIRE1 = "AltFire1";
    }
}
