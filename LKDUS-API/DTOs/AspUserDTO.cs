using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class AspUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static implicit operator AspUserDTO(AspUserCreateDTO v)
        {
            throw new NotImplementedException();
        }
    }

    public class AspUserCreateDTO
    {

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
