namespace MapShapes.Domain
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    public abstract class Enumeration<TEnumeration> : Enumeration<TEnumeration, int>
        where TEnumeration : Enumeration<TEnumeration>
    {
        protected Enumeration(int value, string displayName)
            : base(value, displayName)
        {
        }

        public static TEnumeration FromInt32(int value)
        {
            return FromValue(value);
        }

        public static bool TryFromInt32(int listItemValue, out TEnumeration result)
        {
            return TryParse(listItemValue, out result);
        }
    }

    [Serializable]
    [DebuggerDisplay("{DisplayName} - {Value}")]
    [DataContract(Namespace = "http://github.com/HeadspringLabs/Enumeration/5/13")]
    public abstract class Enumeration<TEnumeration, TValue> : IComparable<TEnumeration>, IEquatable<TEnumeration>
        where TEnumeration : Enumeration<TEnumeration, TValue>
        where TValue : IComparable
    {
        private static readonly Lazy<TEnumeration[]> Enumerations = new Lazy<TEnumeration[]>(GetEnumerations);

        protected Enumeration(TValue value, string displayName)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            this.Value = value;
            this.DisplayName = displayName;
        }

        [field: DataMember(Order = 1)]
        public string DisplayName { get; }

        [field: DataMember(Order = 0)]
        public TValue Value { get; }

        public int CompareTo(TEnumeration other)
        {
            return this.Value.CompareTo(other == default(TEnumeration) ? default(TValue) : other.Value);
        }

        public bool Equals(TEnumeration other)
        {
            return other != null && this.ValueEquals(other.Value);
        }

        public static TEnumeration FromValue(TValue value)
        {
            return Parse(value, "value", item => item.Value.Equals(value));
        }

        public static TEnumeration[] GetAll()
        {
            return Enumerations.Value;
        }

        public static bool operator ==(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
        {
            return !Equals(left, right);
        }

        public static TEnumeration Parse(string displayName)
        {
            return Parse(displayName, "display name", item => item.DisplayName == displayName);
        }

        public static bool TryParse(TValue value, out TEnumeration result)
        {
            return TryParse(e => e.ValueEquals(value), out result);
        }

        public static bool TryParse(string displayName, out TEnumeration result)
        {
            return TryParse(e => e.DisplayName == displayName, out result);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TEnumeration);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public sealed override string ToString()
        {
            return this.DisplayName;
        }

        protected virtual bool ValueEquals(TValue value)
        {
            return this.Value.Equals(value);
        }

        private static TEnumeration[] GetEnumerations()
        {
            var enumerationType = typeof(TEnumeration);
            return enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(info => enumerationType.IsAssignableFrom(info.FieldType))
                .Select(info => info.GetValue(null))
                .Cast<TEnumeration>()
                .ToArray();
        }

        private static TEnumeration Parse(object value, string description, Func<TEnumeration, bool> predicate)
        {
            if (!TryParse(predicate, out var result))
            {
                string message = $"'{value}' is not a valid {description} in {typeof(TEnumeration)}";
                throw new ArgumentException(message, "value");
            }

            return result;
        }

        private static bool TryParse(Func<TEnumeration, bool> predicate, out TEnumeration result)
        {
            result = GetAll().FirstOrDefault(predicate);
            return result != null;
        }
    }
}