using System;

namespace FiestaLib.Util
{
    public enum InitializationStage
    {
        DatabaseHandling = 0,
        HandlerStore = 1,

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TunnelModuleAttribute : Attribute
    {
        private readonly InitializationStage stageInternal;
        public InitializationStage InitializationStage { get { return stageInternal; } }

        public TunnelModuleAttribute(InitializationStage initializationStage)
        {
            stageInternal = initializationStage;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class InitializerMethodAttribute : Attribute { }
}
