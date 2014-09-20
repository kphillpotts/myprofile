using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Phoneword_Droid
{
	[Activity (Label = "@string/callHistory")]
	public class CallHistoryActivity: ListActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate (bundle);

			var phoneNumbers = Intent.Extras.GetStringArrayList ("phone_numbers") ?? new string[0];
			this.ListAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);
		}
	}
}

