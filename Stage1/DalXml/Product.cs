using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;
internal class Product : IProduct
{
    public int Add(DO.Product p)
    {
        XElement? config = XDocument.Load(@"..\..\xml\config.xml").Root;
        XElement? productId = config?.Element("ProductId");
        p.Id = Convert.ToInt32(productId?.Value??"");
        productId.Value = (p.Id +1).ToString();
        config?.Save(@"..\..\xml\config.xml");
        XElement? root = XDocument.Load(@"..\..\xml\Product.xml").Root;
        //List<XElement> xproducts = root?.Elements("Product").ToList()?? new();
        root?.Add(
            new XElement("Product",
                new XElement("Id", p.Id),
                new XElement("Name", p.Name),
                new XElement("Category", p.Category),
                new XElement("Price", p.Price),
                new XElement("Amount", p.Amount)
            )
        );
        root?.Save(@"..\..\xml\Product.xml");
        //p.Id = DataSource.Config.ProductId;
        //DataSource._products.Add(p);
        return p.Id;
    }

    public void Delete(int id)
    {

    }

    public IEnumerable<DO.Product> Read(Func<DO.Product, bool>? func = null)
    {
        XElement? root = XDocument.Load(@"..\..\xml\Product.xml").Root;
        List<XElement> xproducts = root?.Elements("Product").ToList()?? new();
        List<DO.Product> products = new();
        for (int i = 0; i<xproducts.Count; i++)
        {
            products.Add(new DO.Product()
            {
                Id = Convert.ToInt32(xproducts[i]?.Element("Id")?.Value),
                Name = xproducts[i]?.Element("Name")?.Value,
                Category = (ECategory)Enum.Parse(typeof(ECategory), xproducts[i]?.Element("Category")?.Value??"0"),
                Amount = Convert.ToInt32(xproducts[i]?.Element("Amount")?.Value),
                Price = Convert.ToDouble(xproducts[i]?.Element("Price")?.Value),
            });
        }
        if (func==null)
            return products;
        return products.Where(func);
        //XElement xproducts = XElement.Load(@"..\..\xml\Product.xml");
        //.Elements("Product").;
        //List<DO.Product> a = new();
        //a.Add(new DO.Product { Id=1, Amount = 2, Category = ECategory.Biography, Name="my life", Price = 55.5 });
        //a.Add(new DO.Product { Id=2, Amount = 10, Category = ECategory.Children, Name="dadi gamadi", Price = 13.9 });
        //return a;
    }

    public DO.Product ReadSingle(Func<DO.Product, bool> func)
    {
        return new DO.Product();
    }

    public void Update(DO.Product item)
    {

    }
}

