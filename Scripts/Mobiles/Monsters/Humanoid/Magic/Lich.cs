using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a liche's corpse" )]
	public class Lich : BaseCreature
	{
		[Constructable]
		public Lich() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lich";
			Body = 24;
			BaseSoundID = 0x3E9;

			SetStr( 171, 200 );
			SetDex( 80, 100 );
			SetInt( 500, 500 );

			SetHits( 75, 100 );

			SetDamage( 9, 29 );

			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 70.0, 80.0 );
			SetSkill( SkillName.Meditation, 100.0, 101.0 );

			SetSkill( SkillName.MagicResist, 100.0, 100.0 );
			SetSkill( SkillName.Tactics, 100.0, 100.0 );
			SetSkill( SkillName.Anatomy, 100.0, 100.0 );
			SetSkill( SkillName.Wrestling, 75.0, 100.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 32;
			PackItem( new GnarledStaff() );
			
			int rnd = Utility.RandomMinMax( 0, 100 );
			int rnd_armor = Utility.RandomMinMax( 0, 120 );

			if ( 50 > rnd )
			{
				PackItem(Loot.RandomGem());
			}
			rnd = Utility.RandomMinMax( 0, 100 );
			if ( 50 > rnd )
			{
				PackItem(LootPackItem.RandomScroll( 2, 8, 8 ));
			}
			
			rnd = Utility.RandomMinMax( 0, 100 );
			if ( 50 > rnd )
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
			//AddLoot( LootPack.Rich );
			//AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.Potions );
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public Lich( Serial serial ) : base( serial )
		{
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