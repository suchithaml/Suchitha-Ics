
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Products
{
    public partial class Products : System.Web.UI.Page
    {
        // Define a dictionary to store product details (name, image URL, price)
        private static readonly Dictionary<string, (string ImageUrl, decimal Price)> products = new Dictionary<string, (string, decimal)>
        {
            { "Laptop", ("~/images/laptop.jpg", 99999.00m) },
            { "Smartphone", ("~/images/smartphone.jpg", 48999.00m) },
            { "Tablet", ("~/images/tablet.jpg", 26999.00m) }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind dropdown list with products
                ddlProducts.DataSource = products.Keys;
                ddlProducts.DataBind();
            }
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected product
            string selectedProduct = ddlProducts.SelectedValue;

            if (products.TryGetValue(selectedProduct, out var productInfo))
            {
                // Display image and price
                imgProduct.ImageUrl = productInfo.ImageUrl;
                lblPrice.Text = "";
            }
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            // Get the selected product
            string selectedProduct = ddlProducts.SelectedValue;

            if (products.TryGetValue(selectedProduct, out var productInfo))
            {
                // Display price
                lblPrice.Text = $"Price: ₹{productInfo.Price}";
            }
        }
    }
}
