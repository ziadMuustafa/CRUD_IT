namespace API_1.GeneralResponse
{
    public class GeneralResponsesForm : IGeneralResponse
    {
        public bool IsSuccess { get; set; }

        public dynamic ResponseMessage { get; set; }
    }
}
