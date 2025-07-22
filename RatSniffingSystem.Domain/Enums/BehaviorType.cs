using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Enums
{
    public enum BehaviorType
    {
        None = 0,
        Freezing = 1,
        Escaping = 2,
        SelfGrooming = 3,
        SelfBiting = 4,
        Defecation = 5,
        Urination = 6,
        RepetitiveMotion = 7,
        Other = 99
    }
}
