using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dog : Animal
{
    protected override void Start()
    {
        base.Start();
        WalkBehavior = new Walk();
        IdleBehavior = new Idle();
    }

    protected override void Update()
    {
        base.Update();
    }
}
