using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class Skeleton : BaseCreature
	{
		[Constructable]
		public Skeleton() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a skeleton";
			Body = Utility.RandomList( 50, 56 );
			BaseSoundID = 0x48D;

			SetStr( 166, 195 );
			SetDex( 80, 100 );
			SetInt( 46, 70 );

			SetHits( 25, 50 );
			SetMana( 0 );

			SetDamage( 9, 29 );

			/*SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 25 );*/

			SetSkill( SkillName.MagicResist, 100.1, 100.0 );
			SetSkill( SkillName.Tactics, 100.1, 100.0 );
			SetSkill( SkillName.Anatomy, 100.1, 100.0 );
			SetSkill( SkillName.Wrestling, 75.1, 100.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 32;

			PackItem( new Club() );
			
			int rnd = Utility.RandomMinMax( 0, 100 );
			int rnd_armor = Utility.RandomMinMax( 0, 120 );

			if ( 15 > rnd )
			{
				if(rnd_armor > 0 && rnd_armor < 10)
					PackItem( new RedLeatherChest() );
				else if(rnd_armor > 10 && rnd_armor < 20)
					PackItem( new RedLeatherLegs() );
				else if(rnd_armor > 20 && rnd_armor < 30)
					PackItem( new RedLeatherCap() );
				else if(rnd_armor > 30 && rnd_armor < 40)
					PackItem( new RedLeatherGorget() );
				else if(rnd_armor > 40 && rnd_armor < 50)
					PackItem( new RedLeatherGloves() );
				else if(rnd_armor > 50 && rnd_armor < 60)
					PackItem( new GreenLeatherChest() );
				else if(rnd_armor > 60 && rnd_armor < 70)
					PackItem( new GreenLeatherLegs() );
				else if(rnd_armor > 70 && rnd_armor < 80)
					PackItem( new GreenLeatherCap() );
				else if(rnd_armor > 80 && rnd_armor < 90)
					PackItem( new GreenLeatherGorget() );
				else if(rnd_armor > 90 && rnd_armor < 100)
					PackItem( new GreenLeatherGloves() );
				else if(rnd_armor > 100 && rnd_armor < 110)
					PackItem( new GreenLeatherArms() );
				else if(rnd_armor > 110 && rnd_armor < 120)
					PackItem( new RedLeatherArms() );
			}
		}

		public override void GenerateLoot()
		{
			//AddLoot( LootPack.Poor );
		}

		public override bool BleedImmune{ get{ return false; } }
		//public override Poison PoisonImmune{ get{ return Poison.Lesser; } }

		public Skeleton( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}