﻿using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductDetailId { get; set; }

        public string ProductDescription { get; set; }

        public string ProductInfo { get; set; }

        public string ProductId { get; set; }

        [BsonIgnore]
        public Product Product { get; set; }
    }
}
