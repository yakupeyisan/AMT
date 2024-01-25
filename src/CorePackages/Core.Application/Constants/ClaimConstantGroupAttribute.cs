using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Constants;
public class ClaimConstantGroupAttribute : Attribute
{
    public string GroupName { get; }
    public ClaimConstantGroupAttribute(string groupName)
    {
        GroupName = groupName;
    }
}
