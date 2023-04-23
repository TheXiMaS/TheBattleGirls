using UnityEngine;

public static class KeyboardInput
{
    public static float GetCustomRawHorizontal(KeyCode leftButton, KeyCode rightButton)
    {
        if (Input.GetKey(leftButton) == true && Input.GetKey(rightButton) == false)
        {
            return -1f;
        }
        else if (Input.GetKey(leftButton) == false && Input.GetKey(rightButton) == true)
        {
            return 1f;
        }
        else
        {
            return 0;
        }
    }

    public static float GetCustomRawVertical(KeyCode upButton, KeyCode downButton)
    {
        if (Input.GetKey(upButton) == true && Input.GetKey(downButton) == false)
        {
            return 1f;
        }
        else if (Input.GetKey(upButton) == false && Input.GetKey(downButton) == true)
        {
            return -1f;
        }
        else
        {
            return 0;
        }
    }
}
