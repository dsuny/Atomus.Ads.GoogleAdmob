using Android.Gms.Ads;
using Xamarin.Forms;

namespace Atomus.Ads.Controls
{
    public class GoogleAdmobInterstitial : AdListener, IInterstitial
    {
        int cnt;
        int cntMod;
        string adUnitId;
        InterstitialAd interstitialAd;
        public GoogleAdmobInterstitial() 
        {
            this.cnt = 0;
            try
            {
                this.cntMod = this.GetAttributeInt("IntervalCount");
            }
            catch (System.Exception)
            {
                this.cntMod = 10;
            }

            try
            {
                this.adUnitId = this.GetAttribute("AdUnitId");
            }
            catch (System.Exception)
            {
            }
        }

        void IInterstitial.Show(string adUnitId)
        {
            this.cnt += 1;

            if (this.cnt % this.cntMod != 0)
                return;

            this.cnt = 0;

            if (this.interstitialAd == null)
            {
                if (!this.adUnitId.IsNullOrEmpty())
                    adUnitId = this.adUnitId;

                this.interstitialAd = new InterstitialAd(Android.App.Application.Context);
                this.interstitialAd.AdListener = this;
                this.interstitialAd.AdUnitId = adUnitId;
            }

            OnAdLoaded();

            this.interstitialAd.LoadAd(new AdRequest.Builder().Build());
        }

        public override void OnAdClosed()
        {
        }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            if (this.interstitialAd.IsLoaded)
                this.interstitialAd.Show();
        }
    }
}