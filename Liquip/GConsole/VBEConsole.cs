using System;
using System.Collections.Generic;
using Cosmos.HAL;
using Liquip.Memory;

namespace Liquip.GConsole;

public struct CharData
{
    public char Char { get; set; }
    public ConsoleColor Foreground { get; set; }
    public ConsoleColor Background { get; set; }
}

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class VBEConsole : TextScreenBase, IDisposable
{
    private Pointer _frameBuffer;

    public Point CursorPos = new() { X = 0, Y = 0 };

    protected int PixelDepth = 0;
    public List<CharData[]> TextBuffer = new();

    public override ushort Cols { get; set; }
    public override ushort Rows { get; set; }

    public override byte this[int x, int y]
    {
        get => (byte)TextBuffer[^y][x].Char;
        set
        {
            var item = TextBuffer[^y][x];
            item.Char = (char)value;
            item.Background = Background;
            item.Foreground = Foreground;
            DrawLine(TextBuffer.Count - (y + 1), y);
        }
    }

    public int Size => (int)_frameBuffer.Size;

    public void Dispose()
    {
        // If the buffer is allocated, free it.
        if (_frameBuffer.Size != 0)
        {
            _frameBuffer.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    public override void Clear()
    {
        TextBuffer.Clear();
        for (var i = 0; i < Rows; i++)
        {
            TextBuffer.Add(new CharData[Cols]);
        }
    }

    public override void SetColors(ConsoleColor aForeground, ConsoleColor aBackground)
    {
        throw new NotImplementedException();
    }

    public override byte GetColor()
    {
        throw new NotImplementedException();
    }

    public override void SetCursorPos(int x, int y)
    {
        throw new NotImplementedException();
    }

    public override void ScrollUp()
    {
        if (TextBuffer.Count == Rows)
        {
            TextBuffer.Insert(0, new CharData[Cols]);
        }

        TextBuffer.RemoveAt(TextBuffer.Count);
        var bufferIndex = TextBuffer.Count - Rows;
        if (bufferIndex < 0)
        {
            bufferIndex = 0;
        }

        DrawLine(bufferIndex, 0);


        CursorPos.Y--;
        if (CursorPos.Y <= 0)
        {
            CursorPos.Y = 0;
        }
    }

    public override int GetCursorSize()
    {
        throw new NotImplementedException();
    }

    public override void SetCursorSize(int value)
    {
        throw new NotImplementedException();
    }

    public override bool GetCursorVisible()
    {
        throw new NotImplementedException();
    }

    public override void SetCursorVisible(bool value)
    {
        throw new NotImplementedException();
    }

    protected int BufferOffset(int x, int y)
    {
        if (x > Cols || x < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }

        if (y > Rows || y < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }

        return Cols * y + x;
    }

    public void DrawLine(int bufferLine, int consoleLine)
    {
        var buffer = Render(TextBuffer[bufferLine]);
        BufferUtils.MemoryCopy(buffer, _frameBuffer, (uint)BufferOffset(0, consoleLine));
        buffer.Free();
    }

    public virtual Pointer Render(
        CharData[] c
    )
    {
        return Pointer.Null;
    }

    public virtual Pointer Render(
        CharData c
    )
    {
        return Pointer.Null;
    }
}
