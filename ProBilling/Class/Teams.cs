using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProBilling.Class
{
	public enum Teams
	{
		/// <summary>
		/// Standaard
		/// </summary>
		[Description("Team One")]
		TeamOne = 0,

		/// <summary>
		/// ABU
		/// </summary>
		[Description("Team Two")]
		TeamTwo = 1,

		/// <summary>
		/// VPO
		/// </summary>
		[Description("Team three")]
		TeamThree = 2,

		/// <summary>
		/// ABU Plus
		/// </summary>
		[Description("Team Four")]
		TeamFour = 3,
	}
}
