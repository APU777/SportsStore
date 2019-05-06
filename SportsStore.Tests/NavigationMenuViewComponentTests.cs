using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;
using SportsStore.Components;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Organization
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "P1", Category = "Apples"},
                new Product{ProductID = 2, Name = "P2", Category = "Apples"},
                new Product{ProductID = 3, Name = "P3", Category = "Plums"},
                new Product{ProductID = 4, Name = "P4", Category = "Oranges"}
            });
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Action - getting a set of category
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            //Statement
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }
    }
}