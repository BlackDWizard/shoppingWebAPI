using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingWebAPI.Information;

namespace ShoppingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductController> _logger;
        private readonly string _connectionString;

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = ConfigurationExtensions.GetConnectionString(_configuration, "ShoppingWeb");
        }

        [HttpGet(Name = "GetProduct")]
        public ProductInfo Get()
        {
            ProductInfo productInfo = new ProductInfo(_connectionString);
            productInfo.Load("0000000001");

            return productInfo;
        }
    }
}
