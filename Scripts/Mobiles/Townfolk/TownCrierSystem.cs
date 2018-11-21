using System;
using Server;

namespace Server.Misc
{
	public class TownCrierSystem
	{
		private int m_Count;
		
		/*		public static void Initialize()
		{
			Count = 0;
		}*/
		
		public TownCrierSystem()
		{
			Count = 0;
		}
		
		public int Count
		{
			get
			{
				return m_Count;
			}
			set { m_Count = value; }
		}
		
		public void AddTownCrier()
		{
			Count += 1;
		}
	}
}
