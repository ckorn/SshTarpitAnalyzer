﻿using CrossCutting.DataClasses;
using Logic.LogFileAnalyzation.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic.LogFileAnalyzation
{
    public class LogFileParser : ILogFileParser
    {
        private const string Connected = "connected";
        private const string Disconnected = "disconnected";
        public List<ConnectionInformation> Parse(string text)
        {
            List<ConnectionInformation> ret = new();
            using StringReader stringReader = new(text);
            Regex lineRegex = new(@"(?<date>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}).*Client\('(?<ip>\d+\.\d+\.\d+\.\d+)', (?<port>\d+)\) (?<type>.*)");
            Dictionary<(string ip, int port), DateTime> connectionStartDictionary = new();
            string line;
            while ((line = stringReader.ReadLine()) != null)
            {
                //2021-03-06 18:05:55 INFO     TarpitServer: Client ('218.92.0.203', 30342) disconnected
                //2021-03-06 18:06:22 INFO     TarpitServer: Client ('218.92.0.203', 33047) connected
                Match match = lineRegex.Match(line);
                if (match.Success)
                {
                    DateTime date = DateTime.Parse(match.Groups["date"].Value);
                    string ip = match.Groups["ip"].Value;
                    int port = Int32.Parse(match.Groups["port"].Value);
                    string type = match.Groups["type"].Value;
                    if (type == Connected)
                    {
                        connectionStartDictionary[(ip, port)] = date;
                    }
                    else if (type == Disconnected)
                    {
                        if (connectionStartDictionary.TryGetValue((ip, port), out DateTime startTime))
                        {
                            ret.Add(new()
                            {
                                Duration = date - startTime,
                                Ip = ip,
                                Port = port,
                            });
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"{nameof(type)} = {type}");
                    }
                }
            }
            return ret;
        }

        public List<ConnectionInformation> ParseByFileName(string fileName) => Parse(File.ReadAllText(fileName));
    }
}
