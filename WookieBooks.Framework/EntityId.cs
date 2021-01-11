using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WookieBooks.DomainFramework
{
    public record EntityId : Value<EntityId>
    {
        public Guid Value { get; internal set; }

        protected EntityId() { }

        public EntityId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Entity Id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(EntityId self) => self.Value;

        public static implicit operator EntityId(string value)
            => new EntityId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
