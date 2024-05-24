using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingWebAPI.Information;
using System.Drawing;

namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingWebController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShoppingWebController> _logger;
        private readonly string _connectionString;

        public ShoppingWebController(ILogger<ShoppingWebController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = ConfigurationExtensions.GetConnectionString(_configuration, "ShoppingWeb");
        }

        [HttpGet]
        [Route("GetProduct")]
        public ProductInfo GetProduct()
        {
            ProductInfo productInfo = new ProductInfo(_connectionString);
            productInfo.Load("0000000001");

            return productInfo;
        }

        [HttpGet]
        [Route("GetProductImage")]
        public ProductImageInfo GetProductImage()
        {
            ProductImageInfo productImageInfo = new ProductImageInfo(_connectionString);
            productImageInfo.Load("0000000001");

            return productImageInfo;
        }

        [HttpPost]
        [Route("InsertProductImage")]
        public void InsertProductImage()
        {
            byte[] imageData;
            using (FileStream fs = new FileStream("E:\\PracticeProject\\shopping\\ShoppingWeb\\shoppingweb\\public\\images\\p1_fromDB.png", FileMode.Open, FileAccess.Read))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, (int)fs.Length);
            }

            ProductImageInfo productImageInfo = new ProductImageInfo(_connectionString);
            productImageInfo.ProductSN = "0000000001";
            productImageInfo.ProductImage = imageData;
            productImageInfo.Creator = "9999999";
            productImageInfo.CreatedDate = DateTime.Now;
            productImageInfo.Insert();
        }
    }
}
