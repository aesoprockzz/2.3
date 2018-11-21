using System;
using Server.Spells;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public enum DoorFacing
	{
		WestCW,
		EastCCW,
		WestCCW,
		EastCW,
		SouthCW,
		NorthCCW,
		SouthCCW,
		NorthCW,
		//Sliding Doors
		SouthSW,
		SouthSE,
		WestSS,
		WestSN
	}

	public class IronGateShort : BaseDoor
	{
		[Constructable]
		public IronGateShort( DoorFacing facing ) : base( 0x84c + (2 * (int)facing), 0x84d + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public IronGateShort( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class IronGate : BaseDoor
	{
		[Constructable]
		public IronGate( DoorFacing facing ) : base( 0x824 + (2 * (int)facing), 0x825 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public IronGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LightWoodGate : BaseDoor
	{
		[Constructable]
		public LightWoodGate( DoorFacing facing ) : base( 0x839 + (2 * (int)facing), 0x83A + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset( facing ) )
		{
		}

		public LightWoodGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DarkWoodGate : BaseDoor
	{
		[Constructable]
		public DarkWoodGate( DoorFacing facing ) : base( 0x866 + (2 * (int)facing), 0x867 + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset( facing ) )
		{
		}

		public DarkWoodGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MetalDoor : BaseDoor
	{
		[Constructable]
		public MetalDoor( DoorFacing facing ) : base( 0x675 + (2 * (int)facing), 0x676 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public MetalDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class MetalDoorTrap : BaseDoor
	{
		[Constructable]
		public MetalDoorTrap( DoorFacing facing ) : base( 0x675 + (2 * (int)facing), 0x676 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public MetalDoorTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			BeginLaunch( from, true );
			base.OnDoubleClick(from);
		}

		public void BeginLaunch( Mobile from, bool useCharges )
		{
			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;
			from.Paralyze( TimeSpan.FromSeconds( 12 ) );
			from.SendAsciiMessage(33, "Wait. Something is happening. Do you hear that?" );

			
			
			Timer.DelayCall( TimeSpan.FromSeconds( 1.00 ), new TimerStateCallback( LaunchEffectGo ), new object[]{ from, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 2.00 ), new TimerStateCallback( LaunchEffectGo ), new object[]{ from, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 3.00 ), new TimerStateCallback( LaunchEffectGo ), new object[]{ from, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 4.00 ), new TimerStateCallback( LaunchEffectGo ), new object[]{ from, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 5.00 ), new TimerStateCallback( LaunchEffectGo ), new object[]{ from, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 6.00 ), new TimerStateCallback( LaunchEffectGo ), new object[]{ from, map } );
			
			Timer.DelayCall( TimeSpan.FromSeconds( 6.50 ), new TimerStateCallback( DoFallback ), new object[]{ from, map } );
		}
		
		private void LaunchEffectGo( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Map map = (Map)states[1];
			
			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc1 = new Point3D( ourLoc.X+1, ourLoc.Y+13, ourLoc.Z + 32);
			Point3D endLoc1 = new Point3D( ourLoc.X+1, ourLoc.Y + Utility.RandomMinMax( -12, 12 ), startLoc1.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc1, map ), new Entity( Serial.Zero, endLoc1, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc2 = new Point3D( ourLoc.X+3, ourLoc.Y+13, ourLoc.Z + 32 );
			Point3D endLoc2 = new Point3D( ourLoc.X+3, ourLoc.Y + Utility.RandomMinMax( -12, 12 ), startLoc2.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc2, map ), new Entity( Serial.Zero, endLoc2, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc3 = new Point3D( ourLoc.X+5, ourLoc.Y+13, ourLoc.Z + 32 );
			Point3D endLoc3 = new Point3D( ourLoc.X+5, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc3.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc3, map ), new Entity( Serial.Zero, endLoc3, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc4 = new Point3D( ourLoc.X+7, ourLoc.Y+16, ourLoc.Z + 32 );
			Point3D endLoc4 = new Point3D( ourLoc.X+7, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc4.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc4, map ), new Entity( Serial.Zero, endLoc4, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc5 = new Point3D( ourLoc.X+9, ourLoc.Y+14, ourLoc.Z + 32 );
			Point3D endLoc5 = new Point3D( ourLoc.X+9, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc5.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc5, map ), new Entity( Serial.Zero, endLoc5, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc6 = new Point3D( ourLoc.X+ 11, ourLoc.Y+14, ourLoc.Z + 32 );
			Point3D endLoc6 = new Point3D( ourLoc.X+ 11, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc6.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc6, map ), new Entity( Serial.Zero, endLoc6, map ),0x36D4, 5, 0, false, false );
			
			Point3D startLoc7 = new Point3D( ourLoc.X-1, ourLoc.Y+13, ourLoc.Z + 32 );
			Point3D endLoc7 = new Point3D( ourLoc.X-1, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc7.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc7, map ), new Entity( Serial.Zero, endLoc7, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc8 = new Point3D( ourLoc.X-3, ourLoc.Y+13, ourLoc.Z + 32 );
			Point3D endLoc8 = new Point3D( ourLoc.X-3, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc8.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc8, map ), new Entity( Serial.Zero, endLoc8, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc9 = new Point3D( ourLoc.X-5, ourLoc.Y+13, ourLoc.Z + 32 );
			Point3D endLoc9 = new Point3D( ourLoc.X-5, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc9.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc9, map ), new Entity( Serial.Zero, endLoc9, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc10 = new Point3D( ourLoc.X-7, ourLoc.Y+16, ourLoc.Z + 32 );
			Point3D endLoc10 = new Point3D( ourLoc.X-7, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc10.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc10, map ), new Entity( Serial.Zero, endLoc10, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc11 = new Point3D( ourLoc.X-9, ourLoc.Y+15, ourLoc.Z + 32 );
			Point3D endLoc11 = new Point3D( ourLoc.X-9, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc11.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc11, map ), new Entity( Serial.Zero, endLoc11, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc12 = new Point3D( ourLoc.X-11, ourLoc.Y+14, ourLoc.Z + 32 );
			Point3D endLoc12 = new Point3D( ourLoc.X-11, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc12.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc12, map ), new Entity( Serial.Zero, endLoc12, map ),0x36D4, 5, 0, false, false );
			
			Point3D startLoc13 = new Point3D( ourLoc.X+13, ourLoc.Y+13, ourLoc.Z + 32 );
			Point3D endLoc13 = new Point3D( ourLoc.X+13, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc13.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc13, map ), new Entity( Serial.Zero, endLoc13, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc14 = new Point3D( ourLoc.X+15, ourLoc.Y+14, ourLoc.Z + 32 );
			Point3D endLoc14 = new Point3D( ourLoc.X+15, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc14.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc14, map ), new Entity( Serial.Zero, endLoc14, map ),0x36D4, 5, 0, false, false );

			Point3D startLoc15 = new Point3D( ourLoc.X+ 17, ourLoc.Y+15, ourLoc.Z + 32 );
			Point3D endLoc15 = new Point3D( ourLoc.X+ 17, ourLoc.Y + Utility.RandomMinMax( -14, 14 ), startLoc15.Z + 32 );
			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc15, map ), new Entity( Serial.Zero, endLoc15, map ),0x36D4, 5, 0, false, false );

			from.PlaySound( Core.AOS ? 0x15E : 0x44B );

			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc1, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc2, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc3, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc4, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc5, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc6, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc7, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.50 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc8, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc9, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.50 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc10, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc11, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.50 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc12, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc13, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc14, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc15, map } );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc15, map } );
			
			
			
		}
		
		private void DoFallback( object state )
		{
			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			Map map = (Map)states[1];
			
			from.Animate( 21, 5, 1, true, false, 0 );
			
			Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), new TimerStateCallback( TeleportToBlack ), new object[]{ from, map } );
			//21 6 1 0 0 1
		}
		
		private void TeleportToBlack( object state )
		{
			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			Map map = (Map)states[1];
			//from.Animate( 21, 5, 1, true, false, 0 );
			//5133,1148,0
			from.BodyValue = 0;
			from.Location = new Point3D( 5133, 1148, 0 );
			from.Map = Map.Felucca;
			Timer.DelayCall( TimeSpan.FromSeconds( 4 ), new TimerStateCallback( TeleportToStart ), new object[]{ from, map } );
			//21 6 1 0 0 1
		}
		
		private void TeleportToStart( object state )
		{
			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			Map map = (Map)states[1];
			//from.Animate( 21, 5, 1, true, false, 0 );
			//5133,1148,0
			from.Location = new Point3D( 1438, 1690, 0 );
			from.BodyValue = 400;
			from.Animate( 21, 5, 1, true, true, 0 );
			
			//Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), new TimerStateCallback( CharacterStandUp ), new object[]{ from, map } );
			//21 6 1 0 0 1
		}
		

		
		
		private void FinishLaunch( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			int hue = Utility.Random( 40 );
			hue = 0x47F;

			int renderMode = Utility.RandomList( 0, 2, 3, 4, 5, 7 );

			Effects.PlaySound( endLoc, map, Utility.Random( 0x11B, 4 ) );
			Effects.SendLocationEffect( endLoc, map, 0x36BD, 16, 10 );
			
			
			int etw = Utility.Random( 1,2 );
			
			bool eastToWest;
			if (etw == 1)
				eastToWest = true;
			else
				eastToWest = false;
			
			Effects.PlaySound( endLoc, from.Map, 0x20C );

			int itemID = eastToWest ? 0x398C : 0x3996;

			TimeSpan duration;


			duration = TimeSpan.FromSeconds( 3.0 );

			for ( int i = -2; i <= 2; ++i )
			{
				Point3D loc = new Point3D( eastToWest ? endLoc.X + i : endLoc.X, eastToWest ? endLoc.Y : endLoc.Y + i, 0 );

				new FireFieldItem( itemID, loc, from, from.Map, duration, i );
			}
			
		}
		
				[DispellableField]
		public class FireFieldItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			private Mobile m_Caster;
			private int m_Damage;

			public override bool BlocksFit{ get{ return true; } }

			public FireFieldItem( int itemID, Point3D loc, Mobile caster, Map map, TimeSpan duration, int val )
				: this( itemID, loc, caster, map, duration, val, 2 )
			{
			}

			public FireFieldItem( int itemID, Point3D loc, Mobile caster, Map map, TimeSpan duration, int val, int damage ) : base( itemID )
			{
				//bool canFit = SpellHelper.AdjustField( ref loc, map, 12, false );

				Visible = false;
				Movable = false;
				Light = LightType.Circle300;

				MoveToWorld( loc, map );

				m_Caster = caster;

				m_Damage = damage;

				m_End = DateTime.Now + duration;

				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( Math.Abs( val ) * 0.2 ), caster.InLOS( this ), true );
				m_Timer.Start();
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			public FireFieldItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 2 ); // version

				writer.Write( m_Damage );
				writer.Write( m_Caster );
				writer.WriteDeltaTime( m_End );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch ( version )
				{
					case 2:
					{
						m_Damage = reader.ReadInt();
						goto case 1;
					}
					case 1:
					{
						m_Caster = reader.ReadMobile();

						goto case 0;
					}
					case 0:
					{
						m_End = reader.ReadDeltaTime();

						m_Timer = new InternalTimer( this, TimeSpan.Zero, true, true );
						m_Timer.Start();

						break;
					}
				}

				if( version < 2 )
					m_Damage = 2;
			}

			public override bool OnMoveOver( Mobile m )
			{
				if ( Visible && m_Caster != null && (!Core.AOS || m != m_Caster) && SpellHelper.ValidIndirectTarget( m_Caster, m ) && m_Caster.CanBeHarmful( m, false ) )
				{
					if ( SpellHelper.CanRevealCaster( m ) )
						m_Caster.RevealingAction();
					
					m_Caster.DoHarmful( m );

					int damage = m_Damage;

					if ( !Core.AOS && m.CheckSkill( SkillName.MagicResist, 0.0, 30.0 ) )
					{
						damage = 1;

						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					AOS.Damage( m, m_Caster, damage, 0, 100, 0, 0, 0 );
					m.PlaySound( 0x208 );

					if ( m is BaseCreature )
						((BaseCreature) m).OnHarmfulSpell( m_Caster );
				}

				return true;
			}

			private class InternalTimer : Timer
			{
				private FireFieldItem m_Item;
				private bool m_InLOS, m_CanFit;

				private static Queue m_Queue = new Queue();

				public InternalTimer( FireFieldItem item, TimeSpan delay, bool inLOS, bool canFit ) : base( delay, TimeSpan.FromSeconds( 1.0 ) )
				{
					m_Item = item;
					m_InLOS = inLOS;
					m_CanFit = canFit;

					Priority = TimerPriority.FiftyMS;
				}

				protected override void OnTick()
				{
					if ( m_Item.Deleted )
						return;

					if ( !m_Item.Visible )
					{
						if ( m_InLOS && m_CanFit )
							m_Item.Visible = true;
						else
							m_Item.Delete();

						if ( !m_Item.Deleted )
						{
							m_Item.ProcessDelta();
							Effects.SendLocationParticles( EffectItem.Create( m_Item.Location, m_Item.Map, EffectItem.DefaultDuration ), 0x376A, 9, 10, 5029 );
						}
					}
					else if ( DateTime.Now > m_Item.m_End )
					{
						m_Item.Delete();
						Stop();
					}
					else
					{
						Map map = m_Item.Map;
						Mobile caster = m_Item.m_Caster;

						if ( map != null && caster != null )
						{
							foreach ( Mobile m in m_Item.GetMobilesInRange( 0 ) )
							{
								if ( (m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && (!Core.AOS || m != caster) && SpellHelper.ValidIndirectTarget( caster, m ) && caster.CanBeHarmful( m, false ) )
									m_Queue.Enqueue( m );
							}

							while ( m_Queue.Count > 0 )
							{
								Mobile m = (Mobile)m_Queue.Dequeue();
								
								if ( SpellHelper.CanRevealCaster( m ) )
									caster.RevealingAction();

								caster.DoHarmful( m );

								int damage = m_Item.m_Damage;

								if ( !Core.AOS && m.CheckSkill( SkillName.MagicResist, 0.0, 30.0 ) )
								{
									damage = 1;

									m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
								}

								AOS.Damage( m, caster, damage, 0, 100, 0, 0, 0 );
								m.PlaySound( 0x208 );

								if ( m is BaseCreature )
									((BaseCreature) m).OnHarmfulSpell( caster );
							}
						}
					}
				}
			}
		}
	}

	public class BarredMetalDoor : BaseDoor
	{
		[Constructable]
		public BarredMetalDoor( DoorFacing facing ) : base( 0x685 + (2 * (int)facing), 0x686 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public BarredMetalDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class BarredMetalDoor2 : BaseDoor
	{
		[Constructable]
		public BarredMetalDoor2( DoorFacing facing ) : base( 0x1FED + (2 * (int)facing), 0x1FEE + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public BarredMetalDoor2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RattanDoor : BaseDoor
	{
		[Constructable]
		public RattanDoor( DoorFacing facing ) : base( 0x695 + (2 * (int)facing), 0x696 + (2 * (int)facing), 0xEB, 0xF2, BaseDoor.GetOffset( facing ) )
		{
		}

		public RattanDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DarkWoodDoor : BaseDoor
	{
		[Constructable]
		public DarkWoodDoor( DoorFacing facing ) : base( 0x6A5 + (2 * (int)facing), 0x6A6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public DarkWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MediumWoodDoor : BaseDoor
	{
		[Constructable]
		public MediumWoodDoor( DoorFacing facing ) : base( 0x6B5 + (2 * (int)facing), 0x6B6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public MediumWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MetalDoor2 : BaseDoor
	{
		[Constructable]
		public MetalDoor2( DoorFacing facing ) : base( 0x6C5 + (2 * (int)facing), 0x6C6 + (2 * (int)facing), 0xEC, 0xF3, BaseDoor.GetOffset( facing ) )
		{
		}

		public MetalDoor2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LightWoodDoor : BaseDoor
	{
		[Constructable]
		public LightWoodDoor( DoorFacing facing ) : base( 0x6D5 + (2 * (int)facing), 0x6D6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public LightWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class StrongWoodDoor : BaseDoor
	{
		[Constructable]
		public StrongWoodDoor( DoorFacing facing ) : base( 0x6E5 + (2 * (int)facing), 0x6E6 + (2 * (int)facing), 0xEA, 0xF1, BaseDoor.GetOffset( facing ) )
		{
		}

		public StrongWoodDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer ) // Default Serialize method
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader ) // Default Deserialize method
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}