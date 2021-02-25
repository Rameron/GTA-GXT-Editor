using System;
using System.IO;
using System.Text;

namespace GTA_GXT_Editor.Utils
{
    public static class FileStreamExtensions
    {
        public static byte[] ReadBytes(this Stream fsStream, int bytesCount)
        {
            byte[] readBytes = new byte[bytesCount];
            fsStream.Read(readBytes, 0, bytesCount);
            return readBytes;
        }
        public static int ReadInt(this Stream fsStream)
        {
            return BitConverter.ToInt32(ReadBytes(fsStream, sizeof(int)), 0);
        }
        public static string ReadString(this Stream fsStream, int stringLength)
        {
            return Encoding.ASCII.GetString(ReadBytes(fsStream, stringLength));
        }

        public static void WriteBytes(this Stream fsStream, byte[] inputBytes)
        {
            fsStream.Write(inputBytes, 0, inputBytes.Length);
        }
        public static void WriteInt(this Stream fsStream, int inputInt)
        {
            WriteBytes(fsStream, BitConverter.GetBytes(inputInt));
        }
        public static void WriteString(this Stream fsStream, string inputString)
        {
            WriteBytes(fsStream, Encoding.ASCII.GetBytes(inputString));
        }

    }
}
