using Sandbox;

namespace SandOS
{
	partial class User : Player
	{
		public override void Respawn()
		{
			Camera = new FixedCamera();

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
		}

		public override void OnKilled()
		{
			return; // NOOP
		}
	}
}
