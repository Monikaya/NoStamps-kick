using Cove.Server.Plugins;
using Cove.Server;
using Cove.Server.Actor;
using Cove.GodotFormat;

// Change the namespace and class name!
namespace NoStamps
{

    public class NoStamps : CovePlugin
    {
        public NoStamps(CoveServer server) : base(server) { }

        public override void onInit()
        {
            base.onInit();

            Log("NoStamps | developed by yeeter");
        }

        //public override void onPlayerJoin(WFPlayer player)
        //{
            //base.onPlayerJoin(player);

            //SendPlayerChatMessage(player, "This server is protected by NoStamps. Any attempts to use the Stamps mod will get you instantly banned. Do not attempt trying it at all.");
            //Log("Sent warning message to player");
        //}

        public Dictionary<Vector2, int> chalkImage = new Dictionary<Vector2, int>();
        public override void onNetworkPacket(WFPlayer sender, Dictionary<string, object> packet)
        {
            base.onNetworkPacket(sender, packet);

            object value;
            if (packet.TryGetValue("type", out value))
            {
                if (typeof(string) != value.GetType()) return;

                string type = value as string;
                if (type == "instance_actor")
                {
                    string actor_type = (string)((Dictionary<string, object>)packet["params"])["actor_type"];
                    if (actor_type == "player")
                    {
                        SendPlayerChatMessage(sender, "This server is protected by NoStamps-kick. Any attempts to use the Stamps mod will get you kicked.");
                        Log("Sent warning message to player");
                    }

                    if (actor_type == "canvas")
                    {
                       
                        Log(sender.Username + " has been kicked from this server for using a stamp");
                        kickPlayer(sender);
                        SendGlobalChatMessage(sender.Username + " has been kicked for using the Stamp mod!");
                    }
                } 
            }
        }

    }
}
