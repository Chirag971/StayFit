using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace StayFitz.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(
         new[] { Intent.ActionView },
         Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
         DataScheme = "msalYOUR_CLIENT_ID",
         DataHost = "auth")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
