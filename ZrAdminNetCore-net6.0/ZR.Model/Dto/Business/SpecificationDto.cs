using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZR.Model.Myself;

namespace ZR.Model.Dto.Business
{
    public class SpecificationDto
    {


    }

    public class SpecificationSaveAccept
    {
        public Specification form { get; set; }


        public string SpecificationType { get; set; }
    }

    public class SpecificationQueryAccept
    {
        public MyPagerInfo pagerinfo { get; set; }


        public string SpecificationNameQuery { get; set; }
    }

}
