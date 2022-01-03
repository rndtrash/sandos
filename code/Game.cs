using Sandbox;

namespace SandOS
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class Game : Sandbox.Game
	{
		public Game()
		{
			if ( IsServer )
			{
				new HudEntity();
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new User();
			client.Pawn = player;

			player.Respawn();
		}

		public override bool CanHearPlayerVoice( Client source, Client dest )
		{
			return false; // NOOP
		}

		public override void DoPlayerSuicide( Client cl )
		{
			cl.Kick();
		}

		public override void DoPlayerNoclip( Client player )
		{
			return; // NOOP
		}

		public override void OnVoicePlayed( long playerId, float level )
		{
			return; // NOOP
		}
	}

}
