using MP3Sharp;
using Sandbox;
using Sandbox.Internal;
using System;

namespace SandOS
{
	partial class User : Player
	{
		protected Sound music;
		protected SoundStream musicSS;

		public override void Respawn()
		{
			Camera = new FixedCamera();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsClient )
				DebugOverlay.ScreenText( $"{musicSS.QueuedSampleCount}" );
		}

		public override void OnKilled()
		{
			return; // NOOP
		}

		[ClientCmd( "sandos_loadurl" )]
		public static void CmdPlayURL( string url )
		{
			(Local.Pawn as User)?.LoadMusicFromWeb( url );
		}

		public async void LoadMusicFromWeb( string url )
		{
			music.Stop();

			var result = await new Http( new Uri( url ) ).GetStreamAsync();
			var mp3Stream = new MP3Stream( result );

			music = Sound.FromScreen( "audiostream.default" );
			musicSS = music.CreateStream( sampleRate: mp3Stream.Frequency, channels: mp3Stream.ChannelCount );

			var buffer = new byte[8192];
			var delay = 1000 * buffer.Length / sizeof( short ) / mp3Stream.ChannelCount / mp3Stream.Frequency;
			Log.Info( $"{delay}" );

			int bytesRead = -1, bytesTotal = 0;
			while ( !music.Finished && bytesRead != 0 )
			{
				if ( musicSS.QueuedSampleCount < buffer.Length * 2 )
				{
					bytesRead = mp3Stream.Read( buffer, 0, buffer.Length );
					var data = new short[bytesRead / 2];
					for ( int i = 0; i < data.Length; i++ )
					{
						data[i] = (short)(buffer[i * 2] | (buffer[i * 2 + 1] << 8));
					}

					musicSS.WriteData( new Span<short>( data ) );
					bytesTotal += bytesRead;
				} else
					await GameTask.DelayRealtime( Math.Max( delay, 1 ) );
			}

			Log.Info( $"read {bytesTotal} bytes" );

			mp3Stream.Close();
		}
	}
}
