using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading;
using JustBot.Services;

namespace JustBot.Controllers
{
    internal class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _memoryStorage= memoryStorage;
            _telegramClient = telegramClient;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;
            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).OperationType = callbackQuery.Data;

            // Генерим информационное сообщение
            string userInput = callbackQuery.Data switch
            {
                "sumChar" => "Подсчет символов",
                "sumNum" => "Сумма чисел",
                _ => String.Empty
            };
            string userInput2 = callbackQuery.Data switch
            {
                "sumChar" => "Введите текст",
                "sumNum" => "Введите числа через пробел",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбор операции - {userInput}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню." + $"{Environment.NewLine}{userInput2} ", cancellationToken: ct, parseMode: ParseMode.Html);



        }
    }
}
