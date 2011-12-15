using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RotmgLib.Network.Protocol.Packets;
using System.Reflection;
using RotmgLib.Network.Protocol;
using RotmgLib.Crypto;

namespace RotmgLib.Network
{
    /* the great debate:
     * manually add a switch statement for every packet so each one gets an event
     * or use a dictionary
     * HMMMMMMMMMMMM
     */

    public delegate void FailureHandler(int error_id, string error_description);
    public delegate void CreateSuccessHandler(int object_id, int character_id);
    public delegate void TextHandler(string name, int object_id, short object_type, int texture_1_id, int texture_2_id, int num_stars, string recipient, string text, string clean_text);
    public delegate void ShootHandler(byte bullet_id, int owner_id, short container_type, TilePoint starting_position, float angle, short damage);
    public delegate void DamageHandler(int target_id, byte condition_effect, ushort damage_amount, byte bullet_id, int object_id);
    public delegate void UpdateHandler(TilePoint[] tiles, GameObject[] new_objects, int[] drops);
    public delegate void NotificationHandler(int object_id, string text);
    public delegate void NewTickHandler(int tick_id, int tick_time, ObjectStatusData[] statuses);
    public delegate void MapInfoHandler(int width, int height, string name, uint fp, int background, string[] extra_xml);

    public class RotmgClient : ServerConnection
    {
        public event FailureHandler OnFailure;
        public event CreateSuccessHandler OnCreateSuccess;
        public event TextHandler OnText;
        public event ShootHandler OnShoot;
        public event DamageHandler OnDamage;
        public event UpdateHandler OnUpdate;
        public event NotificationHandler OnNotification;
        public event NewTickHandler OnNewTick;
        public event MapInfoHandler OnMapInfo;

        public RotmgClient(string host)
            : base(host)
        { }

        private Opcode GetOpcode(MemberInfo member)
        {
            foreach (object attribute in member.GetCustomAttributes(true))
                if (attribute is PacketOpcodeAttribute)
                    return ((PacketOpcodeAttribute)attribute).Opcode;

            throw new KeyNotFoundException("Packet opcode was not specified.");
        }

        private void SendPacket(OutPacket packet)
        {
            Opcode packet_opcode = this.GetOpcode(packet.GetType());

            base.Send((byte)packet_opcode, packet.Write());
        }

        public void SendHello(string build, int gameid, string username, string password, string secret)
        {
            this.SendPacket(new HelloPacket(build, gameid, RSACryptString.Crypt(username), RSACryptString.Crypt(password), RSACryptString.Crypt(secret)));
        }

        public void SendCreate(int object_type)
        {
            this.SendPacket(new CreatePacket(object_type));
        }

        public void SendPlayerShoot(int time, byte bullet_id, short container_type, TilePoint starting_position, float angle)
        {
            this.SendPacket(new PlayerShootPacket(time, bullet_id, container_type, starting_position, angle));
        }

        public void SendMove(int tick_id, int time, TilePoint new_position)
        {
            this.SendPacket(new MovePacket(tick_id, time, new_position));
        }

        public void SendPlayerText(string text)
        {
            this.SendPacket(new PlayerTextPacket(text));
        }

        public void SendMessage()
        {
            this.SendPacket(new MessagePacket());
        }

        protected override void OnReceive(int size, byte opcode, byte[] packet)
        {
            InPacket in_packet = InPacket.Parse((Opcode)opcode, packet);

            switch ((Opcode)opcode)
            {
                case Opcode.FAILURE:
                    if (this.OnFailure != null)
                    {
                        FailurePacket failure = (FailurePacket)in_packet;
                        this.OnFailure(failure.ErrorId, failure.ErrorDescription);
                    }
                    break;
                case Opcode.CREATE_SUCCESS:
                    if (this.OnCreateSuccess != null)
                    {
                        CreateSuccessPacket create_success = (CreateSuccessPacket)in_packet;
                        this.OnCreateSuccess(create_success.ObjectId, create_success.CharId);
                    }
                    break;
                case Opcode.TEXT:
                    if (this.OnText != null)
                    {
                        TextPacket text = (TextPacket)in_packet;
                        this.OnText(text.Name, text.ObjectId, text.ObjectType, text.Texture1Id, text.Texture2Id, text.NumStars, text.Recipient, text.Text, text.CleanText);
                    }
                    break;
                case Opcode.SHOOT:
                    if (this.OnShoot != null)
                    {
                        ShootPacket shoot = (ShootPacket)in_packet;
                        this.OnShoot(shoot.BulletId, shoot.OwnerId, shoot.ContainerType, shoot.StartingPosition, shoot.Angle, shoot.Damage);
                    }
                    break;
                case Opcode.DAMAGE:
                    if (this.OnDamage != null)
                    {
                        DamagePacket damage = (DamagePacket)in_packet;
                        this.OnDamage(damage.TargetId, damage.ConditionEffect, damage.DamageAmount, damage.BulletId, damage.ObjectId);
                    }
                    break;
                case Opcode.UPDATE:
                    if (this.OnUpdate != null)
                    {
                        UpdatePacket update = (UpdatePacket)in_packet;
                        this.OnUpdate(update.Tiles, update.NewObjects, update.Drops);
                    }
                    break;
                case Opcode.NOTIFICATION:
                    if (this.OnNotification != null)
                    {
                        NotificationPacket notification = (NotificationPacket)in_packet;
                        this.OnNotification(notification.ObjectId, notification.Text);
                    }
                    break;
                case Opcode.NEW_TICK:
                    if (this.OnNewTick != null)
                    {
                        NewTickPacket new_tick = (NewTickPacket)in_packet;
                        this.OnNewTick(new_tick.TickId, new_tick.TickTime, new_tick.Statuses);
                    }
                    break;
                case Opcode.MAPINFO:
                    if (this.OnMapInfo != null)
                    {
                        MapInfoPacket map_info = (MapInfoPacket)in_packet;
                        this.OnMapInfo(map_info.Width, map_info.Height, map_info.Name, map_info.FP, map_info.Background, map_info.ExtraXml);
                    }
                    break;
            }
        }
    }
}
