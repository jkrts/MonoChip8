using System;

class Disassembler
{
    public byte[] romData;
    public byte[] memory;
    int maxMem = 0x290;


    //ushort pc = 0x200;

    public Disassembler()
    {
        memory = new byte[maxMem];

        for(int i = 0; i < memory.Length; ++i)
        {
            memory[i] = 0x00;
        }
    } 

    public void LoadRom(string fileName)
    {
        FileHandler fh = new FileHandler(fileName);
        romData = fh.romData;

        ushort pc = 0x200;

        for(int i = 0; i < romData.Length; ++i)
        {
            memory[pc] = romData[i];
            pc++;
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

    public void Disassemble()
    {
        ushort pc = 0x200;
        ushort opcode = 0;
        
        while(pc < memory.Length)
        {
            opcode = (ushort)(memory[pc] << 8 | memory[pc+1]);
            Decode(opcode, pc);
            pc += 2;
        }
    }

    public void Decode(ushort opcode, ushort pc)
    {
        Console.Write("{0:X4}: ", pc);
        Console.Write("{0:X4}",opcode);

        switch(opcode & 0xF000)
        {
            case 0x0000:
                switch(opcode & 0x00FF)
                {
                    case 0x00E0:    // 00E0: CLS - Clears the display
                        Console.Write(" \tCLS\n");
                        break;
                    case 0x00EE:    // 00EE: RET - Return from subroutine 
                        Console.Write("\n");
                        break;
                    default: Console.Write("\n");
                        break;
                }
                break;
            case 0x1000:    // 1nnn: JP addr - Jump to location nnn
                Console.Write(" \tJP addr\t\t1nnn: jump to location nnn\n");
                break;
                
            case 0x2000:    // 2nnn: CALL addr - Call subroutine at nnn
                Console.Write("\n");
                break;

            case 0x3000:    // 3xkk: SE Vx, byte - Skip next instruction if Vx = kk
                Console.Write("\n");
                break;

            case 0x4000:    // 4xkk: SNE Vx, byte - Skip next instruction if Vx != kk
                Console.Write("\n");
                break;

            case 0x5000:    // 5xy0: SE Vx, Vy - Skip next instruction if Vx = Vy
                Console.Write("\n");
                break;

            case 0x6000:    // 6xkk: LD Vx, byte - Set Vx = kk
                Console.Write(" \tLD Vx, byte\t6xkk: Set Vx = kk\n");
                break;

            case 0x7000:    // 7xkk: ADD Vx, byte - Set Vx = Vx + kk
                Console.Write(" \tADD Vx, byte\t7xkk: Set Vx = Vx + kk\n");
                break;

            case 0x8000:
                switch(opcode & 0x000F)
                {
                    case 0x0000:    // 8xy0: LD Vx, Vy - Set Vx = Vy
                        Console.Write("\n");
                        break;

                    case 0x0001:    // 8xy1: OR Vx, Vy - Set Vx = Vx OR Vy
                        Console.Write("\n");
                        break;

                    case 0x0002:    // 8xy2: AND Vx, Vy - Set Vx = Vx AND Vy
                        Console.Write("\n");
                        break;

                    case 0x0003:    // 8xy3: XOR Vx, Vy - Set Vx = Vx XOR Vy
                        Console.Write("\n");
                        break;

                    case 0x0004:    // 8xy4: ADD Vx, Vy - Set Vx = Vx + Vy, set VF = carry
                        Console.Write("\n");
                        break;
                
                    case 0x0005:    // 8xy5: SUB Vx, Vy - Set Vx = Vx - Vy, set VF = NOT borrow
                        Console.Write("\n");
                        break;

                    case 0x0006:    // 8xy6: SHR Vx {, Vy} - Set Vx = Vx SHR 1
                        Console.Write("\n");
                        break;

                    case 0x0007:    // 8xy7: SUBN Vx, Vy - Set Vx = Vy - Vx, Set VF = NOT borrow
                        Console.Write("\n");
                        break;

                    case 0x000E:    // 8xyE: SHL Vx {, Vy} - Set Vx = Vx SHL 1
                        Console.Write("\n");
                        break;
                    default: Console.Write("\n");
                        break;
                }
                break;

            case 0x9000:    // 9xy0: SNE Vx, Vy - Skip next instruction if Vx != Vy
                Console.Write("\n");
                break;

            case 0xA000:    // Annn: LD I, addr - Set I = nnn
                Console.Write(" \tLD I, addr\tAnnn: Set I = nnn\n");
                break;

            case 0xB000:    // Bnnn: JP V0, add - Jump to location nnn + V0
                Console.Write("\n");
                break;

            case 0xC000:    // Cxkk: RND Vx, byte - Set Vx = random byte AND kk
                Console.Write("\n");
                break;

            case 0xD000:    // Dxyn: DRW Vx, Vy, nibble - Display n-byte sprite starting at mem location I at (Vx, Vy). set VF = collision
                Console.Write(" \tDRW Vx, Vy, n\tDxyn: Display n-byte sprite starting at I at (Vx, Vy)\n");
                break;

            case 0xE000:
                switch(opcode & 0x00FF)
                {
                    case 0x009E:    // Ex9E: SKP Vx - Skip next instruction if key with the value of Vx is pressed
                        Console.Write("\n");
                        break;

                    case 0x00A1:    // ExA1: SKNP Vx - Skip next instruction if key with the value of Vx is not pressed
                        Console.Write("\n");
                        break;
                    default: Console.Write("\n");
                        break;
                }
                break;

            case 0xF000:
                switch(opcode & 0x00FF)  
                {
                    case 0x0007:    // Fx07: LD Vx, DT - Set Vx = delay timer value;
                        Console.Write("\n");
                        break;

                    case 0x000A:    // Fx0A: LD Vx, K - Wait for key press, store the value of the key in Vx
                        Console.Write("\n");
                        break;

                    case 0x0015:    // Fx15: LD DT, Vx - Set delay timer = Vx
                        Console.Write("\n");
                        break;

                    case 0x0018:    // Fx18: LD ST, Vx - Set sound timer = Vx
                        Console.Write("\n");
                        break;

                    case 0x001E:    // Fx1E: ADD I, Vx - Set I = I + Vx
                        Console.Write("\n");
                        break;
                    
                    case 0x0029:    // Fx29: LD F, Vx - Set I = location of sprite for digit Vx
                        Console.Write("\n");
                        break;
                    
                    case 0x0033:    // Fx33: LD B, Vx - Store BCD representation of Vx in memory locations I, I+1, I+2
                        Console.Write("\n");
                        break;

                    case 0x0055:    // Fx55: LD [I], Vx - Store registers V0 through Vx in memory starting at location I
                        Console.Write("\n");
                        break;

                    case 0x0065:    // Fx65: LD Vx, [I] - Read registers V0 through Vx from memory starting at location I
                        Console.Write("\n");
                        break;
                    default: Console.Write("\n");
                        break;
                }
                break;
        }
    }

}