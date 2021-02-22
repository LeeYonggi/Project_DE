using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface BaseScene
{
    void Start();
    void Update();
    void FixedUpdate();
    void LateUpdate();
    void Destroy();
    void Load();
}

