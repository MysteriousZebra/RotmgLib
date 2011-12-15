using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using RotmgLib.Crypto;

namespace RotmgLib.Network
{
    public delegate void ReceiveEventHandler(int size, byte opcode, byte[] packet);

    public class ServerConnection
    {
        private TcpClient m_Connection;
        private string    m_Host;
        private Thread    m_ReceiveThread;
        private RC4       m_SendCrypto;
        private RC4       m_RecvCrypto;

        public event ReceiveEventHandler PacketReceived;

        public ServerConnection(string host)
        {
            this.m_Connection = new TcpClient();
            this.m_Host = host;
            this.m_SendCrypto = new RC4(new byte[] { 0x7a, 0x43, 0x56, 0x32, 0x74, 0x73, 0x59, 0x30, 0x5d, 0x73, 0x3b, 0x7c, 0x5d });
            this.m_RecvCrypto = new RC4(new byte[] { 0x68, 0x50, 0x76, 0x4a, 0x28, 0x52, 0x7d, 0x4d, 0x70, 0x24, 0x2d, 0x63, 0x67 });
        }

        public void Connect()
        {
            this.m_Connection.Connect(this.m_Host, 2050);

            this.m_ReceiveThread = new Thread(new ThreadStart(ReceiveLoop));
            this.m_ReceiveThread.Start();
        }

        protected virtual void OnReceive(int size, byte opcode, byte[] packet) // overridable
        {
            if (this.PacketReceived != null)
                this.PacketReceived(size, opcode, packet);
        }

        public void Send(byte opcode, byte[] packet)
        {
            byte[] buffer = new byte[packet.Length + 5];

            byte[] length = BitConverter.GetBytes(packet.Length);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(length);
            
            Array.Copy(length, buffer, 4);
            buffer[4] = opcode;

            Array.Copy(this.m_SendCrypto.Crypt(packet), 0, buffer, 5, packet.Length);

            this.m_Connection.Client.Send(buffer);
        }

        private void ReceiveLoop()
        {
            byte[] buffer = new byte[2500000]; // 2MB should be more than sufficient
            int    length = 5;
            int    offset = 0;
            byte    opcode = 0xFF; // just to avoid having another bool, add new bool when 0xFF is used

            while (true)
            {
                int bytes = this.m_Connection.Client.Receive(buffer, offset, length - offset, SocketFlags.None);

                if (bytes == 0 && (length - offset) != 0) // second clause should really be there BUT given there's 
                {                                         // 0 size packets and I don't care enough to check for them so it happens
                    this.m_Connection.Close();
                    return;
                }

                offset += bytes;

                if (offset == length) // continue receiving
                {
                    if (opcode == 0xFF) // header
                    {
                        if (BitConverter.IsLittleEndian)
                            length = BitConverter.ToInt32(new byte[] { buffer[3], buffer[2], buffer[1], buffer[0] }, 0);
                        else
                            length = BitConverter.ToInt32(new byte[] { buffer[0], buffer[1], buffer[2], buffer[3] }, 0);

                        length -= 5;
                        opcode = buffer[4];
                        offset = 0;
                    }
                    else
                    {
                        byte[] crypt_buffer = new byte[length];
                        Array.Copy(buffer, crypt_buffer, length);

                        crypt_buffer = this.m_RecvCrypto.Crypt(crypt_buffer); // GC should handle the old buffer

                        this.OnReceive(length + 5, opcode, crypt_buffer);

                        length = 5;
                        opcode = 0xFF;
                        offset = 0;
                    }
                }

                Thread.Sleep(10);
            }
        }
    }
}