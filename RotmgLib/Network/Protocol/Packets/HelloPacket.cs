using System;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.HELLO)]
    class HelloPacket : OutPacket
    {
        public string BuildVersion
        { get; set; }
        public int GameID
        { get; set; }
        public string GUID
        { get; set; }
        public string Password
        { get; set; }
        public string Secret
        { get; set; }

        public HelloPacket()
            : this("", 0, "", "", "")
        { }

        public HelloPacket(string build_version, int game_id, string guid, string password, string secret)
        {
            this.BuildVersion = build_version;
            this.GameID = game_id;
            this.GUID = guid;
            this.Password = password;
            this.Secret = secret;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.BuildVersion);
            writer.Write(this.GameID);
            writer.Write(this.GUID);
            writer.Write(this.Password);
            writer.Write(this.Secret);

            return writer.GetBuffer();
        }
    }
}
