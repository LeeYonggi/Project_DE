using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public virtual void BaseUpdate() { }
    public virtual void BaseFixedUpdate() { }
    public virtual void BaseLateUpdate() { }
}
