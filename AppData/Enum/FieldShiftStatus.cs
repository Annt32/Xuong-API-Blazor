using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Enum
{
	public enum FieldShiftStatus
	{
		Canceled = 0,
		WaitForPayment = 1,
		WaitForConfirmation = 2,
		WaitingForCheckin = 3,
	    Renting = 4,
		Cancel = 5,
		Delete = 6
	}
}
