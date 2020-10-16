using Android.Content;
using Android.Content.Res;
using Android.Gms.Ads;
using Android.Views;
using Xamarin.Forms.Platform.Android;

namespace Atomus.Ads.Controls
{
    public class GoogleAdmobBanner : IAdsControl
    {
        Context context;
        ViewGroup Control;
        int heightPixels;
        float density;
        string adUnitId;

        public GoogleAdmobBanner() 
        {
            try
            {
                this.adUnitId = this.GetAttribute("AdUnitId");
            }
            catch (System.Exception)
            {
            }
        }
        public GoogleAdmobBanner(Context context, ViewGroup viewGroup, int heightPixels, float density) : this()
        {
            this.context = context;
            this.Control = viewGroup;
            this.heightPixels = heightPixels;
            this.density = density;
        }

        private int GetSmartBannerDpHeight()
        {
            //var dpHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;
            var dpHeight = this.heightPixels / this.density;

            if (dpHeight <= 400) return 32;

            if (dpHeight > 400 && dpHeight <= 720) return 50;

            return 90;
        }
        object IAdsControl.CreateViewGroup(object e, string AdUnitId)
        {

            if (Control == null && e is ElementChangedEventArgs<AdsBannerControl>)
            {
                if (!this.adUnitId.IsNullOrEmpty())
                    AdUnitId = this.adUnitId;

                var ad = new AdView(this.context)
                {
                    AdSize = AdSize.SmartBanner,
                    AdUnitId = AdUnitId,
                };

                var requestbuilder = new AdRequest.Builder();

                ad.LoadAd(requestbuilder.Build());
                (e as ElementChangedEventArgs<AdsBannerControl>).NewElement.HeightRequest = GetSmartBannerDpHeight();
                //SetNativeControl(ad);
                return ad;
            }
            else
                return null;
        }
    }
}