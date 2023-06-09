﻿using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace UtilityBotSF.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            if (message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($"Подсчет символов" , $"stringlength"),
                        InlineKeyboardButton.WithCallbackData($"Подсчет суммы чисел" , $"addition")
                    });


                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Телеграм-бот предназначен для подсчета длины строки {Environment.NewLine} и для нахождения суммы чисел.</b> {Environment.NewLine}",
                    cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
            }
            else if (message.Text != "/start" && InlineKeyboardController.OperationText == "Подсчет символов")
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
            }
            else if (message.Text != "/start" && InlineKeyboardController.OperationText == "Подсчет суммы чисел")
            {
                string[] stringArrEntered = message.Text.Split(" ");
                int sum = default;
                try //и как же без блока try catch
                {
                    foreach (var item in stringArrEntered)
                    {
                        sum += Convert.ToInt32(item);
                    }
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {sum}", cancellationToken: ct);
                }
                catch (Exception ex)
                {
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Ошибка: {ex.Message}", cancellationToken: ct);
                }
            }
            else
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Выберите операцию посредством меню /start", cancellationToken: ct);
            }
        }
    }
}