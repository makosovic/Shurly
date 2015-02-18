﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Shurly.Core.Enums;
using Shurly.Core.Models;
using Shurly.Core.Security;

namespace Shurly.Core.Persistance
{
    public class CacheStore : IPersistanceStore
    {
        private static readonly ConcurrentDictionary<string, IAccount> AccountCache = new ConcurrentDictionary<string, IAccount>();
        private static readonly ConcurrentDictionary<string, IShurly> ShurlyCache = new ConcurrentDictionary<string, IShurly>();
        private static readonly ConcurrentDictionary<string, Collection<string>> ShurlyOwnershipCache = new ConcurrentDictionary<string, Collection<string>>();

        #region account store

        public IAccount FindAccountById(string accountId)
        {
            IAccount account;

            if (AccountCache.TryGetValue(accountId, out account))
            {
                return account;
            }
            else
            {
                throw new ApplicationException("Account doesnt exists.");
            }
        }

        public IAccount CreateAccount(string accountId, string password)
        {
            IAccount account = new Account(accountId, password);
            if (AccountCache.TryAdd(accountId, account))
            {
                return account;
            }
            else
            {
                throw new ApplicationException(string.Format("Account with Id {0} already exists.", accountId));
            }
        }

        #endregion

        #region shurly store

        public IShurly Register(string url, string accountId)
        {
            int nextId = this.GetNextShurlyId();
            IShurly shurly = new Models.Shurly(nextId, url, accountId);
            
            if(ShurlyCache.TryAdd(shurly.ShortUrl, shurly))
            {
                RegisterOwnership(shurly.ShortUrl, shurly.OwnerId);
                return shurly;
            }
            else
            {
                throw new ApplicationException("There has been an error inserting new Shurly.");
            }
        }

        public IShurly Register(string url, string accountId, RedirectType redirectType)
        {
            int nextId = this.GetNextShurlyId();
            IShurly shurly = new Models.Shurly(nextId, url, accountId, redirectType);

            if (ShurlyCache.TryAdd(shurly.ShortUrl, shurly))
            {
                RegisterOwnership(shurly.ShortUrl, shurly.OwnerId);
                return shurly;
            }
            else
            {
                throw new ApplicationException("There has been an error inserting new Shurly.");
            }
        }

        public IShurly GetShurlyByShortUrl(string shortUrl)
        {
            IShurly shurly;

            if (ShurlyCache.TryGetValue(shortUrl, out shurly))
            {
                return shurly;
            }
            else
            {
                throw new ApplicationException(string.Format("Url '{0}' is not registered.", shortUrl));
            }
        }

        public void LogRedirect(string shortUrl)
        {
            IShurly shurly;

            if (ShurlyCache.TryGetValue(shortUrl, out shurly))
            {
                shurly.LogVisit();
                ShurlyCache.AddOrUpdate(shortUrl, x => shurly, (x, y) => shurly);
            }
        }

        public IEnumerable<KeyValuePair<string, int>> GetStatistics(string accountId)
        {
            Collection<string> shortUrls;
            Collection<IShurly> shurlies = new Collection<IShurly>();

            if (ShurlyOwnershipCache.TryGetValue(accountId, out shortUrls))
            {
                foreach (var shortUrl in shortUrls)
                {
                    IShurly shurly;

                    if (ShurlyCache.TryGetValue(shortUrl, out shurly))
                    {
                        shurlies.Add(shurly);
                    }
                    else
                    {
                        throw new ApplicationException(string.Format("Shurly '{0}' doesnt exist.", shortUrl));
                    }
                }

                return shurlies.Select(x => new KeyValuePair<string, int>(x.ShortUrl, x.Visits));
            }
            else
            {
                throw new ApplicationException("Account doesnt exists.");
            }
        }

        #region private methods

        private int GetNextShurlyId()
        {
            return ShurlyCache.Count + 1;
        }

        private void RegisterOwnership(string shortUrl, string accountId)
        {
            Collection<string> urls;
            if (ShurlyOwnershipCache.TryGetValue(accountId, out urls))
            {
                urls.Add(shortUrl);
                ShurlyOwnershipCache.AddOrUpdate(accountId, x => urls, (x, y) => urls);
            }
            else
            {
                urls = new Collection<string>();
                urls.Add(shortUrl);

                if (!ShurlyOwnershipCache.TryAdd(accountId, urls))
                    throw new ApplicationException(string.Format("There is no account with id '{0}'", accountId));
            }
        }

        #endregion

        #endregion

    }
}