using System;

class Chip8
{
    byte[] memory;
    byte[,] display;

    byte[] V;
    ushort I;
    byte soundTimer;
    byte delayTimer;
    public ushort PC;
    byte SP;
    ushort[] stack;
    public bool drawFlag;
    byte[] keys;

    public Chip8()
    {
        memory = new byte[4096];
        InitMemory();
        display = new byte[64,32];
        InitDisplay();
        V = new byte[16];
        for(int i = 0; i < 16; i++)
        {
            V[i] = 0;
        }

        I = 0;
        soundTimer = 0;
        delayTimer = 0;
        PC = 0x200;
        SP = 0;
        stack = new ushort[16];

        for(int i = 0; i < 16; i++)
        {
            stack[i] = 0;
        }

        keys = new byte[16];
        for(int i = 0; i < 16; i++)
        {
            keys[i] = 1;
        }

        drawFlag = false;
    }

    private void InitMemory()
    {
        for(int i = 0; i < memory.Length; ++i)
        {
            memory[i] = 0x00;
        }

        byte[] fontData = {
            0xF0, 0x90, 0x90, 0x90, 0xF0,   // 0
            0x20, 0x60, 0x20, 0x20, 0x70,   // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0,   // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0,   // 3
            0x90, 0x90, 0xF0, 0x10, 0x10,   // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0,   // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0,   // 6
            0xF0, 0x10, 0x20, 0x40, 0x40,   // 7
            0x20, 0x60, 0x20, 0x20, 0x70,   // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0,   // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90,   // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0,   // B
            0xF0, 0x80, 0x80, 0x80, 0xF0,   // C
            0xE0, 0x90, 0x90, 0x90, 0xE0,   // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0,   // E
            0xF0, 0x80, 0xF0, 0x80, 0x80    // F
        };

        for(int i = 0; i < fontData.Length; ++i)
        {
            memory[0x050 + i] = fontData[i];
        }
   
    }

    public void InitDisplay()
    {
        for(int i = 0; i<32; ++i)
        {
            for(int j = 0; j < 64; ++j)
            {
                display[j,i] = 0;
            }
        }
    }
    
    public void LoadRom(string romName)
    {
        FileHandler fh = new FileHandler(romName);

        for(int i = 0; i < fh.romData.Length; ++i)
        {
            memory[0x200 + i] = fh.romData[i];
        }
    }

    public void PrintMemory()
    {
        ushort pc = 0x00;

        while(pc < memory.Length)
        {
            Console.Write("{0:X4}:", pc);

            for(int j = 0; j < 16; j++)
            {
                Console.Write(" {0:X2}", memory[pc]);
                pc++;
            }

            Console.Write("\n");
        }
    }

    public void PrintDisplay()
    {
        Console.WriteLine("\nDisplay:");
        
        for(int i = 0; i < 32; ++i)
        {
            for(int j = 0; j < 64; ++j)
            {
                if(display[j,i]==0) Console.Write("- ");
                else if(display[j,i]==1) Console.Write("1 ");
            }
            Console.Write("\n");
        }
    }

