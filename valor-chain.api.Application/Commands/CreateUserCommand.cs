namespace valor_chain.api.Application.Commands
{
    public class CreateUserCommand
    {
        public string FirstName { get; set; }//
        public string LastName { get; set; }//
        public string Email { get; set; }//
        public string Password { get; set; }//
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }//
        public DateTime BirthDate { get; set; }
        public int region { get; set; }
        public int Prefecture { get; set; }
        public int subPrefecture { get; set; }
        string[] valueChainLink { get; set; }

    }
}
