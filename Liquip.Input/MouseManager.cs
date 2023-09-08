using System.Drawing;

namespace Liquip.Input;

public class MouseManager
{
    private static uint _nextMouseId = 0;
    public static  uint GetNextMouseId() => _nextMouseId++;

    public static uint ScreenHeight { get; set; } = 0;
    public static uint ScreenWidth { get; set; } = 0;

    public static MouseBase GetMouse(uint mouseId)
    {
        return new MouseBase() { };
    }


    public void HandleMouse(uint mouseId, int deltaX, int deltaY, int mouseState, int scrollWheel)
    {
        // Mouse should be disabled if nothing has been set.
        if (ScreenHeight == 0 || ScreenWidth == 0)
        {
            return;
        }

        var mouse = GetMouse(mouseId);

        // Assign new delta values.

        // mouse.DeltaX = deltaX;
        // mouse.DeltaY = deltaY;
        //
        // mouse.X = (uint)Math.Clamp(mouse.X + (mouse.MouseSensitivity * deltaX), 0, ScreenWidth - 1);
        // mouse.Y = (uint)Math.Clamp(mouse.Y + (mouse.MouseSensitivity * deltaY), 0, ScreenHeight - 1);
        // mouse.LastMouseState = MouseState;
        // mouse.MouseState = (MouseState)mouseState;
    }

}
