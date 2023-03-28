using UtilityBotSF.Models;

namespace UtilityBotSF.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
