using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x13BB, 0x13C0 )]
	public class NewChainCoif : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override int AosStrReq{ get{ return 60; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 28; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }


		[Constructable]
		public NewChainCoif() : base( 0x13BB )
		{
			Weight = 1.0;
			Hue = 1323;
			Name = "mithril coif";
		}

		public NewChainCoif( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}