    public void EmulateCycle()
    {
        //fetch opcode
        ushort opcode = (ushort)(memory[PC] << 8 | memory[PC+1]);
        //Console.WriteLine("{0:X4}",opcode);
        //PC += 2;
        //decode opcode

        byte x, y, k = 0;

        switch(opcode & 0xF000)
        {
            case 0x0000:
                switch(opcode & 0x00FF)
                {
                    case 0x00E0:    // 00E0: CLS - Clears the display
                        for(int i = 0; i < 32; ++i)
                        {
                            for(int j = 0; j < 64; ++j)
                            {
                                display[j,i] = 0;
                            }
                        }
                        drawFlag = true;
                        PC += 2;
                        break;

                    case 0x00EE:    // 00EE: RET - Return from subroutine
                        --SP;
                        PC = stack[SP];
                        PC += 2;

                        break;
                }
                break;

            case 0x1000:    // 1nnn: JP addr - Jump to location nnn
                PC = (ushort)(opcode & 0x0FFF);

                break;
                
            case 0x2000:    // 2nnn: CALL addr - Call subroutine at nnn
                stack[SP] = PC;
                SP++;
                PC = (ushort)(opcode & 0x0FFF);

                break;

            case 0x3000:    // 3xkk: SE Vx, byte - Skip next instruction if Vx = kk
                x = (byte)(opcode >> 8 & 0x000F);
                k = (byte)(opcode & 0x00FF);

                if(V[x] == k)
                {
                    PC += 4;
                } else {
                    PC += 2;
                }

                break;

            case 0x4000:    // 4xkk: SNE Vx, byte - Skip next instruction if Vx != kk
                x = (byte)(opcode >> 8 & 0x000F);
                k = (byte)(opcode & 0x00FF);

                if(V[x] != k)
                {
                    PC += 4;
                } else {
                    PC += 2;
                }

                break;

            case 0x5000:    // 5xy0: SE Vx, Vy - Skip next instruction if Vx = Vy
                x = (byte)(opcode >> 8 & 0x000F);
                y = (byte)(opcode >> 4 & 0x000F);

                if(V[x] == V[y])
                {
                    PC += 4;
                } else {
                    PC += 2;
                }

                break;

            case 0x6000:    // 6xkk: LD Vx, byte - Set Vx = kk
                x = (byte)(opcode >> 8 & 0x000F);
                k = (byte)(opcode & 0x00FF);

                V[x] = k;
                PC += 2;

                break;

            case 0x7000:    // 7xkk: ADD Vx, byte - Set Vx = Vx + kk
                x = (byte)(opcode >> 8 & 0x000F);
                k = (byte)(opcode & 0x00FF);

                V[x] = (byte)(V[x] + k);
                PC += 2;

                break;

            case 0x8000:
                switch(opcode & 0x000F)
                {
                    case 0x0000:    // 8xy0: LD Vx, Vy - Set Vx = Vy
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        V[x] = V[y];

                        PC += 2;

                        break;

                    case 0x0001:    // 8xy1: OR Vx, Vy - Set Vx = Vx OR Vy
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        V[x] = (byte)(V[x] | V[y]);

                        PC += 2;

                        break;

                    case 0x0002:    // 8xy2: AND Vx, Vy - Set Vx = Vx AND Vy
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        V[x] = (byte)(V[x] & V[y]);

                        PC += 2;

                        break;

                    case 0x0003:    // 8xy3: XOR Vx, Vy - Set Vx = Vx XOR Vy
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        V[x] = (byte)(V[x] ^ V[y]);

                        PC += 2;

                        break;

                    case 0x0004:    // 8xy4: ADD Vx, Vy - Set Vx = Vx + Vy, set VF = carry
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        ushort add = (ushort)(V[x] + V[y]);
                        if(add > 255) V[0xF] = 1;
                        V[x] = (byte)(add);

                        PC += 2;

                        break;
                
                    case 0x0005:    // 8xy5: SUB Vx, Vy - Set Vx = Vx - Vy, set VF = NOT borrow
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        if(V[x] > V[y]) V[0xF] = 1; else V[0xF] = 0;
                        V[x] = (byte)(V[x] - V[y]);

                        PC += 2;

                        break;

                    case 0x0006:    // 8xy6: SHR Vx {, Vy} - Set Vx = Vx SHR 1
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        // SPECIAL!

                        PC += 2;
                        break;

                    case 0x0007:    // 8xy7: SUBN Vx, Vy - Set Vx = Vy - Vx, Set VF = NOT borrow
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        // SPECIAL!

                        PC += 2;
                        break;

                    case 0x000E:    // 8xyE: SHL Vx {, Vy} - Set Vx = Vx SHL 1
                        x = (byte)(opcode >> 8 & 0x000F);
                        y = (byte)(opcode >> 4 & 0x000F);

                        // SPECIAL!

                        PC += 2;
                        break;
                }
                break;

            case 0x9000:    // 9xy0: SNE Vx, Vy - Skip next instruction if Vx != Vy
                x = (byte)(opcode >> 8 & 0x000F);
                y = (byte)(opcode >> 4 & 0x000F);
                
                if(V[x] != V[y])
                {
                    PC += 4;
                } else {
                    PC += 2;
                }

                break;

            case 0xA000:    // Annn: LD I, addr - Set I = nnn
                I = (ushort)(opcode &0x0FFF);
                PC += 2;

                break;

            case 0xB000:    // Bnnn: JP V0, add - Jump to location nnn + V0
                PC = (ushort)((opcode & 0x0FFF) + V[0]);
                
                break;

            case 0xC000:    // Cxkk: RND Vx, byte - Set Vx = random byte AND kk
                x = (byte)(opcode >> 8 & 0x000F);
                k = (byte)(opcode & 0x00FF);

                Random rand = new Random();
                byte randomByte = (byte)(rand.Next(0,256));

                V[x] = (byte)(randomByte & k);
                
                PC += 2;
                
                break;

            case 0xD000:    // Dxyn: DRW Vx, Vy, nibble - Display n-byte sprite starting at mem location I at (Vx, Vy). set VF = collision

                // SPECIAL! DRAW CODE
                x = (byte)(opcode >> 8 & 0x000F);
                y = (byte)(opcode >> 4 & 0x000F);

                byte xPos = V[x];
                byte yPos = V[y];
                byte spriteHeight = (byte)(opcode & 0x000F);

                for(int n = 0; n < spriteHeight; n++)
                {
                    byte currentPixel = memory[ I + n ];
                    
                    for(int i = 0; i < 8; i++)
                    {
                        if((currentPixel & (0x80 >> i)) != 0 )
                        {
                            display[V[x]+i,V[y]+n] = 1; 
                            //Console.WriteLine("1");
                        }
                    }
                }

                drawFlag = true;
                PC += 2;

                break;

            case 0xE000:
                switch(opcode & 0x00FF)
                {
                    case 0x009E:    // Ex9E: SKP Vx - Skip next instruction if key with the value of Vx is pressed
                        x = (byte)(opcode >> 8 & 0x000F);
                        
                        if(keys[V[x]] == 1)
                        {
                            PC += 4;
                        } else {
                            PC += 2;
                        }

                        break;

                    case 0x00A1:    // ExA1: SKNP Vx - Skip next instruction if key with the value of Vx is not pressed
                        x = (byte)(opcode >> 8 & 0x000F);
                        
                        if(keys[V[x]] != 1)
                        {
                            PC += 4;
                        } else {
                            PC += 2;
                        }
                        break;
                }
                break;

            case 0xF000:
                switch(opcode & 0x00FF)  
                {
                    case 0x0007:    // Fx07: LD Vx, DT - Set Vx = delay timer value;
                        x = (byte)(opcode >> 8 & 0x000F);
                        V[x] = delayTimer;

                        PC += 2;
                        break;
                    case 0x000A:    // Fx0A: LD Vx, K - Wait for key press, store the value of the key in Vx

                        // SPECIAL! Execution stops until key pressed
                        x = (byte)(opcode >> 8 & 0x000F);
                        
                        for(int i = 0; i < 16; i++)
                        {
                            if(keys[i] == 1)
                            {
                                V[x] = keys[i];
                                PC += 2;
                            }
                        }
                        
                        break;

                    case 0x0015:    // Fx15: LD DT, Vx - Set delay timer = Vx
                        x = (byte)(opcode >> 8 & 0x000F);
                        delayTimer = V[x];
                        
                        PC += 2;
                        break;

                    case 0x0018:    // Fx18: LD ST, Vx - Set sound timer = Vx
                        x = (byte)(opcode >> 8 & 0x000F);
                        soundTimer = V[x];
                        
                        PC += 2;
                        break;

                    case 0x001E:    // Fx1E: ADD I, Vx - Set I = I + Vx
                        x = (byte)(opcode >> 8 & 0x000F);
                        I = (ushort)(I + V[x]);
                        
                        PC += 2;
                        break;
                    
                    case 0x0029:    // Fx29: LD F, Vx - Set I = location of sprite for digit Vx
                        x = (byte)(opcode >> 8 & 0x000F);
                        I = V[x];

                        // SPECIAL!

                        PC += 2;
                        break;
                    
                    case 0x0033:    // Fx33: LD B, Vx - Store BCD representation of Vx in memory locations I, I+1, I+2
                        x = (byte)(opcode >> 8 & 0x000F);
                        
                        // SPECIAL!

                        PC += 2;
                        break;

                    case 0x0055:    // Fx55: LD [I], Vx - Store registers V0 through Vx in memory starting at location I
                        x = (byte)(opcode >> 8 & 0x000F);
                        
                        // SPECIAL1

                        PC += 2;
                        break;

                    case 0x0065:    // Fx65: LD Vx, [I] - Read registers V0 through Vx from memory starting at location I
                        x = (byte)(opcode >> 8 & 0x000F);
                        I = (ushort)(I + V[x]);
                        
                        PC += 2;
                        break;
                } 
                break;
        }
        //Console.Write("\n");

        // update timers
    }

}