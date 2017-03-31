﻿using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopifySharp
{
    /// <summary>
    /// A service for manipulating Shopify's UsageCharge API.
    /// </summary>
    public class UsageChargeService : ShopifyService
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="UsageChargeService" />.
        /// </summary>
        /// <param name="myShopifyUrl">The shop's *.myshopify.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public UsageChargeService(string myShopifyUrl, string shopAccessToken) : base(myShopifyUrl, shopAccessToken) { }

        #endregion

        #region Public, non-static methods

        /// <summary>
        /// Creates a <see cref="UsageCharge"/>. 
        /// </summary>
        /// <param name="recurringChargeId">The id of the <see cref="UsageCharge"/> that this usage charge belongs to.</param>
        /// <param name="description">The name or description of the usage charge.</param>
        /// <param name="price">The price of the usage charge.</param>
        /// <returns>The new <see cref="UsageCharge"/>.</returns>
        public virtual async Task<UsageCharge> CreateAsync(long recurringChargeId, string description, double price)
        {
            var req = RequestEngine.CreateRequest($"recurring_application_charges/{recurringChargeId}/usage_charges.json", Method.POST, "usage_charge");
            
            req.AddJsonBody(new { usage_charge = new { description = description, price = price } });

            return await RequestEngine.ExecuteRequestAsync<UsageCharge>(_RestClient, req);
        }

        /// <summary>
        /// Retrieves the <see cref="UsageCharge"/> with the given id.
        /// </summary>
        /// <param name="recurringChargeId">The id of the recurring charge that this usage charge belongs to.</param>
        /// <param name="id">The id of the charge to retrieve.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The <see cref="UsageCharge"/>.</returns>
        public virtual async Task<UsageCharge> GetAsync(long recurringChargeId, long id, string fields = null)
        {
            var req = RequestEngine.CreateRequest($"recurring_application_charges/{recurringChargeId}/usage_charges/{id}.json", Method.GET, "usage_charge");

            if (string.IsNullOrEmpty(fields) == false)
            {
                req.AddParameter("fields", fields);
            }

            return await RequestEngine.ExecuteRequestAsync<UsageCharge>(_RestClient, req);
        }

        /// <summary>
        /// Retrieves a list of all past and present <see cref="UsageCharge"/> objects.
        /// </summary>
        /// <param name="recurringChargeId">The id of the recurring charge that these usage charges belong to.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        /// <returns>The list of <see cref="UsageCharge"/> objects.</returns>
        public virtual async Task<IEnumerable<UsageCharge>> ListAsync(long recurringChargeId, string fields = null)
        {
            var req = RequestEngine.CreateRequest($"recurring_application_charges/{recurringChargeId}/usage_charges.json", Method.GET, "usage_charges");

            if (string.IsNullOrEmpty(fields) == false)
            {
                req.AddParameter("fields", fields);
            }            

            return await RequestEngine.ExecuteRequestAsync<List<UsageCharge>>(_RestClient, req);
        }

        #endregion
    }
}