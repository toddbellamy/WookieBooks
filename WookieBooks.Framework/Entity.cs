using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WookieBooks.DomainFramework
{
    public abstract class Entity : IInternalEventHandler
    {
        public EntityId Id { get; protected set; }
        
        protected Entity() { }

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
        }

        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}
