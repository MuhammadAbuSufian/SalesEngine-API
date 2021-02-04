using System;
using Project.Model;

namespace Project.ViewModel
{
    public class TokenViewModel : BaseViewModel<Token>
    {
        public TokenViewModel(Token token) : base(token)
        {
            ExpireAt = token.ExpireAt;

            Ticket = token.Ticket;

            UserId = token.UserId;

        }

        public DateTime ExpireAt { get; set; }

        public string Ticket { get; set; }

        public string UserId { get; set; }

    }
}