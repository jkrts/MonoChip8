using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class UIChipState
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

    public UIChipState(GraphicsDeviceManager gdm)
    {
        xPos = 0;
        yPos = 0;
        width = 280;
        height = 675;
        zDepth = 1;
        pixelSize = width / 64; // 10
        startX = 5;
        startY = 26;
        pixelColor = Color.Black;

        pixel = new Texture2D(gdm.GraphicsDevice, 1, 1);
        Color[] colorData = {Color.White};
        pixel.SetData<Color>(colorData);
}

    public void Draw(SpriteBatch sb, Chip8 ch8, SpriteFont font)
    {
        xPos = startX;
        yPos = startY;

        string head = "PC:\nI:\nV0:\nV1:\nV2:\nV3:\nV4:\nV5:\nV6:\nV7:\nV8:\nV9:\nVA:\nVB:\nVC:\nVD:\nVE:\nVF:\nST:\nDT:\nSP:\nS0:\nS1:\nS2:\nS3:\nS4:\nS5:\nS6:\nS7:\nS8:\nS9:\nSA:\nSB:\nSC:\nSD:\nSE:\nSF:\n";
        string hex = ch8.PC.ToString("X4") + "\n" +
                    ch8.I.ToString("X4") + "\n" +
                    ch8.V[0x0].ToString("X2") + "\n" +
                    ch8.V[0x1].ToString("X2") + "\n" +
                    ch8.V[0x2].ToString("X2") + "\n" +
                    ch8.V[0x3].ToString("X2") + "\n" +
                    ch8.V[0x4].ToString("X2") + "\n" +
                    ch8.V[0x5].ToString("X2") + "\n" +
                    ch8.V[0x6].ToString("X2") + "\n" +
                    ch8.V[0x7].ToString("X2") + "\n" +
                    ch8.V[0x8].ToString("X2") + "\n" +
                    ch8.V[0x9].ToString("X2") + "\n" +
                    ch8.V[0xA].ToString("X2") + "\n" +
                    ch8.V[0xB].ToString("X2") + "\n" +
                    ch8.V[0xC].ToString("X2") + "\n" +
                    ch8.V[0xD].ToString("X2") + "\n" +
                    ch8.V[0xE].ToString("X2") + "\n" +
                    ch8.V[0xF].ToString("X2") + "\n" +
                    ch8.soundTimer.ToString("X2") + "\n" +
                    ch8.delayTimer.ToString("X2") + "\n" +
                    ch8.SP.ToString("X2") + "\n" +
                    ch8.stack[0x0].ToString("X4") + "\n" +
                    ch8.stack[0x1].ToString("X4") + "\n" +
                    ch8.stack[0x2].ToString("X4") + "\n" +
                    ch8.stack[0x3].ToString("X4") + "\n" +
                    ch8.stack[0x4].ToString("X4") + "\n" +
                    ch8.stack[0x5].ToString("X4") + "\n" +
                    ch8.stack[0x6].ToString("X4") + "\n" +
                    ch8.stack[0x7].ToString("X4") + "\n" +
                    ch8.stack[0x8].ToString("X4") + "\n" +
                    ch8.stack[0x9].ToString("X4") + "\n" +
                    ch8.stack[0xA].ToString("X4") + "\n" +
                    ch8.stack[0xB].ToString("X4") + "\n" +
                    ch8.stack[0xC].ToString("X4") + "\n" +
                    ch8.stack[0xD].ToString("X4") + "\n" +
                    ch8.stack[0xE].ToString("X4") + "\n" +
                    ch8.stack[0xF].ToString("X4");

        string dec = ch8.PC.ToString() + "\n" +
                    ch8.I.ToString() + "\n" +
                    ch8.V[0x0].ToString() + "\n" +
                    ch8.V[0x1].ToString() + "\n" +
                    ch8.V[0x2].ToString() + "\n" +
                    ch8.V[0x3].ToString() + "\n" +
                    ch8.V[0x4].ToString() + "\n" +
                    ch8.V[0x5].ToString() + "\n" +
                    ch8.V[0x6].ToString() + "\n" +
                    ch8.V[0x7].ToString() + "\n" +
                    ch8.V[0x8].ToString() + "\n" +
                    ch8.V[0x9].ToString() + "\n" +
                    ch8.V[0xA].ToString() + "\n" +
                    ch8.V[0xB].ToString() + "\n" +
                    ch8.V[0xC].ToString() + "\n" +
                    ch8.V[0xD].ToString() + "\n" +
                    ch8.V[0xE].ToString() + "\n" +
                    ch8.V[0xF].ToString() + "\n" +
                    ch8.soundTimer.ToString() + "\n" +
                    ch8.delayTimer.ToString() + "\n" +
                    ch8.SP.ToString() + "\n" +
                    ch8.stack[0x0].ToString() + "\n" +
                    ch8.stack[0x1].ToString() + "\n" +
                    ch8.stack[0x2].ToString() + "\n" +
                    ch8.stack[0x3].ToString() + "\n" +
                    ch8.stack[0x4].ToString() + "\n" +
                    ch8.stack[0x5].ToString() + "\n" +
                    ch8.stack[0x6].ToString() + "\n" +
                    ch8.stack[0x7].ToString() + "\n" +
                    ch8.stack[0x8].ToString() + "\n" +
                    ch8.stack[0x9].ToString() + "\n" +
                    ch8.stack[0xA].ToString() + "\n" +
                    ch8.stack[0xB].ToString() + "\n" +
                    ch8.stack[0xC].ToString() + "\n" +
                    ch8.stack[0xD].ToString() + "\n" +
                    ch8.stack[0xE].ToString() + "\n" +
                    ch8.stack[0xF].ToString();
        
        string bin = Convert.ToString(ch8.PC, 2).PadLeft(16,'0') + "\n" +
                    Convert.ToString(ch8.I, 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.V[0x0], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x1], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x2], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x3], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x4], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x5], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x6], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x7], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x8], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0x9], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0xA], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0xB], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0xC], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0xD], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0xE], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.V[0xF], 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.soundTimer, 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.delayTimer, 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.SP, 2).PadLeft(8, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x0], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x1], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x2], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x3], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x4], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x5], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x6], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x7], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x8], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0x9], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0xA], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0xB], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0xC], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0xD], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0xE], 2).PadLeft(16, '0') + "\n" +
                    Convert.ToString(ch8.stack[0xF], 2).PadLeft(16, '0');

        sb.Begin();
        sb.Draw(pixel, new Rectangle(xPos, yPos, width, height), pixelColor);
        sb.DrawString(font, head, new Vector2(startX+10, startY+10), Color.White);
        sb.DrawString(font, hex, new Vector2(startX+40, startY+10), Color.White);
        sb.DrawString(font, dec, new Vector2(startX+90, startY+10), Color.White);
        sb.DrawString(font, bin, new Vector2(startX+150, startY+10), Color.White);
        sb.End();
    }

}