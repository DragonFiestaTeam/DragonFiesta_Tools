using System;
using System.Reflection;

namespace ScriptNET.Runtime.ObjectModel.Binder
{
    [Obsolete()]
    internal class PropertyBind : IMemberBind
    {
        public PropertyBind(object target, PropertyInfo property)
        {
            Target = target ?? throw new ArgumentNullException("target");
            TargetType = target.GetType();
            this.property = property ?? throw new ArgumentNullException("property");
        }

        #region IMemberBind Members

        public object Target
        {
            get;
            protected set;
        }

        public Type TargetType
        {
            get;
            protected set;
        }

        private PropertyInfo property;

        public MemberInfo Member
        {
            get
            {
                return property;
            }
        }

        public void SetValue(object value)
        {
            property.SetValue(Target, value, null);
        }

        public object GetValue()
        {
            return property.GetValue(Target, null);
        }

        public void AddHandler(object value)
        {
            throw new NotSupportedException();
        }

        public void RemoveHandler(object value)
        {
            throw new NotSupportedException();
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