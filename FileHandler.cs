using System;
using System.IO;
using System.Text;

class FileHandler
{
    public string _fileName = "";
    public byte[] romData;

    public FileHandler(string filename)
    {
        _fileName = filename;

        if(File.Exists(_fileName))
        {
            using (var stream = File.Open(_fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.ASCII, false))
                {
                    romData = new byte[reader.BaseStream.Length];
                    int byteCount = 0;

                    while(reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byte currentByte = reader.ReadByte();
                        romData[byteCount] = currentByte;
                        byteCount++;
                    }
                }
            }

            Console.WriteLine("Finished reading file.");
        }else{
            Console.WriteLine("ERROR! File not found.");
        }
    }
}