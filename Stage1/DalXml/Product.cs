using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;
internal class Product : IProduct
{
    public int Add(DO.Product p)
    {
        XElement? config = XDocument.Load(@"..\..\xml\config.xml").Root;
        XElement? productId = config?.Element("ProductId");
        p.Id = Convert.ToInt32(productId?.Value ?? "");
        productId.Value = (p.Id + 1).ToString();
        config?.Save(@"..\..\xml\config.xml");
        XmlSerializer serializer = new(typeof(List<DO.Product>), new XmlRootAttribute() { ElementName = "ArrayOfProduct", IsNullable = false });
        StreamReader reader = new(@"..\..\xml\Product.xml");
        List<DO.Product> xml = (List<DO.Product>)serializer.Deserialize(reader);
        xml?.Add(p);
        reader.Close();
        StreamWriter writer = new(@"..\..\xml\Product.xml");
        serializer.Serialize(writer, xml);
        writer.Close();
        return p.Id;
    }

    public void Delete(int id)
    {

    }

    public IEnumerable<DO.Product> Read(Func<DO.Product, bool>? func = null)
    {
        XmlSerializer serializer = new(typeof(List<DO.Product>), new XmlRootAttribute() { ElementName = "ArrayOfProduct" });
        StreamReader reader = new(@"..\..\xml\Product.xml");
        List<DO.Product> products = (List<DO.Product>)serializer.Deserialize(reader);
        reader.Close();
        if (func == null)
            return products;
        return from p in products
               where func(p)
               select p;
    }

    public DO.Product ReadSingle(Func<DO.Product, bool> func)
    {
        XmlSerializer serializer = new(typeof(List<DO.Product>), new XmlRootAttribute() { ElementName = "ArrayOfProduct" });
        StreamReader reader = new(@"..\..\xml\Product.xml");
        List<DO.Product> products = (List<DO.Product>)serializer.Deserialize(reader);
        reader.Close();
        return (from p in products
                where func(p)
                select p).First();
    }

    public void Update(DO.Product p)
    {
        XmlSerializer serializer = new(typeof(List<DO.Product>), new XmlRootAttribute() { ElementName = "ArrayOfProduct" });
        StreamReader reader = new(@"..\..\xml\Product.xml");
        List<DO.Product> products = (List<DO.Product>)serializer.Deserialize(reader);
        reader.Close();
        products[products.FindIndex(pr => pr.Id == p.Id)] = p;
        StreamWriter writer = new(@"..\..\xml\Product.xml");
        serializer.Serialize(writer, products);
        writer.Close();
    }
}

