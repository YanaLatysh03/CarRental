using Car.Models.Request;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Car.Models.Response
{
    public class CreatePaginationResponseModel
    {
        public IList<GetCarCatalogueResponseModel> Cars { get; set; }

        public Pages Pages { get; set; }

        public ApplyFilterRequestModel Filter { get; set; }

        public bool IsApplyFilter { get; set; }

        public IList<Color> Color = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();

        public IList<Brand> Brand = Enum.GetValues(typeof(Brand)).Cast<Brand>().ToList();

        public IList<Access> Access = Enum.GetValues(typeof(Access)).Cast<Access>().ToList();
    }
}
