using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace BusinessLogic
{
    public class Serializer
    {
        public string Serialize(int catalogId)
        {
            var catalog = GetCatalogById(catalogId);

            var xmlSerializer = new XmlSerializer(typeof(Catalog));

            using (var writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, catalog);
                return writer.ToString();
            }
        }

        public void Deserialize(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(Catalog));

            using (var reader = new StringReader(xml))
            {
                var catalog = (Catalog)xmlSerializer.Deserialize(reader);
                CreateCatalog(catalog);
            }
        }

        private Catalog GetCatalogById(int catalogId)
        {
            var catalogRepository = new CatalogRepository();
            return catalogRepository.GetAll().Single(c => c.Id == catalogId);
        }

        private void CreateCatalog(Catalog catalog)
        {
            var catalogRepository = new CatalogRepository();
            catalogRepository.Create(catalog);
        }
    }
}
