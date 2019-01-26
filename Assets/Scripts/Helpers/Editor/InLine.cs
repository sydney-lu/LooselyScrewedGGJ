using UnityEngine;

public class InLine
{
    static Rect position;
    static float lineHeight = 20;
    public static float LineHeight
    {
        get { return lineHeight; }
        set { lineHeight = value; }
    }

    static float indent = 0;
    static float items = 1;
    static int currentLine = 1;
    static int currentItem = 0;

    public static Rect NextRect(bool toggleButton = false)
    {
        if (currentItem == 0) GUILayout.Space(lineHeight);
        Rect temp = new Rect
        (
            position.x + indent + (currentItem * (position.width - indent) / items),
            position.y + (lineHeight * currentLine),
            (position.width - indent) / items,
            lineHeight
        );
        currentItem++;

        if (toggleButton) temp.width = 10;
        if (currentItem >= items)
        {
            currentItem = 0;
            currentLine++;
        }
        return temp;
    }
    public static void Reset()
    {
        position = new Rect();
        currentLine = 1;
        indent = 0;
        items = 1;
        currentItem = 0;
    }
    public static void SetRect(Rect newPosition, float newIndent = 0, float itemAmount = 1, bool toggleButton = false)
    {
        Reset();
        position = newPosition;
        indent = newIndent;
        items = itemAmount;
        items = Mathf.Max(1, itemAmount);
        currentItem = 0;
    }

    public static Rect GetRect(Rect newPosition, float newIndent = 0, float itemAmount = 1, bool toggleButton = false)
    {
        SetRect(newPosition, newIndent, itemAmount, toggleButton);
        return NextRect();
    }
}
