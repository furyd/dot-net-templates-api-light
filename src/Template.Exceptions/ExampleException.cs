using System;
using System.Runtime.Serialization;

namespace Template.Exceptions
{
    [Serializable]
    public class ExampleException : Exception
    {
        public string ExampleProperty { get; set; }

        public ExampleException()
        {
        }

        public ExampleException(string message) : base(message)
        {
        }

        public ExampleException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExampleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ExampleProperty = info.GetString(nameof(ExampleProperty));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ExampleProperty), ExampleProperty);
            base.GetObjectData(info, context);
        }
    }
}