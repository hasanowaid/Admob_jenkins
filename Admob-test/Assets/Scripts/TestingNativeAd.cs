using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class TestingNativeAd : MonoBehaviour
{
    private GameObject icon;
    private bool nativeAdLoaded;
    private NativeAd nativeAd;

    public void RequestNativeAd()
    {
        AdLoader adLoader = new AdLoader.Builder("ca-app-pub-3940256099942544/2247696110")
    .ForNativeAd()
    .Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        adLoader.LoadAd(new AdRequest.Builder().Build());
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("Native ad failed to load: " + e);
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Native ad failed to load: " + args);
    }
    void Update()
    {

        if (this.nativeAdLoaded)
        {
            this.nativeAdLoaded = false;
            // Get Texture2D for icon asset of native ad.
            Texture2D iconTexture = this.nativeAd.GetIconTexture();

            icon = GameObject.CreatePrimitive(PrimitiveType.Quad);
            icon.transform.position = new Vector3(1, 1, 1);
            icon.transform.localScale = new Vector3(1, 1, 1);
            icon.GetComponent<Renderer>().material.mainTexture = iconTexture;

            // Register GameObject that will display icon asset of native ad.
            if (!this.nativeAd.RegisterIconImageGameObject(icon))
            {
                // Handle failure to register ad asset.
            }
        }
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        Debug.Log("Native ad loaded.");
        this.nativeAd = args.nativeAd;
        this.nativeAdLoaded = true;
    }
}