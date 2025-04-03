namespace Copmany.MVC.PL.Services
{
    public interface ISengletonSerivce
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
