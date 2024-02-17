﻿using JustBot.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBot.Services
{
    public class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session();
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }

    }
}
