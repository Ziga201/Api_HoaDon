using API_HoaDon.Payloads.DTOs;

namespace API_HoaDon.Payloads.Responses
{
    public class ResponseObject<T>
    {
        public int Status { get; set; }
        public string Messege { get; set; }
        public T Data { get; set; }

        public ResponseObject()
        {
        }

        public ResponseObject(int status, string messege, T data)
        {
            Status = status;
            Messege = messege;
            Data = data;
        }
        public ResponseObject<T> ResponseSucess(string message, T data)
        {
            return new ResponseObject<T>(StatusCodes.Status200OK, message, data);
        }
        public ResponseObject<T> ResponseError(int status, string message, T data)
        {
            return new ResponseObject<T>(status, message, data);
        }

    }
}
