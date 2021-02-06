using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class CharacterBaseState
{
    public abstract void Start(Character character);
    public abstract void Update(Character character);
    public abstract void End(Character character);
}
