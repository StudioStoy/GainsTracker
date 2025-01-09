using Android.App;
using Android.Content.PM;

namespace GainsTracker.ClientNative;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter([Android.Content.Intent.ActionView],
              Categories = [
                Android.Content.Intent.CategoryDefault,
                Android.Content.Intent.CategoryBrowsable
              ],
              DataScheme = CallbackScheme)]
public class WebAuthenticationCallbackActivity : WebAuthenticatorCallbackActivity
{
    private const string CallbackScheme = "gainstracker";
}
