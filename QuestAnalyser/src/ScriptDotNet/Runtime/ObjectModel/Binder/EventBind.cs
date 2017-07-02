using System;
using System.Reflection;

namespace ScriptNET.Runtime.ObjectModel.Binder
{
    [Obsolete()]
    internal class EventBind : IMemberBind
    {
        public EventBind(object target, EventInfo @event)
        {
            Target = target ?? throw new ArgumentNullException("target");
            TargetType = target.GetType();
            this.@event = @event ?? throw new ArgumentNullException("event");
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

        private EventInfo @event;

        public MemberInfo Member
        {
            get
            {
                return @event;
            }
        }

        public void SetValue(object value)
        {
            throw new NotSupportedException();
        }

        public object GetValue()
        {
            throw new NotSupportedException();
        }

        public void AddHandler(object value)
        {
            EventBroker.AssignEvent(@event, Target, (IInvokable)value);
        }

        public void RemoveHandler(object value)
        {
            EventBroker.RemoveEvent(@event, Target, (IInvokable)value);
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