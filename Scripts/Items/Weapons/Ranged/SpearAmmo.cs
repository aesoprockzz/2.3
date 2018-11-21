using System;

namespace Server.Items
{
	public class SpearAmmo : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public SpearAmmo() : this( 1 )
		{
		}

		[Constructable]
		public SpearAmmo( int amount ) : base( 0xF62 )
		{
			Stackable = true;
			Amount = amount;
		}

		public SpearAmmo( Serial serial ) : base( serial )
		{
		}

		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}