using JustBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
