using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : Singleton<Purchaser>, IStoreListener
{
    private static IStoreController m_StoreController;          
    private static IExtensionProvider m_StoreExtensionProvider; 
    public static string donation = "com.owls_eyes.coffee_donation";
    private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 
    
    private static Transform _buyButton;

    void Start() {
        if (m_StoreController == null) {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing() {
        if (IsInitialized()) {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(donation, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized() {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    
    public void BuyDonation(Transform buyButton) {
        BuyProductID(donation, buyButton);
    }

    void BuyProductID(string productId, Transform buyButton) {
        if (IsInitialized()) {
            Product product = m_StoreController.products.WithID(productId);
            _buyButton = buyButton;
            if (product != null && product.availableToPurchase) {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            } else {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        } else {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    //  
    // --- IStoreListener
    //
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error) {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
        if (String.Equals(args.purchasedProduct.definition.id, donation, StringComparison.Ordinal)) {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            UIManager.GetInstance().ShowHeartParticlesAt(_buyButton);
        } else {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}