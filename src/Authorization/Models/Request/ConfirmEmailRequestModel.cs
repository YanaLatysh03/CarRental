
namespace Authorization.Models.Request
{
    public class ConfirmEmailRequestModel
    {
        public int Code { get; set; }

        public string Email { get; set; }
    }
}
