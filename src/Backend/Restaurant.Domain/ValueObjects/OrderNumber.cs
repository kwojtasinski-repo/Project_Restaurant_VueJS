﻿using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.ValueObjects
{
    public sealed class OrderNumber : IEquatable<OrderNumber>
    {
        public string Value { get; }

        public OrderNumber(string productName)
        {
            ValidProductName(productName);
            Value = productName;
        }

        public static implicit operator string(OrderNumber orderNumber)
            => orderNumber.Value;

        public static implicit operator OrderNumber(string value)
            => new(value);

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderNumber);
        }

        public bool Equals(OrderNumber other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                    .Select(x => x != null ? x.GetHashCode() : 0)
                    .Aggregate((x, y) => x ^ y);
        }

        private IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static void ValidProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new OrderNumberCannotBeEmptyException();
            }
        }
    }
}
