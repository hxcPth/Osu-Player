﻿using Microsoft.EntityFrameworkCore;
using Milky.OsuPlayer.Common;
using Milky.OsuPlayer.Common.Configuration;
using Milky.OsuPlayer.Data;
using Milky.OsuPlayer.Presentation;
using Milky.OsuPlayer.Shared;
using Newtonsoft.Json;
using NLog.Config;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Milky.OsuPlayer
{
    public static class EntryStartup
    {
        public static async Task StartupAsync()
        {
            ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("InvariantCulture", typeof(InvariantCultureLayoutRendererWrapper));
            if (!LoadConfig())
            {
                Environment.Exit(0);
                return;
            }

#if DEBUG
            //ConsoleManager.Show();
#endif

            await InitLocalDb();

            StyleUtilities.SetAlignment();

            //https://ffmpeg.zeranoe.com/builds/win32/shared/ffmpeg-4.2.1-win32-shared.zip
            Unosquare.FFME.Library.FFmpegDirectory = Path.Combine(Domain.PluginPath, "ffmpeg");
        }

        private static bool LoadConfig()
        {
            var file = Domain.ConfigFile;
            if (!File.Exists(file))
            {
                AppSettings.CreateNewConfig();
            }
            else
            {
                try
                {
                    var content = ConcurrentFile.ReadAllText(file);
                    AppSettings.Load(JsonConvert.DeserializeObject<AppSettings>(content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            }
                        )
                    );
                }
                catch (JsonException ex)
                {
                    var result = MessageBox.Show("载入配置文件时失败，用默认配置覆盖继续打开吗？" + Environment.NewLine + ex.Message,
                        "Osu Player", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        AppSettings.CreateNewConfig();
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        private static async Task InitLocalDb()
        {
            await using var dbContext = new ApplicationDbContext();
            dbContext.Database.Migrate();
        }
    }
}