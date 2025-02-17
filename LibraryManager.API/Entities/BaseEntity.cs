namespace LibraryManager.API.Entities;

public class BaseEntity
{
    public int Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool IsDeleted { get; private set; } = false;

    public void SetAsDeleted() => IsDeleted = true;
}