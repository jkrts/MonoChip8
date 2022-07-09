using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UIChipScreen
{
    int xPos;
    int yPos;
    int width;
    int height;
    int zDepth;

    int pixelSize;
    int startX;
    int startY;
    Color pixelColor;
    Texture2D pixel;

    bool grid;

    public UIChipScreen(GraphicsDeviceManager gdm)
    {
        xPos = 0;
        yPos = 0;
        width = 640;
        height = 320;
        zDepth = 1;
        pixelSize = width / 64; // 10
        startX = 300;
        startY = 26;
        pixelColor = Color.Black;

        pixel = new Texture2D(gdm.GraphicsDevice, 1, 1);
        Color[] colorData = {Color.White};
        pixel.SetData<Color>(colorData);

        grid = true;
}

    public void Draw(SpriteBatch sb, Chip8 ch8)
    {
        xPos = startX;
        yPos = startY;

        sb.Begin();
        for(int rows = 0; rows < 32; rows++)
        {
            for(int cols = 0; cols < 64; cols++)
            {
                if(ch8.display[cols, rows] > 0)
                {
                    pixelColor = Color.White;
                }
                else
                {
                    pixelColor = Color.Black;
                }

                sb.Draw(pixel, new Rectangle(xPos, yPos, pixelSize, pixelSize), pixelColor);

                xPos += pixelSize;
            }
            xPos = startX;
            yPos += pixelSize;
        }
        sb.End();

        if(grid) drawGrid(sb, ch8);
    }

    void drawGrid(SpriteBatch sb, Chip8 ch8)
    {
        xPos = startX;
        yPos = startY;

        Color gridColor = new Color(40,40,40);

        sb.Begin();
        for(int rows = 0; rows < 32+1; rows++)
        {
            sb.Draw(pixel, new Rectangle(xPos, yPos, width, 1), gridColor);
            yPos += pixelSize;
        }

        xPos = startX;
        yPos = startY;
        for(int cols = 0; cols < 64+1; cols++)
        {
            sb.Draw(pixel, new Rectangle(xPos, yPos, 1, height), gridColor);
            xPos += pixelSize;
        }
            
        sb.End();
    }
}