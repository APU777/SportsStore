using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using SportsStore.Controllers;
using SportsStore.Models;


namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Organization
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //Action
            IEnumerable<Product> result = controller.List(2).ViewData.Model as IEnumerable<Product>;
        }
    }
}
