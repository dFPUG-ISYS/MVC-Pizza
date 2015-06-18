using System.Collections.Generic;

namespace MVCPizza
{
    public interface IControllerHooks
    {
        void OnAdd();
        void OnUpdate();

        List<string> ErrorMessages { get; set; }

        List<ControllerAction> actions { get; }   
    }
}
