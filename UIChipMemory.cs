using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class UIChipMemory
{
    int xPos;
    int yPos;
    int width;
    int height;

    int pixelSize;
    int startX;
    int startY;
    Color pixelColor;
    Texture2D pixel;

    public UIChipMemory(GraphicsDeviceManager gdm)
    {
        xPos = 0;
        yPos = 0;
        width = 640;
        height = 340;
        pixelSize = width / 64; // 10
        startX = 300;
        startY = 362;
        pixelColor = Color.Black;

        pixel = new Texture2D(gdm.GraphicsDevice, 1, 1);
        Color[] colorData = {Color.White};
        pixel.SetData<Color>(colorData);

}

    public void Draw(SpriteBatch sb, Chip8 ch8, SpriteFont font)
    {
        xPos = startX;
        yPos = startY;

        int cellWidth = 20;
        

        sb.Begin();
        sb.Draw(pixel, new Rectangle(xPos, yPos, width, height), pixelColor);
        sb.DrawString(font, "MEMORY", new Vector2(xPos + 10, startY+10), Color.White);

     
        sb.DrawString(font, "PC", new Vector2(xPos + 10 + (cellWidth*0), startY+25), Color.White);
        sb.DrawString(font, "", new Vector2(xPos + 10 + (cellWidth*1), startY+25), Color.White);
        sb.DrawString(font, "", new Vector2(xPos + 10 + (cellWidth*2), startY+25), Color.White);
        sb.DrawString(font, "00", new Vector2(xPos + 10 + (cellWidth*3), startY+25), Color.White);
        sb.DrawString(font, "01", new Vector2(xPos + 10 + (cellWidth*4), startY+25), Color.White);
        sb.DrawString(font, "02", new Vector2(xPos + 10 + (cellWidth*5), startY+25), Color.White);
        sb.DrawString(font, "03", new Vector2(xPos + 10 + (cellWidth*6), startY+25), Color.White);
        sb.DrawString(font, "04", new Vector2(xPos + 10 + (cellWidth*7), startY+25), Color.White);
        sb.DrawString(font, "05", new Vector2(xPos + 10 + (cellWidth*8), startY+25), Color.White);
        sb.DrawString(font, "06", new Vector2(xPos + 10 + (cellWidth*9), startY+25), Color.White);
        sb.DrawString(font, "07", new Vector2(xPos + 10 + (cellWidth*10), startY+25), Color.White);
        sb.DrawString(font, "08", new Vector2(xPos + 10 + (cellWidth*11), startY+25), Color.White);
        sb.DrawString(font, "09", new Vector2(xPos + 10 + (cellWidth*12), startY+25), Color.White);
        sb.DrawString(font, "0A", new Vector2(xPos + 10 + (cellWidth*13), startY+25), Color.White);
        sb.DrawString(font, "0B", new Vector2(xPos + 10 + (cellWidth*14), startY+25), Color.White);
        sb.DrawString(font, "0C", new Vector2(xPos + 10 + (cellWidth*15), startY+25), Color.White);
        sb.DrawString(font, "0D", new Vector2(xPos + 10 + (cellWidth*16), startY+25), Color.White);
        sb.DrawString(font, "0E", new Vector2(xPos + 10 + (cellWidth*17), startY+25), Color.White);
        sb.DrawString(font, "0F", new Vector2(xPos + 10 + (cellWidth*18), startY+25), Color.White);

        sb.Draw(pixel, new Rectangle(xPos + 10 , startY+40,375, 1), Color.White);
        sb.Draw(pixel, new Rectangle(xPos + 10 + 45 , startY+40,1, 285), Color.White);

        ushort currentLine = (ushort)(ch8.PC - (ch8.PC % 0x10));

        int col = 0;
        int row = 0;

        for(int i = currentLine; i < currentLine + 0x130; i++)
        {
            if(currentLine > 4095) break;
            if(col == 0)
            {
                sb.DrawString(font, (currentLine+row*0x10).ToString("X4"), new Vector2(xPos + 10 + (cellWidth*col), yPos+45+(15*row)), Color.White);
            }
            if(i == ch8.PC)
            {
                sb.DrawString(font, ch8.memory[i].ToString("X2"), new Vector2(xPos +(cellWidth*3) + 10 + (cellWidth*col), yPos+45+(15*row)), Color.Yellow);
            }else
            {
                sb.DrawString(font, ch8.memory[i].ToString("X2"), new Vector2(xPos +(cellWidth*3) + 10 + (cellWidth*col), yPos+45+(15*row)), Color.White);
            }
            col += 1;
            if(col == 16)
            {
                row += 1;
                col = 0;                
            }
        }
        

        sb.End();
    }

}