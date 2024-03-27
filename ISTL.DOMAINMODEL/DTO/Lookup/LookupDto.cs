using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.MODELS.DTO.Lookup
{
    public class LookupDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long ParentId { get; set; }
        public sbyte Status { get; set; }
        public bool HasParent { get; set; }

        public LookupDto()
        {
        }

        public LookupDto(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public LookupDto(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public LookupDto(string type)
        {
            Type = type;
        }

        public LookupDto(string type, long parentId, bool hasParent)
        {
            Type = type;
            ParentId = parentId;
            HasParent = hasParent;
        }
    }
}
