using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class PacketHandler
    {
        public static void PlayerInfoReqHandler(PacketSession session, IPacket packet)
        {
            PlayerInfoReq p = packet as PlayerInfoReq;

            Console.WriteLine($"PlayerInfoReq : {p.playerId}, PlayerName : {p.name}");

            foreach (PlayerInfoReq.Skill skill in p.skills)
            {
                Console.WriteLine($"Skill (Id : {skill.id}) (Lvl : {skill.level}) (Dur : {skill.duration})");
            }
        }
    }
}
