using System;

namespace StarMart.SharedKernel
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; }
        public EntityStatus Status { get; }
        public DateTime CreatedOn { get; }
        public DateTime? ModifiedOn { get; }
        void SetCreatedOn(DateTime createdOn);
        void SetModifiedOn(DateTime modifiedOn);
        void Delete();
        void ThrowDomainException(string message);
        void ThrowDomainException(int errorCode, string message);
        void ThrowDomainException(int errorCode, string message, Exception innerException);
    }
}
