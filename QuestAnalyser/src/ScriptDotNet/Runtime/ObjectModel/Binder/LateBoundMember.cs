using System;
using System.Reflection;

namespace ScriptNET.Runtime
{
    internal class LateBoundMember : IMemberBind
    {
        private ObjectBinder binder;
        private bool throwNotFound;

        public LateBoundMember(ObjectBinder binder, object target, string name, bool throwNotFound)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.binder = binder ?? throw new ArgumentNullException("binder");
            this.throwNotFound = throwNotFound;

            Target = target ?? throw new ArgumentNullException("target");
            TargetType = target as Type;
            if (TargetType == null) TargetType = target.GetType();
            MemberName = name;
        }

        #region IMemberBind Members

        public string MemberName
        {
            get;
            private set;
        }

        public object Target
        {
            get;
            private set;
        }

        public Type TargetType
        {
            get;
            private set;
        }

        public MemberInfo Member
        {
            get
            {
                throw new NotSupportedException("Member info is not supported on late bound");
            }
        }

        public void SetValue(object value)
        {
            binder.Set(MemberName, Target, value, throwNotFound, null);
        }

        public object GetValue()
        {
            return binder.Get(MemberName, Target, throwNotFound, null);
        }

        public void AddHandler(object value)
        {
            //This also may set value to property
            binder.Set(MemberName, Target, value, true, null);
        }

        public void RemoveHandler(object value)
        {
            //This also may set value to property
            binder.Set(MemberName, Target, value, true, null);
        }

        #endregion IMemberBind Members

        #region IInvokable Members

        public bool CanInvoke()
        {
            return Target != null && Member != null;
        }

        public object Invoke(IScriptContext context, object[] args)
        {
            return GetValue();
        }

        #endregion IInvokable Members
    }
}