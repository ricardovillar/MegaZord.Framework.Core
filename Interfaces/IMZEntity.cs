namespace MegaZord.Framework.Interfaces {
    public interface IMZEntity {
        long ID { get; set; }
        bool IsNew { get; }
    }
}
