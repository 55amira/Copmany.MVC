namespace Copmany.MVC.PL.Services
{
    public class SengletonService : ISengletonSerivce
    {
        public SengletonService()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
