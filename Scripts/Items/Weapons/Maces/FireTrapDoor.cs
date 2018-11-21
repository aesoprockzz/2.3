using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class FireTrapDoor : MagicWand
	{
		public override int LabelNumber{ get{ return 1041424; } } // a fireworks wand

		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public FireTrapDoor() : this( 100 )
		{
		}

		[Constructable]
		public FireTrapDoor( int charges )
		{
			m_Charges = charges;
			LootType = LootType.Blessed;
		}

		public FireTrapDoor( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( 1060741, m_Charges.ToString() ); // charges: ~1_val~
		}

		public override void OnDoubleClick( Mobile from )
		{
			BeginLaunch( from, true );
		}

		public void BeginLaunch( Mobile from, bool useCharges )
		{
			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			if ( useCharges )
			{
				if ( Charges > 0 )
				{
					--Charges;
				}
				else
				{
					from.SendLocalizedMessage( 502412 ); // There are no charges left on that item.
					return;
				}
			}

			from.SendAsciiMessage( "Wait. Something is happening. Do you hear that?" );

			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc1 = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc1 = new Point3D( startLoc1.X + Utility.RandomMinMax( -8, 8 ), startLoc1.Y + Utility.RandomMinMax( -8, 8 ), startLoc1.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc1, map ), new Entity( Serial.Zero, endLoc1, map ),0x36E4, 5, 0, false, false );
				
			Point3D startLoc2 = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc2 = new Point3D( startLoc2.X + Utility.RandomMinMax( 8, -8 ), startLoc2.Y + Utility.RandomMinMax( 8, -8 ), startLoc2.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc2, map ), new Entity( Serial.Zero, endLoc2, map ),0x36E4, 5, 0, false, false );
			
			Point3D startLoc3 = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc3 = new Point3D( startLoc3.X + Utility.RandomMinMax( -8, 8 ), startLoc3.Y + Utility.RandomMinMax( -8, 8 ), startLoc3.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc3, map ), new Entity( Serial.Zero, endLoc3, map ),0x36E4, 5, 0, false, false );
				
			Point3D startLoc4 = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc4 = new Point3D( startLoc4.X + Utility.RandomMinMax( 8, -8 ), startLoc4.Y + Utility.RandomMinMax( 8, -8 ), startLoc4.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc4, map ), new Entity( Serial.Zero, endLoc4, map ),0x36E4, 5, 0, false, false );
			
			Point3D startLoc5 = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc5 = new Point3D( startLoc5.X + Utility.RandomMinMax( -8, 8 ), startLoc5.Y + Utility.RandomMinMax( -8, 8 ), startLoc5.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc5, map ), new Entity( Serial.Zero, endLoc5, map ),0x36E4, 5, 0, false, false );
				
			Point3D startLoc6 = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc6 = new Point3D( startLoc6.X + Utility.RandomMinMax( 8, -8 ), startLoc6.Y + Utility.RandomMinMax( 8, -8 ), startLoc6.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc6, map ), new Entity( Serial.Zero, endLoc6, map ),0x36E4, 5, 0, false, false );
				
			from.PlaySound( Core.AOS ? 0x15E : 0x44B );

			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc1, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc2, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc3, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc4, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc5, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc6, map } );
		}

		private void FinishLaunch( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			int hue = Utility.Random( 40 );

			if ( hue < 8 )
				hue = 0x66D;
			else if ( hue < 10 )
				hue = 0x482;
			else if ( hue < 12 )
				hue = 0x47E;
			else if ( hue < 16 )
				hue = 0x480;
			else if ( hue < 20 )
				hue = 0x47F;
			else
				hue = 0;

			if ( Utility.RandomBool() )
				hue = Utility.RandomList( 0x47E, 0x47F, 0x480, 0x482, 0x66D );

			//int renderMode = Utility.RandomList( 0, 2, 3, 4, 5, 7 );

			Effects.PlaySound( endLoc, map, Utility.Random( 0x11B, 4 ) );
			Effects.SendLocationEffect( endLoc, map, 0x160, 16, 10 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Charges = reader.ReadInt();
					break;
				}
			}
		}
	}
}