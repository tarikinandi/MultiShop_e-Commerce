using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }

        public void TInsert(CargoOperation entity)
        {
            _cargoOperationDal.Insert(entity);
        }

        public void TUpdate(CargoOperation entity)
        {
            _cargoOperationDal.Update(entity);
        }

        public void TDelete(int id)
        {
            _cargoOperationDal.Delete(id);
        }

        public CargoOperation TGetById(int id)
        {
            var value = _cargoOperationDal.GetById(id);
            return value;
        }

        public List<CargoOperation> TGetAll()
        {
            var values = _cargoOperationDal.GetAll();
            return values;
        }
    }
}
