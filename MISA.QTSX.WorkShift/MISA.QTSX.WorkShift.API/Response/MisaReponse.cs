namespace MISA.QTSX.WorkShift.API.Response
{
    public class MisaReponse<T>
    {
        public int Code { get; set; }
        public T? data { get; set; }
        public string message { get; set; }
        public string traceId { get; set; }
        public DateTime timestap { get; set; }
        public MisaReponse()
        {
            timestap = DateTime.Now;
            
            message = "";
            traceId = "";
        }
    }
}
