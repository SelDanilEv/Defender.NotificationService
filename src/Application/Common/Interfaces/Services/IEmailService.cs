﻿using Defender.NotificationService.Application.Models;

namespace Defender.NotificationService.Application.Common.Interfaces;

public interface IEmailService
{
    Task<string> SendEmailAsync(NotificationRequest request);
}
