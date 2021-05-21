using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Data.Repositories.SendMail
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
