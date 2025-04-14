namespace API_1.GeneralResponse
{
    public interface IGeneralResponse
    {
        public bool IsSuccess { get; set; }

        public dynamic ResponseMessage { get; set; }

    }
}
