using SportsStore.Models;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Organization - creation of several test goods
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            //Organization = creation new basket
            Cart target = new Cart();
            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();
            //Statement
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }
        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Organization - creation of several test goods
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            //Organization = creation new basket
            Cart target = new Cart();
            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
            //Statement
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity
);
        }
        [Fact]
        public void Can_Remove_Line()
        {
            //Organization - creation of several test goods
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };
            //Organization = creation new basket
            Cart target = new Cart();
            //Organization - add some items to the basket
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            //Action
            target.RemoveLine(p2);
            CartLine[] results = target.Lines.ToArray();
            //Statement
            Assert.Equal(0, target.Lines.Where(c => c.Product == p2).Count());
            Assert.Equal(2, target.Lines.Count());
           
        }
        [Fact]
        public void Calculate_Cart_Total()
        {
            //Organization - creation of several test goods
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            //Organization = creation new basket
            Cart target = new Cart();
            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            //Statement

            Assert.Equal(450M, result);
        }
        [Fact]
        public void Can_Clear_Contents()
        {
            //Organization - creation of several test goods
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            //Organization = creation new basket
            Cart target = new Cart();
            //Organization - add some items to the basket
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            //Action - clear basket
            target.Clear();
            //Statement
            Assert.Equal(0, target.Lines.Count());
        }
    }
}
