using System;
using System.Reflection;

namespace ScriptNET.Runtime.ObjectModel.Binder
{
    [Obsolete()]
    internal class FieldBind : IMemberBind
    {
        public FieldBind(object target, FieldInfo field)
        {
            Target = target ?? throw new ArgumentNullException("target");
            TargetType = target.GetType();
            this.field = field ?? throw new ArgumentNullException("field");
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

        private FieldInfo field;

        public MemberInfo Member
        {
            get
            {
                return field;
            }
        }

        public void SetValue(object value)
        {
            field.SetValue(Target, value);
        }

        public object GetValue()
        {
            return field.GetValue(Target);
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