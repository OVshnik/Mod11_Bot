using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading;
using JustBot.Services;

namespace JustBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        public TextMessageController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(Update update,CallbackQuery callbackQuery, Message message, CancellationToken ct)
        {
            string userInput = _memoryStorage.GetSession(message.Chat.Id).OperationType;
            switch (message.Text)
            {
                case "/start":

                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Подсчет символов", "sumChar"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел", "sumNum"),
                    });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Наш бот подсчитывает количество символов в тексте, либо сумму введенных чисел.</b> {Environment.NewLine}", cancellationToken: ct,
                        parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    break;

            }

            if (userInput == "sumChar")
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, $"Длина сообщения: {update.Message.Text.Length} знаков", cancellationToken: ct);
            }
            else if (userInput == "sumNum")
            {
                await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, $"Сумма чисел: {update.Message.Text.Split(new char[] { ' ' }).Select(int.Parse).Sum()}", cancellationToken: ct);
            }
            
        }
    }
}

