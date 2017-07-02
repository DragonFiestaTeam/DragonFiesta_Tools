namespace ScriptNET.Runtime
{
    public interface IScopeActivator
    {
        IScriptScope Create(IScriptScope parent, params object[] args);
    }
}