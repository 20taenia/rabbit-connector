namespace Charon.Core.Entities
{
    public interface IEntityBase: IUpdateable
    {
        bool IsDeleted { get; set; }
        ObjectState State { get; set; }
    }
}