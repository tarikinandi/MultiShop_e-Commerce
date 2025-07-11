using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos
{
    public class CreateCargoDetailDTO
    {
        public string SenderCustomer { get; set; }
        public string ReceiverCustomer { get; set; }
        public string Barcode { get; set; }
        public int CargoCompanyId { get; set; }
    }
}
