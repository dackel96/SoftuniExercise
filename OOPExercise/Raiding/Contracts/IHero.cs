using System;
using System.Collections.Generic;
using System.Text;

namespace Raiding
{
    public interface IHero
    {
        public string Name { get; }
        public int Power { get; }
        public void CastAbility();
    }
}
