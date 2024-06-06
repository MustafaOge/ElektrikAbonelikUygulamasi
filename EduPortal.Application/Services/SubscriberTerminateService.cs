﻿using EduPortal.Application.Interfaces.Repositories;
using EduPortal.Application.Interfaces.Services;
using EduPortal.Application.Messaging;
using EduPortal.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Application.Services
{
    public class SubscriberTerminateService : ISubscriberTerminateService
    {
        private readonly IGenericRepository<OutboxMessage, int> _outboxMessageRepository;
        private readonly IRabbitMQPublisherService _rabbitMQPublisher;
        private readonly RabbitMQConsumerService _rabbitMqConsumer;

        public SubscriberTerminateService(IGenericRepository<OutboxMessage, int> outboxMessageRepository, IRabbitMQPublisherService rabbitMQPublisher, RabbitMQConsumerService rabbitMQConsumer)
        {
            _outboxMessageRepository = outboxMessageRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
            _rabbitMqConsumer = rabbitMQConsumer;
        }

        public async Task TerminateSubscriptionAndAddToOutbox(int subscriberId)
        {
            // Sonlandırılan abonelik bilgisini outbox tablosuna ekle
            OutboxMessage outboxMessage = new OutboxMessage
            {
                Type = "Subscription-Termination",
                Payload = subscriberId.ToString(),
                CreatedByUser = 10, 
                IsProcessed = false
            };
            Log.Information("Yeni bir abone silindi silenn abone id :" + outboxMessage.Payload);
            await _outboxMessageRepository.AddAsync(outboxMessage);
            await _rabbitMQPublisher.StartPublishing();
              _rabbitMqConsumer.StartConsuming(); 

        }
    }


}